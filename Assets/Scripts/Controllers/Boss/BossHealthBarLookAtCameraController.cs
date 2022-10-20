using UnityEngine;

namespace Controllers.Boss
{
    public class BossHealthBarLookAtCameraController : MonoBehaviour
    {
        private Camera _mainCamera; 
        private void Awake()
        {
            _mainCamera = Camera.main;
        }
        public void Update()
        {
            var rotation = _mainCamera.transform.rotation;
            transform.LookAt(transform.position + rotation * Vector3.forward, rotation * Vector3.up);
        }
    }
}