using Enums;
using TMPro;
using UnityEngine;

namespace Controllers.Payment
{
    public class RoomPaymentTextController: MonoBehaviour
    {
        [SerializeField]
        private TextMeshPro remainingCostText;

        public void SetInitText(BaseRoomTypes roomType)
        {
            
        }
        
        public void ChangeText(int leftCost)
        {
            remainingCostText.text = leftCost.ToString();
        }
    }
}