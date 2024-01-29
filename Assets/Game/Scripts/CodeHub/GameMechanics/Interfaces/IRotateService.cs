using UnityEngine;

namespace CodeHub.GameMechanics
{
    public interface IRotateService
    {
        public void EnableRotation(bool canRotate, GameObject objectToRotate = null);
        public void Rotate(GameObject gameObject);
    }
}