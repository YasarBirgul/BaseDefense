using System.Collections.Generic;
using Enums.BaseArea;
using UnityEngine;

namespace Controllers.BaseRooms
{ 
    public class BaseExtentionController : MonoBehaviour
    {
        [SerializeField]
        private List<GameObject> openUpExtentions = new List<GameObject>();
        [SerializeField]
        private List<GameObject> closeDownExtentions=new List<GameObject>();
        
        public List<GameObject> paymentZoneList=new List<GameObject>();
        public void ChangeExtentionVisibility(BaseRoomTypes baseRoomType)
        {
            openUpExtentions[(int)baseRoomType].SetActive(true);
            closeDownExtentions[(int)baseRoomType].SetActive(false);
            paymentZoneList[(int)baseRoomType].gameObject.SetActive(false);
            if (paymentZoneList.Count <= (int) baseRoomType + 2) return;
            {
                paymentZoneList[(int)baseRoomType + 2].gameObject.SetActive(true);
            }
        }
    }
}