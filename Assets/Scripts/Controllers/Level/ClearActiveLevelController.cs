using UnityEngine;

namespace Controllers.Level
{
    public class ClearActiveLevelController : MonoBehaviour
    { 
        public void ClearActiveLevel(Transform levelHolder)
        {
            Destroy(levelHolder.GetChild(0).gameObject);
        }
    }
}