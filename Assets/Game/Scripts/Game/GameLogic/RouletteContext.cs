using System;
using System.Collections.Generic;
using CodeHub.OtherUtilities;
using DG.Tweening;
using Game.Scripts.Game.GameLogic.BetLogic;
using Game.Scripts.Game.GameLogic.GameData;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Scripts.Game.GameLogic
{
    public class RouletteContext : MonoBehaviour
    {
        [SerializeField] private PlayerDatabase _playerDatabase;
        [SerializeField] private BetContext _betContext;
        [SerializeField] private LocalPlayerInputController _inputController;
        [SerializeField] private ResultPanelContext _resultPanel;
        [SerializeField] private List<NumberData> _numbers;

        [SerializeField] private float rotationTime = 1f;
        [SerializeField] private float ballRotateTime = 2f;

        [SerializeField] private int minRotateWheel;
        [SerializeField] private int maxRotateWheel;
        [SerializeField] private int maxRotateBallWheel;

        [SerializeField] private RectTransform wheelParts;
        [SerializeField] private RectTransform ballParent;

        private float sectorAngle;

        public void Play()
        {
            //todo make roulette animation
            _inputController.SetEnableButtons(false);
            _inputController.SetEnableOverlayClickBlockers(true);
            var randomNumber = GetRandomNumber();
            DOVirtual.DelayedCall(3f, () => _resultPanel.ShowPanel(randomNumber));

            StartBet(); //todo change 

            Spin(randomNumber);
        }

        private void StartBet()
        {
            _playerDatabase.IncreasePlayerBalance(-_betContext.GetAllBetCount());
        }

        private NumberData GetRandomNumber()
        {
            var randomIndex = Random.Range(0, _numbers.Count);
            return _numbers[randomIndex];
        }

        public void Spin(NumberData numberData, Action onComplete = null)
        {
            sectorAngle = 360f / _numbers.Count;

            int fullCircles = Random.Range(minRotateWheel, maxRotateWheel);
            int finalSectorIndex = _numbers.IndexOf(numberData);
            float finalAngle = sectorAngle * finalSectorIndex;

            wheelParts.DORotate(new Vector3(0f, 0f, (-fullCircles * 360f) - finalAngle), rotationTime * fullCircles,
                    RotateMode.FastBeyond360)
                .SetEase(Ease.OutQuint);
            ballParent.DORotate(new Vector3(0f, 0f, (-maxRotateBallWheel * 360f)), ballRotateTime * fullCircles,
                    RotateMode.FastBeyond360)
                .SetEase(Ease.OutQuint).OnComplete((() => onComplete?.Invoke()));
        }
    }
}