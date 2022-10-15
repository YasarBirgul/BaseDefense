using System.Collections.Generic;
using UnityEngine;
using Signals;
using System.Linq;
using AIBrains.WorkerBrain.MoneyWorker;
using Concrete;
using Data.UnityObject;
using Data.ValueObject.AIData.WorkerAIData;
using Enums;
using Sirenix.OdinInspector;
using Interfaces;

namespace Managers
{
    public class MoneyWorkerManager : MonoBehaviour ,IGetPoolObject,IReleasePoolObject
    {
      #region Self variables 

        #region Private Variables

        [ShowInInspector]
        private List<StackableMoney> _targetList = new List<StackableMoney>();
        [ShowInInspector]
        private List<MoneyWorkerAIBrain> _workerList = new List<MoneyWorkerAIBrain>();
        [ShowInInspector]
        private List<Vector3> _slotTransformList = new List<Vector3>();
        
        #endregion

        #region Serialized Variables

        [SerializeField] 
        private Transform moneyWorkerStartTransform;

        #endregion
        
        
        #endregion

        #region Event Subscriptions

        private void OnEnable()
        {
            SubscribeEvents();
        }
        private void SubscribeEvents()
        {
            AISignals.Instance.onGetMoneyAIData += OnGetWorkerAIData;
            AISignals.Instance.onGetTransformMoney += OnSendMoneyPositionToWorkers;
            AISignals.Instance.onThisMoneyTaken += OnThisMoneyTaken;
            AISignals.Instance.onSetMoneyPosition += OnAddMoneyPositionToList;
            //MoneyWorkerSignals.Instance.onSendWaitPosition += OnSendWaitPosition;
        }

        private void UnsubscribeEvents()
        {
            AISignals.Instance.onGetMoneyAIData -= OnGetWorkerAIData;
            AISignals.Instance.onGetTransformMoney -= OnSendMoneyPositionToWorkers;
            AISignals.Instance.onThisMoneyTaken -= OnThisMoneyTaken;
            AISignals.Instance.onSetMoneyPosition -= OnAddMoneyPositionToList;
            //MoneyWorkerSignals.Instance.onSendWaitPosition -= OnSendWaitPosition;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        private WorkerAITypeData OnGetWorkerAIData(WorkerType type)
        {
            return Resources.Load<CD_WorkerAI>("Data/CD_WorkerAI").WorkerAIData.WorkerAITypes[(int)type];
        }

        private void OnAddMoneyPositionToList(StackableMoney pos)
        {
            _targetList.Add(pos);
        }

        private Transform OnSendMoneyPositionToWorkers(Transform workerTransform)
        {
            if (_targetList.Count == 0)
                return null;

            var _targetT = _targetList.OrderBy(t => (t.transform.position - workerTransform.transform.position).sqrMagnitude)
            .Where(t => !t.IsSelected)
            .Take(_targetList.Count - 1)
            .LastOrDefault();
            _targetT.IsSelected = true;
            return _targetT.transform;
        }

        private void SendMoneyPositionToWorkers(Transform workerTransform)
        {
            OnSendMoneyPositionToWorkers(workerTransform);
        }

        private void OnThisMoneyTaken()
        {
            var removedObj = _targetList.Where(t => t.IsCollected).FirstOrDefault();
            _targetList.Remove(removedObj);
            _targetList.TrimExcess();

            for (int i = 0; i < _workerList.Count; i++)
            {
                if (_workerList[i].CurrentTarget == removedObj)
                {
                    SendMoneyPositionToWorkers(_workerList[i].transform);
                }
            }
        } 
        public void GetStackPositions(List<Vector3> gridPos)
        {
            int gridCount = gridPos.Count;
            for (int i = 0; i < gridCount; i++)
            { 
              _slotTransformList.Add(gridPos[i]+moneyWorkerStartTransform.position);
            }
        }
        private void SetWorkerPosition(MoneyWorkerAIBrain workerAIBrain)
        {
             workerAIBrain.SetInitPosition(_slotTransformList[0]+moneyWorkerStartTransform.position);
            _slotTransformList.RemoveAt(0);
            _slotTransformList.TrimExcess();
        }
        [Button("Add Money Worker")]
        private void CreateMoneyWorker()
        {
            if(_workerList.Count == 3) return;
            var obj = GetObject(PoolType.MoneyWorkerAI) ;
            var objComp = obj.GetComponent<MoneyWorkerAIBrain>();
            _workerList.Add(objComp);
            SetWorkerPosition(objComp);
        }
        [Button("Release Worker")]
        private void ReleaseMoneyWorker()
        {
            if (_workerList[0])
            {
                var obj = _workerList[0];
                ReleaseObject(obj.gameObject, PoolType.MoneyWorkerAI);
                _workerList.Remove(obj);
            }
        }
        public void ReleaseObject(GameObject obj, PoolType poolName)
        {
            PoolSignals.Instance.onReleaseObjectFromPool?.Invoke(poolName, obj);
        }
        #endregion

        public GameObject GetObject(PoolType poolName)
        {
            return PoolSignals.Instance.onGetObjectFromPool?.Invoke(poolName);
        }
        public void GenerateSoldier()
        {
            CreateMoneyWorker();
        }
    }
}
