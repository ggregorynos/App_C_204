using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace CodeHub.WheelSpinLogic
{
    public class WheelSpin : MonoBehaviour
    {
        [SerializeField] private float rotationTime = 1f;
        [SerializeField] private List<WheelSector> sectors;
        
        [SerializeField] private int minRotateWheel;
        [SerializeField] private int maxRotateWheel;
        
        [SerializeField] private RectTransform wheelParts;
    
        private float sectorAngle; 

        [ContextMenu("try spin")]
        public async Task<WheelSector> TrySpin()
        {
            sectorAngle = 360f / sectors.Count;
            
            int fullCircles = Random.Range(minRotateWheel, maxRotateWheel);
            int finalSectorIndex = Random.Range(0, sectors.Count - 1);
            float finalAngle = sectorAngle * finalSectorIndex; 
            
            var sequence = DOTween.Sequence();

            sequence.Join(wheelParts.DORotate(new Vector3(0f, 0f, (-fullCircles * 360f)-finalAngle), rotationTime * fullCircles,
                    RotateMode.FastBeyond360)
                .SetEase(Ease.OutQuint));
            
            await sequence.Play().AsyncWaitForCompletion();
            return sectors[finalSectorIndex];
        }
    }
}