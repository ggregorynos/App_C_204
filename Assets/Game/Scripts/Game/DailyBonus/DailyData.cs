using System;
using UnityEngine;

namespace Game.Scripts.Game.DailyBonus
{
    public class DailyData
    {
        private const string _bonusClaimedDayYearAlias = "bonusClaimedDayYearAlias";
        private const string _currentDayAlias = "currentDailyBonusAlias";
        
        public int CurrentClaimDay
        {
            get => PlayerPrefs.GetInt(_currentDayAlias, 0);
            set
            {
                PlayerPrefs.SetInt(_currentDayAlias, value);
                PlayerPrefs.Save();
            }
        }

        public bool HasDailyBonus()
        {
            int currentDay = GetCurrentDayOfYear();
            return currentDay != BonusClaimedDay;
        }

        public bool HasUpgradeDailyReward()
        {
            int currentDay = GetCurrentDayOfYear();
            if (currentDay == 1)
            {
                BonusClaimedDay = 0;
            }

            return BonusClaimedDay + 1 == currentDay;
        }
        
        public void DailyBonusClaim()
        {
            BonusClaimedDay = GetCurrentDayOfYear();
        }

        public void IncreaseDailyBonusClaim(int value)
        {
            BonusClaimedDay -= value;
        }
        
        private int BonusClaimedDay
        {
            get => PlayerPrefs.GetInt(_bonusClaimedDayYearAlias, 0);
            set
            {
                PlayerPrefs.SetInt(_bonusClaimedDayYearAlias, value);
                PlayerPrefs.Save();
            }
        }

        private int GetCurrentDayOfYear()
        {
            return DateTime.Now.DayOfYear;
        }
    }
}