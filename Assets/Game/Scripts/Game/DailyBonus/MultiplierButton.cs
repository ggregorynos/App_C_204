using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.Game.DailyBonus
{
    public class MultiplierButton : MonoBehaviour
    {
        [field: SerializeField] public RewardData RewardData;
        [field: SerializeField] public Image Image;

        [SerializeField] private Sprite _active;
        [SerializeField] private Sprite _nonActive;

        public void EnableBtn()
        {
            Image.sprite = _active;
        }

        public void DisableBtn()
        {
            Image.sprite = _nonActive;
        }
    }
}