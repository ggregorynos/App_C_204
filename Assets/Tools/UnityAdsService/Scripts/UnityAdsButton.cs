using System;
using UnityEngine;
using UnityEngine.UI;

namespace Tools.UnityAdsService.Scripts
{
    [RequireComponent(typeof(Button))]
    public class UnityAdsButton : MonoBehaviour
    {
        private UnityAdsService UnityAdsService => UnityAdsService.Instance;
        private Button WatchButton => GetComponent<Button>();
        
        public event Action OnCanGetReward;

        private void Start()
        {
            Deactivate();
            
            if (UnityAdsService.IsInitialize)
            {
                Initialize();
            }
            else
            {
                UnityAdsService.OnInitialize += Initialize;
            }
        }

        private void OnDestroy()
        {
            UnsubscribeAllEvent();
            
            UnityAdsService.OnInitialize -= Initialize;
        }

        public void Deactivate()
        {
            WatchButton.interactable = false;
        }
        
        public void Activate()
        {
            WatchButton.interactable = true;
        }

        private void Initialize()
        {
            Activate();
            
            WatchButton.onClick.AddListener(ShowRewardedAd);
        }
        
        private void ShowRewardedAd()
        {
            var listener = UnityAdsService.ShowRewardedAd();
            
            if (listener != null)
            {
                listener.OnShowCompleteAds += NotifyGetRewarded;
            }
        }

        private void NotifyGetRewarded()
        {
            OnCanGetReward?.Invoke();
        }

        private void UnsubscribeAllEvent()
        {
            OnCanGetReward = null;
        }
    }
}