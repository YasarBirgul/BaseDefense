using UnityEngine;

namespace Controllers.Level
{
    public class LevelLoaderController : MonoBehaviour
    { 
        public void InitializeLevel(int levelID, Transform levelHolder)
        {
            Instantiate(Resources.Load<GameObject>($"Prefabs/level{levelID}"),levelHolder);
        }
    }
}