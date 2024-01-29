using DG.Tweening;
using UnityEngine;

namespace CodeHub.OtherUtilities
{
    public class CircleAnimation : MonoBehaviour
    {
        [SerializeField] private float duration;
        void Start()
        {
            gameObject.transform.DORotate(new Vector3(0f, 0f, -360f), duration, RotateMode.FastBeyond360)
                .SetEase(Ease.OutQuint).SetLoops(-1);
        }
    }
}
