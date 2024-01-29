using System;
using UnityEngine;

namespace Game.Scripts.Game.ShopLogic
{
    [CreateAssetMenu(fileName = "ShopElementData", menuName = "ShopElementData", order = 1)]
    public class ShopElementData : ScriptableObject
    {
        [SerializeField] private int _price;

        private string _hasOpenAlias = "hasOpenAlias";

        public int Price => _price;

        public bool HasOpen
        {
            get => Convert.ToBoolean(PlayerPrefs.GetString(_hasOpenAlias + name, "False"));
            set
            {
                PlayerPrefs.SetString(_hasOpenAlias + name, value.ToString());
                PlayerPrefs.Save();
            }
        }
    }
}