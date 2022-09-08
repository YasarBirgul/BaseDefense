using System.Collections.Generic;
using Data.ValueObject.AIStackData;
using UnityEngine;

namespace Data.UnityObject
{
    [CreateAssetMenu(fileName = "CD_AIStack", menuName = "BaseDefense/CD_AIStack", order = 0)]
    public class CD_AIStack : ScriptableObject
    {
       public List<AIStackData> AIStackList;
    }
}