using System.Collections;
using UnityEngine;

namespace CodeHub.OtherUtilities
{
    public class FlashMaker : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Color _flashColor;
        [SerializeField] private float _flashDuration;
        [SerializeField] private int _flashCount;

        private Color _originalColor;

        private void Start()
        {
            _originalColor = _spriteRenderer.color;
        }

        public void Flash()
        {
            StartCoroutine(FlashCoroutine());
        }

        private IEnumerator FlashCoroutine()
        {
            for (int i = 0; i < _flashCount; i++)
            {
                _spriteRenderer.color = _flashColor;
                yield return new WaitForSeconds(_flashDuration / 2);
                _spriteRenderer.color = _originalColor;
                yield return new WaitForSeconds(_flashDuration / 2);
            }
        }
    }
}
