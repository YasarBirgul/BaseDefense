using System.Collections.Generic;
using Data.ValueObject.LevelData;
using Interfaces;
using UnityEngine;

namespace Data.UnityObject
{
    [CreateAssetMenu(fileName = "CD_Level", menuName = "BaseDefense/CD_Level", order = 0)]
    public class CD_Level : ScriptableObject,SaveableEntity
    {
        public List<LevelData> LevelDatas=new List<LevelData>();
        public int LevelId;
        public string Key ="LevelData";
        public CD_Level()
        {
            
            
        } 
        public CD_Level(int levelId,List<LevelData> levelDatas)
        {
            LevelId = levelId;
            LevelDatas = levelDatas;

        }
        public string GetKey()
        {
            return Key;
        }
    }
}