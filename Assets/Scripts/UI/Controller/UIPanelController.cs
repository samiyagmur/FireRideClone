using System.Collections.Generic;
using Type;
using UnityEngine;

namespace Controller
{
    public class UIPanelController : MonoBehaviour
    {
        [SerializeField]
        private List<GameObject> uýPanels = new List<GameObject>();

        internal void ChangePanel(UIPanelType panelType, bool panelStatus) => uýPanels[(int)panelType].SetActive(panelStatus);
    }
}