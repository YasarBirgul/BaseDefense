using UnityEngine;

namespace Commands.LevelCommands
{
    public class LevelLoaderCommand : MonoBehaviour
    { 
        public void InitializeLevel(int _levelID, Transform levelHolder)
        {
            Instantiate(Resources.Load<GameObject>($"Prefabs/level{_levelID}"),levelHolder);
        }
    }
}