using CodeHub.OtherUtilities;
using DG.Tweening;
using Game.Mephistoss.PanelMachine.Scripts;
using Game.Scripts.Game.GameLogic.BetLogic;
using Game.Scripts.Game.GameLogic.GameData;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.Game.GameLogic
{
    public class ResultPanelContext : MonoBehaviour
    {
        private const int MultiplyNumberReward = 4;
        private const int MultiplyColorReward = 2;

        [SerializeField] private PlayerDatabase _playerDatabase;
        [SerializeField] private PanelMachine _panelMachine;
        [SerializeField] private LocalPlayerInputController _playerInputController;

        [SerializeField] private PanelBase _cardPanel;
        [SerializeField] private PanelBase _winPanel;

        [SerializeField] private BetContext _betContext;
        [SerializeField] private BetInputController _betInputController;

        [SerializeField] private Image _cardBack;
        [SerializeField] private TMP_Text _cardNumber;
        [SerializeField] private TMP_Text _winCountTxt;

        [SerializeField] private float _durationCardPanel;
        [SerializeField] private float _durationWinPanel;

        [SerializeField] private AudioSource _win;


        public void ShowPanel(NumberData numberData)
        {
            UpdateCardPanel(numberData);
            _panelMachine.AddPanel(_cardPanel);
            DOVirtual.DelayedCall(_durationCardPanel, () => TryAddWinPanel(numberData));
        }

        private void UpdateCardPanel(NumberData numberData)
        {
            _cardBack.sprite = numberData.ColorData.BackPanel;
            _cardNumber.text = numberData.Number + "";
        }

        private void TryAddWinPanel(NumberData numberData)
        {
            if (HasSomeReward(numberData))
            {
                _panelMachine.CloseLastPanel();
                _panelMachine.AddPanel(_winPanel);
                int reward = GetReward(numberData);
                _betContext.IncreaseWinCount(reward);
                _winCountTxt.text = reward + "";
                DOVirtual.DelayedCall(_durationWinPanel, ReturnToGame);

                _win.Play();
            }
            else
                ReturnToGame();
        }

        private void ReturnToGame()
        {
            _panelMachine.CloseLastPanel();

            _playerInputController.SetEnableButtons(true);
            _playerInputController.SetEnableOverlayClickBlockers(false);

            _betContext.ResetField();

            _betInputController.CheckLocalBet();
            _betContext.UpdatePlayBtnStatus();
        }

        private int GetReward(NumberData numberData)
        {
            int numberReward = 0;
            int colorReward = 0;
            if (HasWinNumber(numberData))
            {
                numberReward = _betContext.LocalBet * MultiplyNumberReward;
                _playerDatabase.IncreasePlayerBalance(numberReward);
            }

            if (HasWinColor(numberData))
            {
                colorReward = _betContext.LocalBet * MultiplyColorReward;
                _playerDatabase.IncreasePlayerBalance(colorReward);
            }

            return numberReward + colorReward;
        }

        private bool HasSomeReward(NumberData numberData) =>
            HasWinNumber(numberData) || HasWinColor(numberData);

        private bool HasWinNumber(NumberData numberData) =>
            _betContext.BetDataHolder.Numbers.Contains(numberData);

        private bool HasWinColor(NumberData numberData) =>
            _betContext.BetDataHolder.Colors.Contains(numberData.ColorData);
    }
}