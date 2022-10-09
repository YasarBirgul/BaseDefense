using UnityEngine;

namespace Data.UnityObject
{
    [CreateAssetMenu(fileName = "CD_Score", menuName = "BaseDefense/CD_Score", order = 0)]
    public class CD_Score : ScriptableObject
    {
        public ScoreData ScoreData;
    }
}