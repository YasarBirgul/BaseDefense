using System;
using System.Collections;
using System.Collections.Generic;
using AIBrains.EnemyBrain;
using Enums;
using Interfaces;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace Managers
{
    public class EnemySpawnManager : MonoBehaviour, IGetPoolObject
    {
        #region Self Variables

        #region Serialized Variables

        #endregion
    
        #region Public Variables
        
        public int NumberOfEnemiesToSpawn = 50;
        
        public float SpawnDelay = 2;
        
        #endregion

        #region Private Variables
        
        private EnemyTypes enemyType;
        
        private NavMeshTriangulation triangulation;
        
        private GameObject _EnemyAIObj;
        private EnemyAIBrain _EnemyAIBrain;
        
        #endregion
        
        #endregion
       
        private void Awake()
        {
            StartCoroutine(SpawnEnemies());
        }
        private IEnumerator SpawnEnemies()
        {
            WaitForSeconds wait = new WaitForSeconds(SpawnDelay);
            
            int spawnedEnemies = 0;

            while (spawnedEnemies < NumberOfEnemiesToSpawn)
            {
                DoSpawnEnemy();
                spawnedEnemies++;
                yield return wait;
            }
        }
        private void DoSpawnEnemy()
        {
            int randomType = Random.Range(0, Enum.GetNames(typeof(EnemyTypes)).Length-1);
            int randomPercentage = Random.Range(0, 101);
            if (randomType == (int)EnemyTypes.LargeEnemy)
            {
                if (randomPercentage<30)
                {
                    randomType = (int)EnemyTypes.RedEnemy;
                }
            }
            GetObject(((EnemyTypes)randomType).ToString());
        }
        public GameObject GetObject(string poolName)
        {
            _EnemyAIObj = ObjectPoolManager.Instance.GetObject<GameObject>(poolName);
            return _EnemyAIObj;
        }
    }
}