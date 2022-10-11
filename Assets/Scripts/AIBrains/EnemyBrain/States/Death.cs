using DG.Tweening;
using Enums;
using Interfaces;
using Signals;
using UnityEngine;
using UnityEngine.AI;

namespace AIBrains.EnemyBrain.States
{ 
    public class Death : IState,IReleasePoolObject,IGetPoolObject
    {
        #region Self Variables

        #region Public Variables
        
        #endregion

        #region Serialized Variables,
        
        #endregion

        #region Private Variables

        private readonly NavMeshAgent _navMeshAgent;
        private readonly Animator _animator;
        private EnemyAIBrain _enemyAIBrain;
        private EnemyTypes _enemyType;
        private static readonly int Die = Animator.StringToHash("Die");

        #endregion
        
        #endregion
        public Death(NavMeshAgent navMeshAgent,Animator animator,EnemyAIBrain enemyAIBrain,EnemyTypes enemyType)
        {
            _navMeshAgent = navMeshAgent;
            _animator = animator;
            _enemyAIBrain = enemyAIBrain;
            _enemyType = enemyType;
        }
        public void Tick()
        {
            
        }
        public void OnEnter()
        {
            EnemyDead();
            _navMeshAgent.enabled = false;
            _animator.SetTrigger(Die);
            for (int i = 0; i < 3; i++)
            {
                var createObj = GetObject(PoolType.Money);
                createObj.transform.position = _enemyAIBrain.transform.position + new Vector3(0,3,0);
            }
        } 
        public void OnExit()
        {
            
        }
        private void EnemyDead()
        {
            var poolType = (PoolType)System.Enum.Parse(typeof(PoolType), _enemyType.ToString());
            DOVirtual.DelayedCall(1f, () =>
            {
                _enemyAIBrain.transform.DOMoveY(-3f,1f).OnComplete(()=> ReleaseObject(_enemyAIBrain.gameObject,poolType));
            });
        }
        public void ReleaseObject(GameObject obj, PoolType poolName)
        {
            PoolSignals.Instance.onReleaseObjectFromPool?.Invoke(poolName,obj);
        }
        public GameObject GetObject(PoolType poolName)
        {
            return PoolSignals.Instance.onGetObjectFromPool?.Invoke(poolName);
        }
    }
}