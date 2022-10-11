using System.Collections.Generic;
using UnityEngine;

namespace Controllers.UI
{ 
    public class UIPanelController : MonoBehaviour
    {
        [SerializeField] private List<GameObject> UIPanelList = new List<GameObject>();
        public void OpenPanel(UIPanels panelParam)
        {
            UIPanelList[(int) panelParam].SetActive(true);
        }
        public void ClosePanel(UIPanels panelParam)
        {
            UIPanelList[(int) panelParam].SetActive(false);
        }
    }
}