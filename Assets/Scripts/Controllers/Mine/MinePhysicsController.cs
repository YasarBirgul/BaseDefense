using Managers;
using UnityEngine;

namespace Controllers.Mine
{
    public class MinePhysicsController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables
            [SerializeField]
            private MineBombManager mineBombManager;
            [SerializeField]
            private SphereCollider lureCollider;
            [SerializeField]
            private Collider marketCollider;
            [SerializeField]
            private SphereCollider explosionCollider;
        #endregion

        #region Private Variables
        
        private const int _initalLureSphereSize = 30;
        private const int _initalExplosionSphereSize = 10;
        private const float _payOffset=0.1f;
        
        private float _timer;

        #endregion
        #endregion
        
        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                if (_timer>_payOffset)
                {
                    // Module will be applied
                    mineBombManager.PayGemToMine();
                    _timer = 0;
                }
                else
                {
                    _timer += Time.deltaTime;
                }
            }
        }

        public void LureColliderState(bool _state)
        {
            if (_state)
            {
                
                //gameObject.tag = "MineLure";
                lureCollider.radius = _initalExplosionSphereSize;
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
               lureCollider.radius = _initalLureSphereSize;
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