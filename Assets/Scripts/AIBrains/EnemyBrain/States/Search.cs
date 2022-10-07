using System.Collections.Generic;
using Abstract;
using UnityEngine;
using UnityEngine.AI;

namespace AIBrains.EnemyBrain.States
{
    public class Search : IState
    {
        private readonly EnemyAIBrain _enemyAIBrain;
        private readonly NavMeshAgent _navMeshAgent;
        private readonly Transform _spawnPoint;
        private readonly List<Transform> _targetList;

        public Search(EnemyAIBrain enemyAIBrain, NavMeshAgent navMeshAgent, Transform spawnPoint)
        {
            _enemyAIBrain = enemyAIBrain;
            _navMeshAgent = navMeshAgent;
            _spawnPoint = spawnPoint;
        }
        public void Tick()
        {
            
        }
        public void OnEnter()
        {
            _enemyAIBrain.enabled = true;
            GetRandomPointBakedSurface();
        }
        public void OnExit()
        {
            
        }

        private void GetRandomPointBakedSurface()
        {
            bool RandomPoint(Vector3 center, float range, out Vector3 result)
            {
                for (int i = 0; i < 60; i++)
                {
                    Vector3 randomPoint = center + Random.insideUnitSphere*range;
                    Vector3 randomPosition = new Vector3(randomPoint.x, 0, _spawnPoint.transform.position.z);
                    NavMeshHit hit;
                    if (NavMesh.SamplePosition(randomPosition, out hit, 1.0f, 1))
                    {
                        result = hit.position;
                        return true;
                    }
                }
                result = Vector3.zero;
                return false;
            }
            if(!RandomPoint(_spawnPoint.position,20,out var point)) return;
            _navMeshAgent.Warp(point);
        }
    }
}