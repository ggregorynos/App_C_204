using System;
using System.Collections.Generic;
using CodeHub.OtherUtilities;
using Game.Scripts.Game.GameLogic.GameData;
using UnityEngine;

namespace Game.Scripts.Game.GameLogic.BetLogic
{
    public class BetContext : MonoBehaviour
    {
        [SerializeField] private List<BaseBetButton> _betButtons;
        [SerializeField] private PlayerDatabase _playerDatabase;
        public int LocalBet { get; private set; }
        public BetDataHolder BetDataHolder { get; private set; }

        public Action OnZeroBet;
        public Action OnLessMoney;

        public void Initialize()
        {
            BetDataHolder = new BetDataHolder();
            LocalBet = 0;
            InitializeBetButtons();
        }

        public void ClickOnBetButton(BaseBetButton button)
        {
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
        }

        public void SetLocalBet(int bet) =>
            LocalBet = bet;

        public bool CanAddBet(int bet) => 
            GetBetCount(bet)<=_playerDatabase.PlayerBalance;

        private void AddBetData(BaseBetButton baseBetButton)
        {
            if (baseBetButton is ColorBetButton colorBetButton)
                BetDataHolder.AddColor(colorBetButton.ColorData);

            if (baseBetButton is NumberBetButton numberBetButton)
                BetDataHolder.AddNumber(numberBetButton.NumberData);
        }

        private int GetAllBetCount() =>
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
    }
}