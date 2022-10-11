using Abstract;
using Interfaces;
using UnityEngine;
using UnityEngine.AI;

namespace AIBrains.EnemyBrain.States
{
    public class MoveToBomb : IState
    {
        private readonly EnemyAIBrain _enemyAIBrain;
        private readonly NavMeshAgent _navMeshAgent;
        private readonly Animator _animator;
        private readonly float _attackRange;
        private readonly float _chaseSpeed;
        private bool _attackOnPlayer; 

        public MoveToBomb(NavMeshAgent navmeshAgent, Animator animator)
        {
           
        }
       // public void Tick()
       // {
       //     if(_enemyAIBrain.MineTarget!=null){
       //         _navMeshAgent.destination = _enemyAIBrain.MineTarget.transform.position;
       //     }
       // }
       // public void OnEnter()
       // {
       //     Debug.Log("MoveToBomb");
       //     _attackOnPlayer = false;
       //     _navMeshAgent.speed = _chaseSpeed;
       //     _navMeshAgent.SetDestination(_enemyAIBrain.MineTarget.transform.position);
       // }
       // public void OnExit()
       // {
       // }
       public void Tick()
       {
           throw new System.NotImplementedException();
       }

       public void OnEnter()
       {
           throw new System.NotImplementedException();
       }

       public void OnExit()
       {
           throw new System.NotImplementedException();
       }
    }
}