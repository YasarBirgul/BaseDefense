using Data.ValueObject.AIData;
using UnityEngine;

namespace Data.UnityObject
{
    [CreateAssetMenu(fileName = "CD_AI", menuName = "BaseDefense/CD_AI", order = 0)]
    public class CD_AI : ScriptableObject
    {
        public AmmoWorkerAIData AmmoWorkerAIData;
        public MineWorkerAIData MineWorkerAIData;
        public SoldierAIData SoldierAIData;
    }
}