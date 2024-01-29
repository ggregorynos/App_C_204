using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.Game.ShopLogic
{
    public class ElementShopBtn : MonoBehaviour
    {
        [SerializeField] private ShopElementData _elementData;

        [SerializeField] private Button _buyBtn;
        [SerializeField] private Button _selectBtn;
        
        [SerializeField] private GameObject _selectMark;
        
        public ShopElementData ElementData => _elementData;
        public Action<ElementShopBtn> onBuy;
        public Action<ElementShopBtn> onSelect;

        public void UpdateUI()
        {
            if (_elementData.HasOpen == false)
            {
                _buyBtn.gameObject.SetActive(true);
                _selectBtn.gameObject.SetActive(false);
            }
            else
            {
                _buyBtn.gameObject.SetActive(false);
                _selectBtn.gameObject.SetActive(true);
            }
            
            _selectMark.SetActive(false);
        }

        public void Select()
        {
            _buyBtn.gameObject.SetActive(false);
            _selectBtn.gameObject.SetActive(false);
            _selectMark.SetActive(true);
        }

        public void BuyEventsInvoke()
        {
            onBuy?.Invoke(this);
        }

        public void SelectEventsInvoke()
        {
            onSelect?.Invoke(this);
        }
    }
}