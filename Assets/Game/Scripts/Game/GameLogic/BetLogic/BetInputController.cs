using System;
using CodeHub.GameMechanics;
using CodeHub.OtherUtilities;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.Game.GameLogic.BetLogic
{
    public class BetInputController : MonoBehaviour
    {
        [SerializeField] private PlayerDatabase _playerDatabase;
        [SerializeField] private BetContext _betContext;
        [SerializeField] private int _minimalBet = 5;
        [SerializeField] private TMP_Text _betTxt;

        [SerializeField] private Button _minButton;
        [SerializeField] private Button _maxButton;

        private int _maxBet = 1000;

        public int MinimalBet => _minimalBet;

        public void Initialize()
        {
            SetLocalBet(_minimalBet);
            UpdateBetTxt();
            UpdateBtns();
        }

        public void Plus()
        {
            SetLocalBet(_betContext.LocalBet + _minimalBet);
            UpdateBetTxt();
            UpdateBtns();
        }

        public void Minus()
        {
            SetLocalBet(_betContext.LocalBet - _minimalBet);
            UpdateBetTxt();
            UpdateBtns();
        }

        public void CheckLocalBet()
        {
            if (_betContext.LocalBet > _playerDatabase.PlayerBalance)
            {
                int newLocalBet = _playerDatabase.PlayerBalance / _minimalBet;
                
                if (newLocalBet == 0)
                    SetLocalBet(_minimalBet);
                else
                    SetLocalBet(newLocalBet * _minimalBet);
                
                UpdateBtns();
                UpdateBetTxt();
            }
        }

        public void UpdateBtns()
        {
            _minButton.interactable = true;
            _maxButton.interactable = true;
            if (_betContext.LocalBet == _minimalBet)
            {
                _minButton.interactable = false;
            }

            if (_betContext.LocalBet == _maxBet || _betContext.LocalBet + _minimalBet > _playerDatabase.PlayerBalance
                                                ||!_betContext.CanAddBet(_betContext.LocalBet+_minimalBet))
            {
                _maxButton.interactable = false;
            }
        }

        private void SetLocalBet(int newBet)
        {
            int clampBetByMax = Math.Clamp(newBet, _minimalBet, _maxBet);

            if (_betContext.CanAddBet(clampBetByMax))
                _betContext.SetLocalBet(clampBetByMax);
        }

        private void UpdateBetTxt()
        {
            _betTxt.text = _betContext.LocalBet + "";
        }
    }
}