using UnityEngine;
using UnityEngine.UI;

namespace GameLogic
{
    public class SettingsButton: MonoBehaviour
    {
        [SerializeField] private Button _buttonOn;
        [SerializeField] private Button _buttonOff;

        public void SetEnable(bool enable)
        {
            _buttonOn.gameObject.SetActive(enable);
            _buttonOff.gameObject.SetActive(!enable);
        }
    }
}