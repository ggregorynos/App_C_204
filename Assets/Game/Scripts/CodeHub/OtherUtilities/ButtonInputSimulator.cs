using UnityEngine;
using UnityEngine.UI;

namespace CodeHub.OtherUtilities
{
    [RequireComponent(typeof(Button))]
    public class ButtonInputSimulator : MonoBehaviour
    {
        [SerializeField] private KeyCode _keyCode;
        private Button _button;

        private void Start()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(SimulateKeyDown);
        }

        private void SimulateKeyDown()
        {
            InputSimulator.SimulateKeyDown(_keyCode);
        }
    }
}