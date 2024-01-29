using System;
using System.Collections;
using UnityEngine;

namespace CodeHub.GameMechanics
{
    public static class Utilities
    {
        public static void Invoke(this MonoBehaviour monoBehaviour, Action action, float delay)
        {
            monoBehaviour.StartCoroutine(InvokeRoutine(action, delay));
        }

        private static IEnumerator InvokeRoutine(Action action, float delay)
        {
            yield return new WaitForSeconds(delay);
            action?.Invoke();
        }
    }
}