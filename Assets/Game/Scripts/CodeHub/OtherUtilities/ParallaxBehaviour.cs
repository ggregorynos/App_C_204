using UnityEngine;

namespace CodeHub.OtherUtilities
{
    public class ParallaxBehaviour : MonoBehaviour
    {
        [SerializeField] private Transform followingTarget;
        [SerializeField, Range(0f, 1f)] private float parallaxStrength = 0.1f;
        [SerializeField] private bool disableVerticalParallax;
        private Vector3 _targetPreviousPosition;

        private void Start()
        {
            if (!followingTarget)
                followingTarget = Camera.main.transform;
        
            _targetPreviousPosition = followingTarget.position;
        }

        private void FixedUpdate()
        {
            var delta = followingTarget.position - _targetPreviousPosition;
            
            if (disableVerticalParallax)
                delta.y = 0;

            _targetPreviousPosition = followingTarget.position;

            var targetPosition = transform.position + delta * parallaxStrength;
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.fixedDeltaTime * 10f);
        }
    }
}
