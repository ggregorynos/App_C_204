using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.Mephistoss.PanelMachine.Scripts
{
    public class CloseLastPanelButton : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private PanelMachine _panelMachine;

        public PanelMachine PanelMachine => _panelMachine;

        public void OnPointerClick(PointerEventData eventData)
        {
            _panelMachine.CloseLastPanel();
        }
    }
}