using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeHub.OtherUtilities;
using CodeHub.WheelSpinLogic;
using DG.Tweening;
using Game.Mephistoss.PanelMachine.Scripts;
using TMPro;
using UnityEngine;

namespace Game.Scripts.Game.DailyBonus
{
    public class DailyRewardContext : MonoBehaviour
    {
        [SerializeField] private float _startDelay = 0.75f;
        [SerializeField] private PlayerDatabase _playerDatabase;
        [SerializeField] private PanelMachine _panelMachine;
        [SerializeField] private PanelBase _dailyPanel;
        [SerializeField] private PanelBase _rewardPanel;

        [SerializeField] private List<RewardData> _rewards;
        [SerializeField] private List<MultiplierButton> _multiplierButtons;
        [SerializeField] private RewardData _maxReward;
        [SerializeField] private WheelSpin _wheelSpin;
        [SerializeField] private TMP_Text _rewardTxt;

        [SerializeField] private AudioSource _startDaily;
        [SerializeField] private AudioSource _spin;
        [SerializeField] private AudioSource _getReward;

        private DailyData _dailyData;
        private RewardData _currentRewardData;

        private void Start()
        {
            _dailyData = new DailyData();
            CheckDailyPanelActive();
        }

        public void Collect()
        {
            _panelMachine.CloseLastPanel();
            _panelMachine.CloseLastPanel();
        }

        public void AddClaimReward()
        {
            CheckDisableUpgradeRewards();

            var claimDay = _dailyData.CurrentClaimDay;
            var currentDay = claimDay + 1;
            UpdateBonusDailyRewardData(claimDay);
            RewardClaimed(currentDay);

            _panelMachine.AddPanel(_dailyPanel);
            _startDaily.Play();

            DOVirtual.DelayedCall(_startDelay, () => { TrySpin(); });
        }

        private void CheckDailyPanelActive()
        {
            if (_dailyData.HasDailyBonus())
                AddClaimReward();
        }

        private void CheckDisableUpgradeRewards()
        {
            if (!_dailyData.HasUpgradeDailyReward())
                _dailyData.CurrentClaimDay = 0;
        }

        [ContextMenu("Test/GetReward")]
        private void IncreaseDay() =>
            _dailyData.IncreaseDailyBonusClaim(1);

        private void RewardClaimed(int day)
        {
            _dailyData.DailyBonusClaim();
            _dailyData.CurrentClaimDay = day;
        }

        private void UpdateBonusDailyRewardData(int claimedDay)
        {
            if (claimedDay < 7)
                _currentRewardData = _rewards[claimedDay];
            else
                _currentRewardData = _maxReward;

            EnableCurrentMultiplier(_currentRewardData);
        }

        private async Task TrySpin()
        {
            _spin.Play();
            var sector = await _wheelSpin.TrySpin();
            _spin.Stop();

            _panelMachine.AddPanel(_rewardPanel);
            _getReward.Play();


            if (_currentRewardData == _rewards.FirstOrDefault())
                _rewardTxt.text = sector.Value + "";
            else
                _rewardTxt.text = sector.Value.ToString("# ##0") + " x" + _currentRewardData.Xreward;

            _playerDatabase.IncreasePlayerBalance(sector.Value * _currentRewardData.Xreward);
        }

        private void EnableCurrentMultiplier(RewardData rewardData)
        {
            DisableAllxElements();
            EnableXVisualize(rewardData);
        }

        private void EnableXVisualize(RewardData rewardData)
        {
            var element = _multiplierButtons.Where((button => button.RewardData == rewardData)).FirstOrDefault();
            if (element != null)
                element.EnableBtn();
        }

        private void DisableAllxElements()
        {
            foreach (var element in _multiplierButtons)
            {
                element.DisableBtn();
            }
        }
    }
}