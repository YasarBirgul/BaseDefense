using Signals;
using UnityEngine;

namespace Controllers.MilitaryBase
{
    public class MilitaryBaseSoldierAmountUpgradeButtonController : MonoBehaviour
    { 
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                AISignals.Instance.onSoldierAmountUpgrade?.Invoke();
            }
        }
    }
}