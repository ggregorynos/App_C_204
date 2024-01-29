using System.Collections.Generic;
using CodeHub.OtherUtilities;
using TMPro;
using UnityEngine;

namespace Game.Scripts.Game.Menu
{
    public class CoinPlayerBalanceUpdater : MonoBehaviour
    {
        [SerializeField] private PlayerDatabase _playerDatabase;
        [SerializeField] private List<TMP_Text> _playerBalanceTxt;

        public void Initialize()
        {
            _playerDatabase.OnPlayerBalanceChange += UpdatePlayerBalanceTxtByValue;
            UpdatePlayerBalanceTxtByValue(_playerDatabase.PlayerBalance);
        }

        private void UpdatePlayerBalanceTxtByValue(int value)
        {
            foreach (var playerBalanceTxt in _playerBalanceTxt)
            {
                if (playerBalanceTxt != null)
                {
                    playerBalanceTxt.text = "<sprite=0>" + value + "<sprite=1>";
                }
            }
        }
    }
}