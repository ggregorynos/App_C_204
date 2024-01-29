using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace CodeHub.OtherUtilities
{
    public class PlayerRecordsUpdater : MonoBehaviour
    {
        [SerializeField] private PlayerDatabase _playerDatabase;
        [SerializeField] private List<TMP_Text> _playerRecordsTxt;

        public void UpdatePlayerRecordsTxt()
        {
            foreach (var playerRecordTxt in _playerRecordsTxt)
            {
                if (playerRecordTxt != null) playerRecordTxt.text = _playerDatabase.PlayerScore + "";
            }
        }
    }
}
