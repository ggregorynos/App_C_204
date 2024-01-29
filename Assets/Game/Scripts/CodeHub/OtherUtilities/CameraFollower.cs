using UnityEngine;

namespace CodeHub.OtherUtilities
{
    public class CameraFollower : MonoBehaviour
    {
        private GameObject _target;
        private bool _canFollow;

        public void SetTarget(GameObject target)
        {
            _target = target;
        }

        public void EnableFollow(bool enable)
        {
            _canFollow = enable;
        }

        private void LateUpdate()
        {
            if (_target == null || !_canFollow) return;

            if (transform.position.y > 0)
            {
                transform.position = Vector3.zero;

                return;
            }

            if (_target.transform.position.y > 0) return;

            Vector3 newPos = new Vector3(transform.position.x, _target.transform.position.y,
                transform.position.z);
            transform.position = newPos;
        }
    }
}