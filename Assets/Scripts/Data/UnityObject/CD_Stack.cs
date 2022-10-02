using System;
using Data.ValueObject.StackData;
using UnityEngine;

namespace Data.UnityObject
{
    [CreateAssetMenu(fileName = "CD_Stack", menuName = "BaseDefense/CD_Stack", order = 0)]
    public class CD_Stack : ScriptableObject
    {
        public StackData[] StackDatas = new StackData[2];
    }
}