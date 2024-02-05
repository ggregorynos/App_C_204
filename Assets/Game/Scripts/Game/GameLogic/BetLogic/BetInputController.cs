using System;
using TMPro;
using UnityEngine;

namespace Game.Scripts.Game.GameLogic.BetLogic
{
    public class BetInputController : MonoBehaviour
    {
        [SerializeField] private BetContext _betContext;
        [SerializeField] private int _minimalBet = 5;
        [SerializeField] private TMP_Text _betTxt;

        private int _maxBet = 1000;

        public void Initialize()
        {
            UpdateBetTxt();
        }

        public void Plus()
        {
            SetLocalBet(_betContext.LocalBet + _minimalBet);
            UpdateBetTxt();
        }

        public void Minus()
        {
            SetLocalBet(_betContext.LocalBet - _minimalBet);
            UpdateBetTxt();
        }

        private void SetLocalBet(int newBet)
        {
            int clampBet = Math.Clamp(newBet, 0, _maxBet);
            if (_betContext.CanAddBet(clampBet))
                _betContext.SetLocalBet(clampBet);
        }

        private void UpdateBetTxt()
        {
            _betTxt.text = _betContext.LocalBet + "";
        }
    }
}