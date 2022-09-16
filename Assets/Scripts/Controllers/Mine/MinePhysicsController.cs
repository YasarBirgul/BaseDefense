using Managers;
using UnityEngine;

namespace Controllers
{
    public class MinePhysicsController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables
            [SerializeField]
            private MineManager mineManager;
            [SerializeField]
            private SphereCollider lureCollider;
            [SerializeField]
            private Collider marketCollider;
            [SerializeField]
            private SphereCollider explosionCollider;
        #endregion

        #region Private Variables
        private int initalLureSphereSize = 30;
        private int initalExplosionSphereSize = 10;

        private float timer;
        private float payOffset=0.1f;
        

        #endregion
        #endregion
        
        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                if (timer>payOffset)
                {
                    // Module will be applied
                    mineManager.PayGemToMine();
                    timer = 0;
                }
                else
                {
                    timer += Time.deltaTime;
                }
            }
        }

        public void LureColliderState(bool _state)
        {
            if (_state)
            {
                
                //gameObject.tag = "MineLure";
                lureCollider.radius = initalExplosionSphereSize;
                lureCollider.enabled = true;
            }
            else
            {
                lureCollider.radius = .1f;
                lureCollider.enabled = false;
            }
        }
        public void ExplosionColliderState(bool _state)
        {
            if (_state)
            {
               //gameObject.tag = "MineExplosion";
               lureCollider.radius = initalLureSphereSize;
                explosionCollider.enabled = true;
            }
            else
            {
                lureCollider.radius = .1f;
                explosionCollider.enabled = false;
                //Satin alma veonun etkilesimi revize edilecek
            }
        }
    }
}