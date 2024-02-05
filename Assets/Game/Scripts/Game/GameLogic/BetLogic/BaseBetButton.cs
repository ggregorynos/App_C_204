using System;
using UnityEngine;

namespace Game.Scripts.Game.GameLogic.BetLogic
{
    public abstract class BaseBetButton : MonoBehaviour
    {
        public Action<BaseBetButton> OnClick;

        public void ClickOnButton()
        {
            OnClick?.Invoke(this);
        }
    }
}