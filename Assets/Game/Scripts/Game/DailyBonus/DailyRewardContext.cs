using System.Collections.Generic;
using CodeHub.OtherUtilities;
using Game.Mephistoss.PanelMachine.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.Game.DailyBonus
{
    public class DailyRewardContext : MonoBehaviour
    {
        [SerializeField] private PlayerDatabase _playerDatabase;
        [SerializeField] private PanelMachine _panelMachine;
        [SerializeField] private PanelBase _dailyPanel;
        [SerializeField] private Button _dailyBtn;

        [SerializeField] private DailyBonusUI _dailyBonus;
        [SerializeField] private List<RewardData> _rewards;
        [SerializeField] private RewardData _maxReward;

        [SerializeField] private AudioSource _dailyRewardAudio;

        private DailyData _dailyData;

        public void Initialize()
        {
            _dailyData = new DailyData();
            CheckDailyBtnStatus();
        }

        public void AddClaimReward()
        {
            CheckDisableUpgradeRewards();

            var claimDay = _dailyData.CurrentClaimDay;
            var currentDay = claimDay + 1;
            UpdateBonusDailyRewardData(claimDay);
            GetReward(currentDay);

            _panelMachine.AddPanel(_dailyPanel);
            _dailyRewardAudio.Play();
            CheckDailyBtnStatus();
        }

        private void CheckDailyBtnStatus()
        {
            _dailyBtn.interactable = _dailyData.HasDailyBonus();
        }

        private void CheckDisableUpgradeRewards()
        {
            if (!_dailyData.HasUpgradeDailyReward())
            {
                _dailyData.CurrentClaimDay = 0;
            }
        }

        [ContextMenu("Test/GetReward")]
        private void IncreaseDay()
        {
            _dailyData.IncreaseDailyBonusClaim(1);
        }

        private void GetReward(int day)
        {
            var reward = _dailyBonus.RewardData;
            _playerDatabase.IncreasePlayerBalance(reward.CoinReward);
            _dailyData.DailyBonusClaim();
            _dailyData.CurrentClaimDay = day;
        }

        private void UpdateBonusDailyRewardData(int claimedDay)
        {
            if (claimedDay < 7)
            {
                _dailyBonus.SetRewardData(_rewards[claimedDay]);
            }
            else
            {
                _dailyBonus.SetRewardData(_maxReward);
            }

            _dailyBonus.UpdateRewardsTxt();
        }
    }
}