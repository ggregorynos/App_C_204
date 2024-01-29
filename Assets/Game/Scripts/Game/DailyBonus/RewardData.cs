using UnityEngine;

namespace Game.Scripts.Game.DailyBonus
{
    [CreateAssetMenu(fileName = "DailyRewardData", menuName = "DailyRewardData", order = 1)]
    public class RewardData : ScriptableObject
    {
        [SerializeField] private int _coinReward;

        public int CoinReward => _coinReward;
    }
}