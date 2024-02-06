using System;
using CodeHub.OtherUtilities;
using DG.Tweening;
using Game.Mephistoss.PanelMachine.Scripts;
using Game.Scripts.Game.GameLogic.BetLogic;
using UnityEngine;

namespace Game.Scripts.Game.GameLogic
{
    public class GameContext : MonoBehaviour
    {
        [SerializeField] private BetContext _betContext;
        [SerializeField] private PlayerDatabase _playerDatabase;
        [SerializeField] private BetInputController _betInputController;
        [SerializeField] private PanelMachine _panelMachine;
        [SerializeField] private PanelBase _noCoinPanel;
        [SerializeField] private float _noCoinDurationPanel;

        private Tween _closePanelTween;

        private void Start()
        {
            Initialize();
            _betContext.OnLessMoney += AddNoCoinPanel;
            CheckPlayerBalance();
        }

        public void CloseNoCoinPanel()
        {
            _closePanelTween?.Kill();
            _panelMachine.CloseLastPanel();
        }

        private void CheckPlayerBalance()
        {
            if (_playerDatabase.PlayerBalance <= 0)
            {
                AddNoCoinPanel();
            }
        }

        private void Initialize()
        {
            _betContext.Initialize();
            _betInputController.Initialize();
        }

        private void AddNoCoinPanel()
        {
            _panelMachine.AddPanel(_noCoinPanel);
            _closePanelTween = DOVirtual.DelayedCall(_noCoinDurationPanel, (() => _panelMachine.CloseLastPanel()));
        }

        private void OnDestroy()
        {
            _closePanelTween?.Kill();
            _betContext.OnLessMoney -= AddNoCoinPanel;
        }
    }
}