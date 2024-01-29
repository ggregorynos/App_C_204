using TMPro;
using UnityEngine;

namespace Game.Scripts.Game.DailyBonus
{
    public class DailyBonusUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text coinTxt;

        private int _day;
        private RewardData _rewardData;

        public RewardData RewardData => _rewardData;

        public void SetRewardData(RewardData reward)
        {
            _rewardData = reward;
        }

        public void UpdateRewardsTxt()
        {
            coinTxt.gameObject.SetActive(true);
            coinTxt.text = _rewardData.CoinReward + "";
        }
    }
}