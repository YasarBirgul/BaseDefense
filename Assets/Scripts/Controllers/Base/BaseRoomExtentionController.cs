using System.Collections.Generic;
using Buyablezone.Interfaces;
using Buyablezone.PurchaseParams;
using Data.ValueObject.LevelData;
using Enums;
using UnityEngine;

namespace Controllers.Base
{
    public class BaseRoomExtentionController : MonoBehaviour,IBuyable
    {
        private List<GameObject> OpenUpExtentions;
        private List<GameObject> CloseDownExtentions;
        
        public void ChangeExtentionVisibility(BaseRoomTypes baseRoomType)
        {
            OpenUpExtentions[(int)baseRoomType].SetActive(true);
            CloseDownExtentions[(int)baseRoomType].SetActive(false);
        }
        public BuyableZoneDataList GetBuyableData()
        {
            throw new System.NotImplementedException();
        }

        public void TriggerBuyingEvent()
        {
            throw new System.NotImplementedException();
        }

        public bool MakePayment()
        {
            throw new System.NotImplementedException();
        }
    }
}