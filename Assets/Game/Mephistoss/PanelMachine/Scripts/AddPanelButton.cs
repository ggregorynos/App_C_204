using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.Mephistoss.PanelMachine.Scripts
{
    public class AddPanelButton : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private PanelMachine _panelMachine;
        [SerializeField] private PanelBase _panelBase;

        public PanelMachine PanelMachine => _panelMachine;
        public PanelBase PanelBase => _panelBase;

        public void OnPointerClick(PointerEventData eventData)
        {
            _panelMachine.AddPanel(_panelBase);
        }
    }
}