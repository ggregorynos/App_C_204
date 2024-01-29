using UnityEngine;

namespace CodeHub.OtherUtilities
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class CharacterFlip : MonoBehaviour
    {
        [SerializeField] private float repeatRateForCheckFlip = 0.5f;
        private Rigidbody2D rb;

        private bool _canFlip;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            EnableFip(true);
            InvokeRepeating(nameof(StartCheckFlip),0,0.5f);
        }

        private void StartCheckFlip()
        {
            if(!_canFlip) return;
            
            float movementDirection = rb.velocity.x;

            if (movementDirection < 0f)
            {
                FlipCharacter(true);
            }
            else if (movementDirection > 0f)
            {
                FlipCharacter(false);
            }
        }

        private void FlipCharacter(bool flip)
        {
            Vector3 scale = transform.localScale;

            if (flip)
            {
                scale.x = -Mathf.Abs(scale.x);
            }
            else
            {
                scale.x = Mathf.Abs(scale.x);
            }

            transform.localScale = scale;
        }

        private void EnableFip(bool enable)
        {
            _canFlip = enable;
        }
    }
}