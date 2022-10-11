using System.Collections.Generic;
using Data.ValueObject.PlayerData;
using UnityEngine;

namespace Data.UnityObject
{
    [CreateAssetMenu(fileName = "CD_PlayerStack", menuName = "BaseDefense/CD_PlayerStack", order = 0)]
    public class CD_PlayerStack : ScriptableObject
    {
        public List<PlayerStackData> PlayerStackDataList;
    }
}