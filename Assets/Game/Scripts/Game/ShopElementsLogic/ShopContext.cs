using System;
using System.Collections.Generic;
using CodeHub.OtherUtilities;
using UnityEngine;

namespace Game.Scripts.Game.ShopElementsLogic
{
    public class ShopContext : MonoBehaviour
    {
        [SerializeField] private PlayerDatabase _playerDatabase;
        [SerializeField] private List<ShopElement> _elements;
        private const string CurrentAdsAlias = "CurrentAdsAlias";

        private void Start()
        {
            UpdateAdsBtns();
            foreach (var element in _elements)
            {
                element.GetReward += GetReward;
            }
        }

        private void OnDestroy()
        {
            foreach (var element in _elements)
            {
                element.GetReward -= GetReward;
            }
        }

        private void GetReward(ShopElement shopElement)
        {
            _playerDatabase.IncreasePlayerBalance(shopElement.RewardValue);
            CurrentAdsIndex++;
            UpdateAdsBtns();
        }

        private void UpdateAdsBtns()
        {
            foreach (var element in _elements)
            {
                element.EnableAdsBtn(false);
            }
            
            _elements[CurrentAdsIndex].EnableAdsBtn(true);
        }

        private int CurrentAdsIndex
        {
            get => PlayerPrefs.GetInt(CurrentAdsAlias, 0);
            set
            {
                var clampValue= Math.Clamp(value, 0, _elements.Count - 1);
                PlayerPrefs.SetInt(CurrentAdsAlias, clampValue);
                PlayerPrefs.Save();
            }
        }
    }
}
