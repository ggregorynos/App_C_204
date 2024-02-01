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

        private void Start()
        {
            _playerDatabase.OnPlayerBalanceChange += UpdatePlayerBalanceTxt;
            UpdatePlayerBalanceTxt(0);
        }

        public void UpdatePlayerBalanceTxt(int value)
        {
            foreach (var playerBalanceTxt in _playerBalanceTxt)
            {
                if (playerBalanceTxt != null)
                {
                    playerBalanceTxt.text = _playerDatabase.PlayerBalance + "";
                }
            }
        }
    }
}