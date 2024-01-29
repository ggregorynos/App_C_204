using UnityEngine;
using UnityEngine.Events;

namespace CodeHub.OtherUtilities
{
    public class VolumeSegmentsController : MonoBehaviour
    {
        [SerializeField] private SegmentsActivator _segmentsActivator;
        [SerializeField] private UnityEvent OnValueChanged;

        public void Plus()
        {
            _segmentsActivator.ActiveElements(_segmentsActivator.GetActiveCount()+1);
            OnValueChanged.Invoke();
        }

        public void Minus()
        {
            _segmentsActivator.ActiveElements(_segmentsActivator.GetActiveCount()-1);
            OnValueChanged.Invoke();

        }
    }
}