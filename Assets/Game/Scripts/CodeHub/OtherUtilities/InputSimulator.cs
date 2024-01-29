using UnityEngine;

namespace CodeHub.OtherUtilities
{
    public static class InputSimulator
    {
        public static void SimulateKeyDown(KeyCode key)
        {
            Input.GetKeyDown(key);
        }
    }
}