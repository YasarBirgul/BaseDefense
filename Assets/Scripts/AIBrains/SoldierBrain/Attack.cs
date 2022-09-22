using Abstract;
using UnityEngine;
using UnityEngine.AI;

namespace AIBrains.SoldierBrain
{
    public class Attack : IState
    {
        private SoldierAIBrain _soldierAIBrain;
        private NavMeshAgent _navMeshAgent;
        private float _timer=10f;
        private float _attackTime = 10f;
        public Attack(SoldierAIBrain soldierAIBrain,NavMeshAgent navMeshAgent)
        {
            _soldierAIBrain = soldierAIBrain;
            _navMeshAgent = navMeshAgent; 
        }
        public void Tick()
        {
            _timer -= Time.deltaTime;
            if (_timer <= 0 )
            {
                if (_soldierAIBrain.EnemyTarget != null)
                {
                    Debug.Log("Shoot : " + _soldierAIBrain.enemyList);
                    _soldierAIBrain.EnemyTarget = null;
                    // takedamage.invoke (Func) bize health döndürsün, health eğer 0 ise karşı obje ölsün...
                    // eğer karşı objenin set active`i false ise yani health 0 döndüyse yani health 0 ise target null çekip 
                    // yeni target belirleyelim. 
                    
                    // eğer yeni bir target yok ise patrol state`e geri dönelim.
                }
                else if (_soldierAIBrain.enemyList.Count != 0) 
                {
                    // 
                    
                    Debug.Log("Out Shoot : " + _soldierAIBrain.enemyList);
                    _soldierAIBrain.enemyList.RemoveAt(0);
                    _soldierAIBrain.enemyList.TrimExcess();
                    _soldierAIBrain.SetEnemyTargetTransform();
                }
                _timer = _attackTime ;
            }
        }
        public void OnEnter()
        {
            Debug.Log("AttackEntered");
        }
        public void OnExit()
        {
            
        }
    }
}