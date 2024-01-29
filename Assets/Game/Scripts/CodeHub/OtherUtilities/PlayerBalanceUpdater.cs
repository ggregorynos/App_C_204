using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace CodeHub.OtherUtilities
{
    public class PlayerBalanceUpdater : MonoBehaviour
    {
        [SerializeField] private PlayerDatabase _playerDatabase;
        [SerializeField] private List<TMP_Text> _playerBalanceTxt;

        private bool _hasSprite;

        public void Initialize()
        {
            _playerDatabase.OnPlayerBalanceChange += UpdatePlayerBalanceTxtByValue;
            CheckSprite();
        }

        private void CheckSprite()
        {
            foreach (var playerBalanceTxt in _playerBalanceTxt)
            {
                if (playerBalanceTxt != null && playerBalanceTxt.text.Contains("<sprite=0>"))
                {
                    _hasSprite = true;
                    break;
                }
            }
        }

        public void UpdatePlayerBalanceTxt()
        {
            foreach (var playerBalanceTxt in _playerBalanceTxt)
            {
                if (playerBalanceTxt != null)
                {
                    if (_hasSprite)
                    {
                        playerBalanceTxt.text = "<sprite=0>" + _playerDatabase.PlayerBalance;
                    }
                    else
                    {
                        playerBalanceTxt.text = _playerDatabase.PlayerBalance.ToString();
                    }
                }
            }
        }

        private void UpdatePlayerBalanceTxtByValue(int value)
        {
            foreach (var playerBalanceTxt in _playerBalanceTxt)
            {
                if (playerBalanceTxt != null)
                {
                    if (_hasSprite)
                    {
                        playerBalanceTxt.text = "<sprite=0>" + value;
                    }
                    else
                    {
                        playerBalanceTxt.text = value.ToString();
                    }
                }
            }
        }
    }
}