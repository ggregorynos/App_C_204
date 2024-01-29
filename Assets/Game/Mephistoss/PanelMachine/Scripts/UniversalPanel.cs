using DG.Tweening;
using UnityEngine;

namespace Game.Mephistoss.PanelMachine.Scripts
{
    public class UniversalPanel : PanelBase
    {
        [SerializeField] private GameObject panelObject;
        [SerializeField] private GameObject objectToAnim;
        [SerializeField] private bool escapeKeyPressed = true;

        private float durationForOpen = 0.6f;
        private float durationForClose = 0.2f;

        public override void Enter(PanelMachine machineInstance)
        {
            base.Enter(machineInstance);
            OpenPanel();
        }

        public override void Exit(PanelMachine machineInstance)
        {
            base.Exit(machineInstance);
            ClosePanel();
        }

        public override void EscapeKeyPressed()
        {
            if (!escapeKeyPressed) return;
            base.EscapeKeyPressed();
            machine.CloseLastPanel();
        }

        private void OpenPanel()
        {
            if (objectToAnim != null)
            {
                objectToAnim.transform.DOKill();
                objectToAnim.transform.localScale = Vector3.zero;
                objectToAnim.transform.DOScale(Vector3.one, durationForOpen).SetEase(Ease.OutBack);
            }

            panelObject.gameObject.SetActive(true);
        }

        private void ClosePanel()
        {
            if (objectToAnim != null)
            {
                objectToAnim.transform.DOKill();
                objectToAnim.transform.localScale = Vector3.one;
                objectToAnim.transform.DOScale(Vector3.zero, durationForClose).OnComplete(() =>
                {
                    panelObject.gameObject.SetActive(false);
                });
            }
            else
            {
                panelObject.gameObject.SetActive(false);
            }
        }
    }
}