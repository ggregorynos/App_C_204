using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.Game.ShopLogic
{
    [CreateAssetMenu(fileName = "ShopElementContextData", menuName = "ShopElementContextData", order = 1)]
    public class ShopElementContextData : ScriptableObject
    {
        [SerializeField] private List<ShopElementData> _elementsData;

        private string _currentElementAlias = "currentElementAlias";

        public ShopElementData GetCurrentElement()
        {
            var currentHook = _elementsData[CurrentElementIndex];
            return currentHook;
        }

        public void SetCurrentElement(ShopElementData hookData)
        {
            int index = _elementsData.IndexOf(hookData);
            CurrentElementIndex = index;
        }

        private int CurrentElementIndex
        {
            get => PlayerPrefs.GetInt(_currentElementAlias, 0);
            set
            {
                PlayerPrefs.SetInt(_currentElementAlias, value);
                PlayerPrefs.Save();
            }
        }
    }
}