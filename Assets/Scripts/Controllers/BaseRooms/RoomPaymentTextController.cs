using TMPro;
using UnityEngine;

namespace Controllers.BaseRooms
{
    public class RoomPaymentTextController: MonoBehaviour
    {
        [SerializeField]
        private TextMeshPro remainingCostText;
        public void SetAndUpdateText(int roomCost) => remainingCostText.text = roomCost.ToString();
    }
}