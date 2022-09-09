using System.Collections;
using Managers;
using UnityEngine;

namespace Controllers.Detector
{
    public class Detector : MonoBehaviour
    {
        public bool EnemyInRange => _detectedBeast != null;

        private PlayerManager _detectedBeast;

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<PlayerManager>())
            {
                _detectedBeast = other.GetComponent<PlayerManager>(); 
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.GetComponent<PlayerManager>())
            {
                StartCoroutine(ClearDetectedBeastAfterDelay());
            }
        }

        private IEnumerator ClearDetectedBeastAfterDelay()
        {
            yield return new WaitForSeconds(3f);
            _detectedBeast = null;
        }

        public Vector3 GetNearestBeastPosition()
        {
            return _detectedBeast?.transform.position ?? Vector3.zero;
        }
    }
}