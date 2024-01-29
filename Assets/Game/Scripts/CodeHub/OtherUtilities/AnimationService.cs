using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace CodeHub.OtherUtilities
{
    public class AnimationService
    {
        private float _durationDeflate;
        private float _durationAppearance;
        private Vector3 _punchVector;

        private float shakeDuration = 0.75f;
        private Vector3 shakeStrength = new Vector3(0.1f, 0.1f, 30f);
        private int shakeVibrato = 5;

        public AnimationService()
        {
            _durationDeflate = 0.3f;
            _durationAppearance = 0.2f;
            _punchVector = new Vector3(1.2f, 1.2f);
        }

        public AnimationService(float durationDeflate, float durationAppearance, Vector3 punchVector)
        {
            _durationDeflate = durationDeflate;
            _durationAppearance = durationAppearance;
            _punchVector = punchVector;
        }

        public async Task PlayDeflateAnimation(GameObject gameObject)
        {
            var deflateSequence = DOTween.Sequence();

            InitDeflateAnimation(deflateSequence, gameObject);

            await deflateSequence.Play().AsyncWaitForCompletion();
        }

        public async Task PlayAppearanceAnimation(GameObject gameObject)
        {
            var appearanceSequence = DOTween.Sequence();

            InitAppearanceAnimation(appearanceSequence, gameObject);

            await appearanceSequence.Play().AsyncWaitForCompletion();
        }

        public void StartShakeAnimation(GameObject gameObject)
        {
            if (DOTween.IsTweening(gameObject.transform))
                return;

            gameObject.transform.DOShakeRotation(shakeDuration, shakeStrength, shakeVibrato,
                    2f, true, ShakeRandomnessMode.Harmonic)
                .SetEase(Ease.InOutSine);
        }

        private void InitDeflateAnimation(Sequence sequence, GameObject gameObject)
        {
            sequence.Join(gameObject.transform.DOPunchScale(_punchVector, _durationDeflate, 1)
                .SetLoops(2, LoopType.Yoyo));
        }

        private void InitAppearanceAnimation(Sequence sequence, GameObject gameObject)
        {
            gameObject.transform.localScale = Vector3.zero;
            sequence.Join(gameObject.transform.DOScale(Vector3.one, _durationAppearance).SetEase(Ease.InOutBack));
        }
    }
}