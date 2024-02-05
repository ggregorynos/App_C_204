using System;
using UnityEngine;

namespace Game.Scripts.Game.GameLogic.BetLogic
{
    public abstract class BaseBetButton : MonoBehaviour
    {
        public Action<BaseBetButton> OnClick;
        
        [SerializeField] private GameObject _coin;

        public bool HasActive { get; private set; }

        public void ActivateButton(bool active)
        {
            HasActive = active;
            _coin.gameObject.SetActive(active);
        }

        public void ClickOnButton()
        {
            OnClick?.Invoke(this);
        }
    }
}