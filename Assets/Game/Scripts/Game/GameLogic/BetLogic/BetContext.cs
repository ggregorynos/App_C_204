using System;
using System.Collections.Generic;
using CodeHub.OtherUtilities;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.Game.GameLogic.BetLogic
{
    public class BetContext : MonoBehaviour
    {
        [SerializeField] private BetInputController _betInputController;
        [SerializeField] private TMP_Text _winTxt;
        [SerializeField] private Button _playBtn;
        [SerializeField] private List<BaseBetButton> _betButtons;
        [SerializeField] private PlayerDatabase _playerDatabase;
        public int LocalBet { get; private set; }
        public BetDataHolder BetDataHolder { get; private set; }

        private int _winCount;

        public Action OnZeroBet;
        public Action OnLessMoney;

        public void Initialize()
        {
            BetDataHolder = new BetDataHolder();
            LocalBet = 0;
            _winCount = 0;
            UpdateWinTxt();
            UpdatePlayBtnStatus();
            InitializeBetButtons();
        }

        public void ClickOnBetButton(BaseBetButton button)
        {
            if (button.HasActive)
            {
                RemoveBetData(button);
                UpdatePlayBtnStatus();
                _betInputController.UpdateBtns();
                return;
            }

            if (CurrentBetZero())
            {
                OnZeroBet?.Invoke();
                return;
            }

            if (NotHasMoneyToAddNewBet())
            {
                OnLessMoney?.Invoke();
                return;
            }

            AddBetData(button);
            UpdatePlayBtnStatus();
            _betInputController.UpdateBtns();
        }

        public void ResetField()
        {
            BetDataHolder.Reset();
            foreach (var betButton in _betButtons)
            {
                betButton.ActivateButton(false);
            }
        }

        public void SetLocalBet(int bet) =>
            LocalBet = bet;

        public bool CanAddBet(int bet) =>
            GetBetCount(bet) <= _playerDatabase.PlayerBalance;

        public void IncreaseWinCount(int plusValue)
        {
            _winCount += plusValue;
            UpdateWinTxt();
        }

        public void UpdatePlayBtnStatus() =>
            _playBtn.interactable = !(BetDataHolder.Colors.Count == 0 && BetDataHolder.Numbers.Count == 0);

        private void AddBetData(BaseBetButton baseBetButton)
        {
            if (baseBetButton is ColorBetButton colorBetButton)
                BetDataHolder.AddColor(colorBetButton.ColorData);

            if (baseBetButton is NumberBetButton numberBetButton)
                BetDataHolder.AddNumber(numberBetButton.NumberData);

            baseBetButton.ActivateButton(true);
        }

        private void RemoveBetData(BaseBetButton baseBetButton)
        {
            if (baseBetButton is ColorBetButton colorBetButton)
                BetDataHolder.RemoveColor(colorBetButton.ColorData);

            if (baseBetButton is NumberBetButton numberBetButton)
                BetDataHolder.RemoveNumber(numberBetButton.NumberData);

            baseBetButton.ActivateButton(false);
        }

        public int GetAllBetCount() =>
            (BetDataHolder.Colors.Count + BetDataHolder.Numbers.Count) * LocalBet;

        private int GetBetCount(int localBet) =>
            (BetDataHolder.Colors.Count + BetDataHolder.Numbers.Count) * localBet;

        private bool CurrentBetZero() =>
            LocalBet == 0;

        private bool NotHasMoneyToAddNewBet() =>
            GetAllBetCount() + LocalBet > _playerDatabase.PlayerBalance;

        private void InitializeBetButtons()
        {
            foreach (var betButton in _betButtons)
            {
                betButton.OnClick += ClickOnBetButton;
            }
        }

        private void OnDestroy()
        {
            foreach (var betButton in _betButtons)
            {
                betButton.OnClick -= ClickOnBetButton;
            }
        }

        private void UpdateWinTxt() =>
            _winTxt.text = _winCount + "";
    }
}