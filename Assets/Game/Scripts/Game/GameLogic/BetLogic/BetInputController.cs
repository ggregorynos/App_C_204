using System;
using System.Linq;
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
        [Space] [SerializeField] private int[] _betSteps = new int[0];

        [SerializeField] private int _minimalBet = 5;
        [SerializeField] private TMP_Text _betTxt;

        [SerializeField] private Button _minButton;
        [SerializeField] private Button _maxButton;
        [SerializeField] private Button _maxBetButton;

        private int _maxBet = 1000;

        public int MinimalBet => _minimalBet;

        public void Initialize()
        {
            SetLocalBet(_minimalBet);
            UpdateBetTxt();
            UpdateBtns();
        }

        public void MaxBet()
        {
            SetMaxBet();

            UpdateBetTxt();
            UpdateBtns();
        }

        public void Plus()
        {
            int currentStepIndex = CurrentStepIndex(_betContext.LocalBet);

            if (currentStepIndex < _betSteps.Length - 1)
            {
                var newLocalBet = _betSteps[currentStepIndex + 1];
                if (newLocalBet >= _playerDatabase.PlayerBalance)
                    SetMaxBet();
                else
                    SetLocalBet(_betSteps[currentStepIndex + 1]);
            }
            else
            {
                // int counts = _betContext.LocalBet / _betSteps[currentStepIndex] + 1;
                //
                // SetLocalBet(_betSteps[currentStepIndex] * counts);

                SetMaxBet();
            }

            UpdateBetTxt();
            UpdateBtns();
        }

        public void Minus()
        {
            int currentStepIndex = CurrentStepIndex(_betContext.LocalBet);

            if (currentStepIndex == 0)
                SetLocalBet(_minimalBet);
            else
            {
                if (_betContext.LocalBet > _betSteps[currentStepIndex])
                    SetLocalBet(_betSteps[currentStepIndex]);
                else
                    SetLocalBet(_betSteps[currentStepIndex - 1]);
            }

            UpdateBetTxt();
            UpdateBtns();
        }

        public void CheckLocalBet()
        {
            if (_betContext.LocalBet > _playerDatabase.PlayerBalance)
            {
                SetMaxBet();
            }
        }

        private void SetMaxBet()
        {
            int newLocalBet = _playerDatabase.PlayerBalance / _minimalBet;

            if (newLocalBet == 0)
                SetLocalBet(_minimalBet);
            else
                SetLocalBet(newLocalBet * _minimalBet);

            UpdateBtns();
            UpdateBetTxt();
        }

        public void EnableBetBtns(bool enable)
        {
            _minButton.interactable = enable;
            _maxButton.interactable = enable;
            _maxBetButton.interactable = enable;
            if (enable)
                UpdateBtns();
        }

        public void UpdateBtns()
        {
            _minButton.interactable = true;
            _maxButton.interactable = true;
            _maxBetButton.interactable = true;

            if (_betContext.LocalBet == _minimalBet)
                _minButton.interactable = false;

            if (_betContext.LocalBet == _playerDatabase.PlayerBalance)
                _maxButton.interactable = false;

            if (_betContext.LocalBet == _maxBet || _betContext.LocalBet + _minimalBet > _playerDatabase.PlayerBalance
                                                || !_betContext.CanAddBet(_betContext.LocalBet + _minimalBet))
            {
                _maxButton.interactable = false;
                _maxBetButton.interactable = false;
            }
        }

        private int CurrentStepIndex(int bet)
        {
            int currentStep = _betSteps.Where(s => s <= bet).Last();

            return Array.IndexOf(_betSteps, currentStep);
        }

        private void SetLocalBet(int newBet)
        {
            int clampBetByMax = Math.Clamp(newBet, _minimalBet, _maxBet);

            if (_betContext.CanAddBet(clampBetByMax))
                _betContext.SetLocalBet(clampBetByMax);
        }

        private void UpdateBetTxt() =>
            _betTxt.text = _betContext.LocalBet + "";
    }
}