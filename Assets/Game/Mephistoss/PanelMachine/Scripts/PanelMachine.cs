using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using UnityEngine;

namespace Game.Mephistoss.PanelMachine.Scripts
{
    public class PanelMachine : MonoBehaviour
    {
        [SerializeField] private PanelBase _startPanel;
        [SerializeField] [ReadOnly] private List<PanelBase> _panels;

        private bool _transition = true;

        private void Start()
        {
            _panels = new List<PanelBase>();
            if(_startPanel!=null)
                AddPanel(_startPanel);
        }

        public void SwitchLastPanelTo(PanelBase panel)
        {
            if (!_panels.Any() && !_transition)
                return;

            CloseLastPanel();
            AddPanel(panel);
        }

        public void AddPanel(PanelBase panel)
        {
            if (!_transition)
                return;

            _panels.Add(panel);
            panel.Enter(this);
        }

        public void CloseLastPanel()
        {
            if (!_panels.Any())
                return;

            if (!LastPanel.CanExit() || !_transition)
                return;

            LastPanel.Exit(this);
            _panels.Remove(LastPanel);
        }

        public void ReactToCloseAction()
        {
            if (!_panels.Any())
                return;

            LastPanel.EscapeKeyPressed();
        }

        public void ClosePanelByInterface<T>(bool forceClose)
        {
            for (int i = 0; i < _panels.Count; i++)
            {
                if (_panels[i] is T)
                {
                    if (_panels[i].CanExit() == false && forceClose == false)
                    {
                        continue;
                    }

                    _panels[i].Exit(this);
                    _panels.RemoveAt(i);
                }
            }
        }

        private PanelBase LastPanel => _panels[_panels.Count - 1];

        public void EnableTransition(bool enable)
        {
            _transition = enable;
        }

        public void CloseAllMenuPanel()
        {
            ClosePanelByInterface<IMenuPanel>(false);
        }

        public void ClosePanelForPanel(PanelBase panelBase)
        {
            var cloneStates = _panels;
            for (int i = cloneStates.Count - 1; i >= 0; i--)
            {
                if (cloneStates[i] == panelBase)
                {
                    break;
                }

                CloseLastPanel();
            }
        }
    }
}