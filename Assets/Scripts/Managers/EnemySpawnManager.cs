using System.Collections;
using System.Collections.Generic;
using AIBrains.EnemyBrain;
using Enums;
using Interfaces;
using Signals;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace Managers
{
    public class EnemySpawnManager : MonoBehaviour, IGetPoolObject
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField]
        private List<Transform> randomTargetTransform;

        [SerializeField]
        private Transform spawnTransform;
        
        [SerializeField]
        private int numberOfEnemiesToSpawn = 50;
        
        #endregion
    
        #region Public Variables
        
        #endregion

        #region Private Variables
        
        private EnemyTypes _enemyType;
        
        private NavMeshTriangulation _triangulation;
        
        private GameObject _enemyAIObj;
        
        private EnemyAIBrain _enemyAIBrain;
        
        private const float _spawnDelay = 2;
        
        #endregion
        
        #endregion
       
        private void Awake()
        {
            StartCoroutine(SpawnEnemies());
        }

        #region Event Subscription
        private void OnEnable()
        {
            SubscribeEvents();
        }
        private void SubscribeEvents()
        {
            AISignals.Instance.getSpawnTransform += SetSpawnTransform;
            AISignals.Instance.getRandomTransform += SetRandomTransform;
        }

        private void UnsubscribeEvents()
        {
            AISignals.Instance.getSpawnTransform -= SetSpawnTransform;
            AISignals.Instance.getRandomTransform -= SetRandomTransform;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion
        private Transform SetSpawnTransform()
        {
            return spawnTransform;
        }

        private Transform SetRandomTransform()
        {
            return randomTargetTransform[Random.Range(0,randomTargetTransform.Count)];
        }
        private IEnumerator SpawnEnemies()
        {
            WaitForSeconds wait = new WaitForSeconds(_spawnDelay);
            
            int spawnedEnemies = 0;

            while (spawnedEnemies < numberOfEnemiesToSpawn)
            {
                DoSpawnEnemy();
                spawnedEnemies++;
                yield return wait;
            }
        }
        private void DoSpawnEnemy()
        {
            int randomType = Random.Range(0,System.Enum.GetNames(typeof(EnemyTypes)).Length-1);
            int randomPercentage = Random.Range(0, 101);
            if (randomType == (int)EnemyTypes.LargeEnemy)
            {
                if (randomPercentage<30)
                {
                    randomType = (int)EnemyTypes.RedEnemy;
                }
            }
            var poolType = (PoolType) System.Enum.Parse(typeof(PoolType), ((EnemyTypes) randomType).ToString());
            GetObject(poolType);
        }
        public GameObject GetObject(PoolType poolName)
        {
            _enemyAIObj = PoolSignals.Instance.onGetObjectFromPool?.Invoke(poolName);
            return _enemyAIObj;
        }
    }
}