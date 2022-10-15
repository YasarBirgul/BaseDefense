using System.Collections.Generic;
using UnityEngine;

namespace Controllers.UI
{ 
    public class UIPanelController : MonoBehaviour
    {
        [SerializeField]
        private List<GameObject> uiPanelList;
        public void OpenPanel(UIPanels panelParam)
        {
            uiPanelList[(int)panelParam].SetActive(true);
        }
        public void ClosePanel(UIPanels panelParam)
        {
            uiPanelList[(int)panelParam].SetActive(false);
        }
    }
}