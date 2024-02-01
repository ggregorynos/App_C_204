using UnityEngine;

namespace Game.Scripts.Game.DailyBonus
{
    [CreateAssetMenu(fileName = "DailyRewardData", menuName = "DailyRewardData", order = 1)]
    public class RewardData : ScriptableObject
    {
        [SerializeField] private int _xReward;

        public int Xreward => _xReward;
    }
}