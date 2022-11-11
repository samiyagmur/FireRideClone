using System.Collections.Generic;
using Type;
using UnityEngine;

namespace Controller
{
    public class UIPanelController : MonoBehaviour
    {
        [SerializeField]
        private List<GameObject> u�Panels = new List<GameObject>();

        internal void ChangePanel(UIPanelType panelType, bool panelStatus) => u�Panels[(int)panelType].SetActive(panelStatus);
    }
}