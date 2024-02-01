using System;
using UnityEngine;

namespace CodeHub.OtherUtilities
{
    [CreateAssetMenu(fileName = "PlayerDatabase", menuName = "PlayerDatabase", order = 1)]
    public class PlayerDatabase : ScriptableObject
    {
        private const string playerBalanceAlias = "playerBalanceAlias";
        private const string playerMoneyAlias = "playerMoneyAlias";
        private const string playerScoreAlias = "playerScoreAlias";

        private const string hasEnableSoundAlias = "hasSoundAlias";
        private const string hasEnableMusicAlias = "hasMusicAlias";
        private const string volumeSoundAlias = "volumeSoundAlias";
        private const string volumeMusicAlias = "volumeMusicAlias";

        private const string ratingScoreAlias = "ratingScoreAlias";

        private const string skinStatusAlias = "skinStatusAlias";

        private const string currentSkinNumberAlias = "currentSkinNumberAlias";
        private const string bonusGameAlias = "bonusGameAlias";

        private const string hasEnemyPackAlias = "hasEnemyPackAlias";
        private const string recordTimeAlias = "recordTimeAlias";
        private const string recordCountAlias = "recordCountAlias";

        private int bonusCooldownHours = 24;

        public Action<int> OnPlayerBalanceChange;
        public Action<int> OnPlayerMoneyChange;
        public Action<int> OnPlayerRecordChange;

        public int PlayerMoney
        {
            get => PlayerPrefs.GetInt(playerMoneyAlias, 0);
            set
            {
                PlayerPrefs.SetInt(playerMoneyAlias, value);
                PlayerPrefs.Save();
                OnPlayerMoneyChange?.Invoke(value);
            }
        }

        public int PlayerBalance
        {
            get => PlayerPrefs.GetInt(playerBalanceAlias, 100);
            private set
            {
                PlayerPrefs.SetInt(playerBalanceAlias, value);
                PlayerPrefs.Save();
                OnPlayerBalanceChange?.Invoke(value);
            }
        }
        
        public int PlayerRecord
        {
            get => PlayerPrefs.GetInt(recordCountAlias, 0);
            set
            {
                PlayerPrefs.SetInt(recordCountAlias, value);
                PlayerPrefs.Save();
                OnPlayerRecordChange(value);
            }
        }

        public void IncreasePlayerBalance(int value)
        {
            PlayerBalance += value;
        }
        
        public void IncreasePlayerMoney(int value)
        {
            PlayerMoney += value;
        }

        public int PlayerScore
        {
            get => PlayerPrefs.GetInt(playerScoreAlias, 0);
            private set
            {
                PlayerPrefs.SetInt(playerScoreAlias, value);
                PlayerPrefs.Save();
            }
        }

        public void SetPlayerScore(int value)
        {
            PlayerScore = value;
        }

        public bool HasEnableSound
        {
            get => Convert.ToBoolean(PlayerPrefs.GetString(hasEnableSoundAlias, "True"));
            set
            {
                PlayerPrefs.SetString(hasEnableSoundAlias, value.ToString());
                PlayerPrefs.Save();
            }
        }

        public bool HasEnableMusic
        {
            get => Convert.ToBoolean(PlayerPrefs.GetString(hasEnableMusicAlias, "True"));
            set
            {
                PlayerPrefs.SetString(hasEnableMusicAlias, value.ToString());
                PlayerPrefs.Save();
            }
        }

        public int VolumeSound
        {
            get
            {
               return PlayerPrefs.GetInt(volumeSoundAlias, 4);
            } 
            set
            {
                PlayerPrefs.SetInt(volumeSoundAlias, value);
                PlayerPrefs.Save();
            }
        }

        public int VolumeMusic
        {
            get => PlayerPrefs.GetInt(volumeMusicAlias, 2);
            set
            {
                PlayerPrefs.SetInt(volumeMusicAlias, value);
                PlayerPrefs.Save();
            }
        }

        public int GetRatingScore(int playerNumber)
        {
            return PlayerPrefs.GetInt(ratingScoreAlias + playerNumber, 0);
        }

        public void SetRatingScore(int playerNumber, int value)
        {
            PlayerPrefs.SetInt(ratingScoreAlias + playerNumber, value);
            PlayerPrefs.Save();
        }

        public bool GetSkinStatus(int skinNumber)
        {
            if (skinNumber == 0) return true;
            return Convert.ToBoolean(PlayerPrefs.GetString(skinStatusAlias + skinNumber, "False"));
        }

        public void SetSkinStatus(int skinNumber, bool hasOpen)
        {
            PlayerPrefs.SetString(skinStatusAlias + skinNumber, hasOpen.ToString());
            PlayerPrefs.Save();
        }

        public int CurrentSkin
        {
            get => PlayerPrefs.GetInt(currentSkinNumberAlias, 0);
            set
            {
                PlayerPrefs.SetInt(currentSkinNumberAlias, value);
                PlayerPrefs.Save();
            }
        }

        public void BonusClaimed()
        {
            PlayerPrefs.SetString(bonusGameAlias, DateTime.Now.ToString());
            PlayerPrefs.Save();
        }

        public bool HasBonusGame()
        {
            if (PlayerPrefs.HasKey(bonusGameAlias))
            {
                string lastBonusDateStr = PlayerPrefs.GetString(bonusGameAlias);
                DateTime lastBonusDate = DateTime.Parse(lastBonusDateStr);

                // Перевірка часу
                TimeSpan timeSinceLastBonus = DateTime.Now - lastBonusDate;
                if (timeSinceLastBonus.TotalHours >= bonusCooldownHours)
                {
                    // Бонус доступний
                    return true;
                }
            }

            else
            {
                // Бонус доступний (гравець ще не отримував бонус)
                return true;
            }

            // Бонус недоступний
            return false;
        }
        
        public bool HasEnemyPack
        {
            get => Convert.ToBoolean(PlayerPrefs.GetString(hasEnemyPackAlias, "False"));
            set
            {
                PlayerPrefs.SetString(hasEnemyPackAlias, value.ToString());
                PlayerPrefs.Save();
            }
        }
        
        public float RecordTime
        {
            get => PlayerPrefs.GetFloat(recordTimeAlias, 0);
            set
            {
                PlayerPrefs.SetFloat(recordTimeAlias, value);
                PlayerPrefs.Save();
            }
        }
    }
}