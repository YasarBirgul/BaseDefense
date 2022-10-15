using System.Collections.Generic;
using System.Threading.Tasks;
using Abstract;
using DG.Tweening;
using Enums;
using Interfaces;
using Signals;
using UnityEngine;

namespace Controllers.Player
{
    [RequireComponent(typeof(StackController.StackController))]
    public class MoneyStackerController : AStacker,IReleasePoolObject
    {
        #region Self Variables

        #region Public Variables
        
        #endregion

        #region Serialized Variables

        [SerializeField] 
        private List<Vector3> positionList;

        [SerializeField] 
        private float radiusAround;
        
        #endregion

        #region Private Variables

        private Sequence _getStackSequence;

        private int _stackListConstCount;

        private bool _canRemove = true;
        
        #endregion

        #endregion
        
        private void Awake()
        {
            DOTween.Init(true, true, LogBehaviour.Verbose).SetCapacity(500, 125);
        }
        private new List<GameObject> StackList
        {
            get => base.StackList;
            set => base.StackList = value;
        }
        public override void SetStackHolder(Transform otherTransform)
        {
           otherTransform.SetParent(transform);
        }
        public override void GetStack(GameObject stackableObj)
        {   
            
            _getStackSequence = DOTween.Sequence();
            var randomBouncePosition =CalculateRandomAddStackPosition();
            var randomRotation = CalculateRandomStackRotation();
            
            _getStackSequence.Append(stackableObj.transform.DOLocalMove(randomBouncePosition, .5f));
            _getStackSequence.Join(stackableObj.transform.DOLocalRotate(randomRotation, .5f)).OnComplete(() =>
            {
                stackableObj.transform.rotation = Quaternion.LookRotation(transform.forward);
                StackList.Add(stackableObj);
                stackableObj.transform.DOLocalMove(positionList[StackList.Count - 1], 0.3f);
                stackableObj.transform.localRotation = new Quaternion(0,0,0,0).normalized;
            });
        }
        public void OnRemoveAllStack()
        {
            if(!_canRemove)
                return;
            _canRemove = false;
            _stackListConstCount = StackList.Count;
            RemoveAllStack();
        }
        private async void RemoveAllStack()
        {
            if (StackList.Count == 0)
            {
                _canRemove = true;
                return;
            }
            if(StackList.Count >= _stackListConstCount -6)
            {
                RemoveStackAnimation(StackList[StackList.Count - 1]);
                StackList.TrimExcess();
                await Task.Delay(201);
                RemoveAllStack();
            }
            else
            {
                for (int i = 0; i < StackList.Count; i++)
                {   
                    RemoveStackAnimation(StackList[i]);
                    StackList.TrimExcess();
                }
                _canRemove = true;
            }
        }
        private void RemoveStackAnimation(GameObject removedStack)
        {
            _getStackSequence = DOTween.Sequence();
            var randomRemovedStackPosition = CalculateRandomRemoveStackPosition();
            var randomRemovedStackRotation = CalculateRandomStackRotation();
            
            _getStackSequence.Append(removedStack.transform.DOLocalMove(randomRemovedStackPosition, .2f));
            _getStackSequence.Join(removedStack.transform.DOLocalRotate(randomRemovedStackRotation, .2f)).OnComplete(() =>
            {
                removedStack.transform.rotation = Quaternion.LookRotation(transform.forward);

                StackList.Remove(removedStack);
                removedStack.transform.DOLocalMove(transform.localPosition, .1f).OnComplete(() =>
                {
                    ReleaseObject(removedStack,PoolType.Money);
                });
            });
        }
        public override void ResetStack(IStackable stackable)
        {
           
        } 
        public void GetStackPositions(List<Vector3> stackPositions)
        {
            positionList = stackPositions;
        }
        private Vector3 CalculateRandomAddStackPosition()
        {
            var randomHeight = Random.Range(0.1f, 3f);
            var randomAngle = Random.Range(230,310);
            var rad = randomAngle * Mathf.Deg2Rad;
            return  new Vector3(radiusAround * Mathf.Cos(rad),
                transform.parent.position.y + randomHeight, -radiusAround * Mathf.Sin(rad));
        }
        private Vector3 CalculateRandomRemoveStackPosition()
        {
            var randomHeight = Random.Range(0.1f, 3f);
            var randomAngle = Random.Range(1,179);
            var rad = randomAngle * Mathf.Deg2Rad;
            return  new Vector3(radiusAround * Mathf.Cos(rad),
                transform.parent.position.y + randomHeight, radiusAround * Mathf.Sin(rad));
        }
        private Vector3 CalculateRandomStackRotation()
        {
            var randomRotationX = Random.Range(-90, 90);
            var randomRotationY = Random.Range(-90, 90);
            var randomRotationZ = Random.Range(-90, 90);
            return new Vector3(randomRotationX,randomRotationY,randomRotationZ);
        }
        public void ReleaseObject(GameObject obj, PoolType poolName)
        { 
            PoolSignals.Instance.onReleaseObjectFromPool.Invoke(poolName,obj);
        }
    }
}