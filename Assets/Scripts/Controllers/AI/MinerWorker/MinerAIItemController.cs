using System.Collections.Generic;
using System.Linq;
using Enum;
using Enums;
using Interfaces;
using Signals;
using UnityEngine;

namespace Controllers.AI.MinerWorker
{
    public class MinerAIItemController : MonoBehaviour,IGetPoolObject,IReleasePoolObject
    {
        #region Self Variables

        #region Public Variables

        private Dictionary<MinerItems, GameObject> _itemList = new Dictionary<MinerItems, GameObject>();

        #endregion
        #region Serialized Variables

        [SerializeField]
        private GameObject pickaxe;
        [SerializeField] 
        private GameObject gem;
        [SerializeField]
        private Transform gemHolder;

        #endregion
        #endregion

        private void Awake()
        {
            AddToDictionary();
            CloseAllObject();
            
        }
        private void CloseAllObject()
        {
            for (int index = 0; index < _itemList.Count; index++)
            {
                _itemList.ElementAt(index).Value.SetActive(false);
            }
        }
        private void AddToDictionary()
        {
            _itemList.Add(MinerItems.Gem, gem);
            _itemList.Add(MinerItems.Pickaxe, pickaxe);
        }

        public void OpenItem(MinerItems currentItem)
        {
            if (MinerItems.Pickaxe == currentItem)
            {
                _itemList[currentItem].SetActive(true);
            }
            if (MinerItems.None==currentItem)
            {
               foreach (Transform child in gemHolder) {
                   ReleaseObject(child.gameObject,PoolType.Gem);
               }
            }
            if (MinerItems.Gem == currentItem)
            {
                gem = GetObject(PoolType.Gem);
                gem.transform.parent=gemHolder;
                gem.transform.localPosition=Vector3.zero;
                gem.transform.localScale=Vector3.one*3;
                gem.transform.localRotation= Quaternion.identity;
            }
        } 
        public void CloseItem(MinerItems currentItem)
        {
            if (MinerItems.Pickaxe == currentItem)
            {
               _itemList[currentItem].SetActive(false);
            }
        } 
        public GameObject GetObject(PoolType poolName)
        {
            return PoolSignals.Instance.onGetObjectFromPool?.Invoke(poolName);
        } 
        public void ReleaseObject(GameObject obj, PoolType poolName)
        {
            PoolSignals.Instance.onReleaseObjectFromPool?.Invoke(poolName,obj);
        }
    }
}