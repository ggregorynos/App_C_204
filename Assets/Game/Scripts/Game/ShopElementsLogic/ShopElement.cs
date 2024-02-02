using System;
using Tools.UnityAdsService.Scripts;
using UnityEngine;

namespace Game.Scripts.Game.ShopElementsLogic
{
    public class ShopElement : MonoBehaviour
    {
        [SerializeField] private GameObject _activeAdsBtn;
        [SerializeField] private GameObject _nonActiveAdsBtn;
        [SerializeField] private UnityAdsButton _adsButton;
        [field: SerializeField] public int RewardValue;

        public Action<ShopElement> GetReward;

        private void Start()
        {
            _adsButton.OnCanGetReward += InvokeGetReward;
        }

        private void OnDestroy()
        {
            _adsButton.OnCanGetReward -= InvokeGetReward;
        }

        public void EnableAdsBtn(bool enable)
        {
            _activeAdsBtn.gameObject.SetActive(enable);
            _nonActiveAdsBtn.gameObject.SetActive(!enable);
        }

        public void InvokeGetReward()
        {
            GetReward?.Invoke(this);
        }
    }
}