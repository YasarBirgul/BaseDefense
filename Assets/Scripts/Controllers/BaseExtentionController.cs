using System.Collections.Generic;
using Controllers.Payment;
using Enums;
using UnityEngine;

namespace Controllers
{ 
    public class BaseExtentionController : MonoBehaviour
    {
        [SerializeField]
        private List<GameObject> OpenUpExtentions = new List<GameObject>();
        [SerializeField]
        private List<GameObject> CloseDownExtentions=new List<GameObject>();
        
        public List<GameObject> PaymentZoneList=new List<GameObject>();
        public void ChangeExtentionVisibility(BaseRoomTypes baseRoomType)
        {
            OpenUpExtentions[(int)baseRoomType].SetActive(true);
            CloseDownExtentions[(int)baseRoomType].SetActive(false);
            PaymentZoneList[(int)baseRoomType].gameObject.SetActive(false);
            if (PaymentZoneList.Count <= (int) baseRoomType + 2) return;
            {
                PaymentZoneList[(int)baseRoomType + 2].gameObject.SetActive(true);
            }
        }
    }
}