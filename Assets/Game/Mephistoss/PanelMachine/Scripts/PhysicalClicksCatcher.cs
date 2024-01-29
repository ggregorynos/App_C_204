using UnityEngine;
using UnityEngine.Events;

namespace Game.Mephistoss.PanelMachine.Scripts
{
    public class PhysicalClicksCatcher : MonoBehaviour
    {
        [SerializeField] private PanelMachine panelMachine;
        public UnityEvent<KeysPressed> onKeyPressed;
        private const string escape = "escape";

        public bool canEscape= true;

        private void Update()
        {
            if (Input.GetKeyUp(escape)&&canEscape)
            {
                onKeyPressed.Invoke(KeysPressed.escape);
                panelMachine.ReactToCloseAction();
                
                Debug.Log("Escape");
            }
        }
    }
}