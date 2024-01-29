using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace CodeHub.OtherUtilities
{
    public class ProgressBarPreloaderAnimation : MonoBehaviour
    {
        [SerializeField] private Image[] objects;
        [SerializeField] private Sprite _acitve;
        [SerializeField] private Sprite _nonActive;

        [SerializeField] private float _duration;

        private void Start()
        {
            StartCoroutine(ActivateObjects());
        }

        private IEnumerator ActivateObjects()
        {
            while (true)
            {
                // active
                foreach (var t in objects)
                {
                    t.sprite= _acitve;
                    yield return new WaitForSeconds(_duration);
                }

                // disable all
                foreach (var t in objects)
                {
                    t.sprite= _nonActive;
                }
            }
        }
    }
}
