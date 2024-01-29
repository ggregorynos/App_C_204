using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CodeHub.OtherUtilities
{
    public class LocalPlayerInputController : MonoBehaviour
    {
        [SerializeField] private List<Button> _buttons;
        [SerializeField] private List<GameObject> overlayClickBlockers;

        public void SetEnableButtons(bool enable)
        {
            foreach (var button in _buttons)
            {
                button.interactable = enable;
            }
        }

        public void SetEnableOverlayClickBlockers(bool enable)
        {
            foreach (var clickBlocker in overlayClickBlockers)
            {
                clickBlocker.gameObject.SetActive(enable);
            }
        }
    }
}
