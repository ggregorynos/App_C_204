using System;
using System.Collections.Generic;
using CodeHub.OtherUtilities;
using DG.Tweening;
using Game.Mephistoss.PanelMachine.Scripts;
using Game.Scripts.Game.GameLogic.BetLogic;
using Game.Scripts.Game.GameLogic.GameData;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Scripts.Game.GameLogic
{
    public class RouletteContext : MonoBehaviour
    {
        [SerializeField] private PanelMachine _panelMachine;
        [SerializeField] private PanelBase _roulettePanel;
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
        [SerializeField] private GameObject _ball;
        [SerializeField] private Transform _ballEndPoint;

        [SerializeField] private AudioSource _spin;
        [SerializeField] private AudioSource _cardPanel;

        private float sectorAngle;

        private Vector3 _startBallPosition;

        private void Start()
        {
            _startBallPosition = _ball.transform.localPosition;
        }

        public void Play()
        {
            DisableInput();
            _panelMachine.AddPanel(_roulettePanel);

            var randomNumber = GetRandomNumber();

            ballParent.rotation = Quaternion.identity;
            _ball.transform.localPosition = _startBallPosition;

            _playerDatabase.IncreasePlayerBalance(-_betContext.GetAllBetCount());
            Spin(randomNumber);
            RotateBall((() =>
            {
                _panelMachine.CloseLastPanel();
                _resultPanel.ShowPanel(randomNumber);
                _spin.Stop();
                _cardPanel.Play();
            }));

            _spin.Play();
        }

        private void DisableInput()
        {
            _inputController.SetEnableButtons(false);
            _inputController.SetEnableOverlayClickBlockers(true);
        }

        private NumberData GetRandomNumber()
        {
            var randomIndex = Random.Range(0, _numbers.Count);
            return _numbers[randomIndex];
        }

        private void Spin(NumberData numberData, Action onComplete = null)
        {
            sectorAngle = 360f / _numbers.Count;

            int fullCircles = Random.Range(minRotateWheel, maxRotateWheel);
            int finalSectorIndex = _numbers.IndexOf(numberData);
            float finalAngle = sectorAngle * finalSectorIndex;

            wheelParts.DORotate(new Vector3(0f, 0f, (-fullCircles * 360f) - finalAngle), rotationTime * fullCircles,
                    RotateMode.FastBeyond360)
                .SetEase(Ease.OutQuint);
        }

        private void RotateBall(Action onComplete = null)
        {
            ballParent.DORotate(new Vector3(0f, 0f, (-maxRotateBallWheel * 360f)), ballRotateTime * maxRotateBallWheel,
                    RotateMode.FastBeyond360)
                .SetEase(Ease.Linear).OnComplete((() => { MoveBall(onComplete); }));
        }

        private void MoveBall(Action onComplete)
        {
            _ball.transform.DOMove(_ballEndPoint.position, 0.5f).SetEase(Ease.Linear)
                .OnComplete((() => { DOVirtual.DelayedCall(1f, () => onComplete?.Invoke()); }));
        }
    }
}