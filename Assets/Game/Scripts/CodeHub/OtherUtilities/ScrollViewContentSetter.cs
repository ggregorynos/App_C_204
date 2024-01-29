using UnityEngine;

namespace CodeHub.OtherUtilities
{
    public class ScrollViewContentSetter : MonoBehaviour
    {
        [SerializeField] private float _startYposition;

        private void OnEnable()
        {
            transform.position = new Vector3(transform.position.x, _startYposition);
        }
    }
}
