using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Tools.UnityAdsService.Scripts.Common;
using Tools.UnityAdsService.Scripts.View;
using UnityEngine;
using UnityEngine.Advertisements;

namespace Tools.UnityAdsService.Scripts
{
    public class UnityAdsService : MonoBehaviour, IUnityAdsInitializationListener
    {
        private const int ADLoadMaxCount = 2;
        
        private const string RewardedVideoPlacementAndroid = "Rewarded_Android";
        private const string RewardedVideoPlacementIOS = "Rewarded_iOS";
        
        public static UnityAdsService Instance { get; private set; }

        [SerializeField] private string _gameIdAndroid;
        [SerializeField] private string _gameIdIos;
        [SerializeField] private bool _testMode;

        [Space] 
        [SerializeField] private FaderView _faderViewPrefab;
        [SerializeField] private Transform _parentFader;

        private NetworkReachabilityService networkService;
        private string currentIdInitialize;

        private string CurrentIdShow { get; set; }
        private Queue<UnityAdsListener> AdsListenersPool { get; set; }
        
        public bool IsInitialize { get; private set; }
        public bool IsAvailableShow { get; private set; }
        
        public event Action OnInitialize;
        public event Action<bool> OnAvailableShow;

        private void Awake()
        {
            CreateSingleton();
            Initialize();
        }

        public UnityAdsListener ShowRewardedAd()
        {
            if (!networkService.IsInternetAvailable || !IsAvailableShow)
            {
                var faderView = CreateFader();
                DOVirtual.DelayedCall(0.5f,() => faderView.ShowError()).Play();
                
                return null;
            }
            
            var listener = AdsListenersPool.Dequeue();
            Advertisement.Show(CurrentIdShow, listener);

            StartCoroutine(LoadRewardsCoroutine());
            StartCoroutine(PrepareShowAD(listener));

            return listener;
        }

        private void Initialize()
        {
            InitializePlatform();
            InitializeNetworkService();

            if (networkService.IsInternetAvailable)
            {
                InitializeAdvertisement();
            }
            else
            {
                networkService.OnChanged += InitializeAdvertisement;
            }

            networkService.OnChanged += CheckAvailableInternetConnection;
        }

        private void CheckAvailableInternetConnection(bool isAvailable)
        {
            if (!isAvailable)
            {
                networkService.TryAddListener(AwaitInternetConnection);
                
                IsAvailableShow = false;
                OnAvailableShow?.Invoke(false);
            }
        }

        private void InitializeNetworkService()
        {
            networkService = GetComponentInChildren<NetworkReachabilityService>();
            networkService.CheckInternet();
        }

        private void InitializeAdvertisement(bool isAvailable = true)
        {
            if (!isAvailable)
            {
                return;
            }

            if (Advertisement.isSupported)
            {
                Debug.Log(Application.platform + " supported by Advertisement");
            }

            Advertisement.Initialize(currentIdInitialize, _testMode, this);
            networkService.OnChanged -= InitializeAdvertisement;
        }

        private void AwaitInternetConnection(bool isAvailable)
        {
            if (AdsListenersPool != null)
            {
                AdsListenersPool.Clear();
            }
            
            StartCoroutine(LoadRewardsAtNoInternetConnectionCoroutine());

            networkService.OnChanged -= AwaitInternetConnection;
        }

        private UnityAdsListener CreateUnityAdsListener()
        {
            var listener = new UnityAdsListener();
            listener.Load(CurrentIdShow);
            AdsListenersPool.Enqueue(listener);

            return listener;
        }

        public void OnInitializationComplete()
        {
            Debug.Log("Init Success");
            StartCoroutine(LoadRewardsAtStartCoroutine());
        }

        public void OnInitializationFailed(UnityAdsInitializationError error, string message)
        {
            Debug.Log($"Init Failed: [{error}]: {message}");
        }

        private IEnumerator LoadRewardsAtStartCoroutine()
        {
            yield return StartCoroutine(LoadRewardsCoroutine());

            Debug.Log($"All ADS loaded in pool");
            
            IsInitialize = true;
            OnInitialize?.Invoke();
            
            IsAvailableShow = true;
            OnAvailableShow?.Invoke(true);
        }
        
        private IEnumerator LoadRewardsAtNoInternetConnectionCoroutine()
        {
            yield return StartCoroutine(LoadRewardsCoroutine());

            Debug.Log($"All ADS loaded in pool");
            IsAvailableShow = true;
            OnAvailableShow?.Invoke(true);
        }
        
        private IEnumerator LoadRewardsCoroutine()
        {
            AdsListenersPool ??= new Queue<UnityAdsListener>();

            while (AdsListenersPool.Count <= ADLoadMaxCount)
            {
                var listener = CreateUnityAdsListener();

                yield return new WaitUntil(() => listener.IsAdsLoaded);
            }
            yield break;
        }

        private IEnumerator PrepareShowAD(UnityAdsListener listener)
        {
            var faderView = CreateFader();
            
            listener.OnShowCompleteAds += faderView.CloseView;
            listener.OnShowFailedAds += faderView.ShowError;

            yield return new WaitForSeconds(1f);

            if (!listener.IsAdsStarted)
            {
                Advertisement.Show(CurrentIdShow, listener);
                yield return new WaitForSeconds(2f);

                if (!listener.IsAdsStarted)
                {
                    Debug.Log("ADS don't start, hide Fader");
                    faderView.ShowError();
                    yield break;
                }
            }
        }

        private FaderView CreateFader()
        {
            var faderView = Instantiate(_faderViewPrefab, _parentFader)
                .GetComponent<FaderView>();

            faderView.transform.localPosition = Vector3.zero;
            
            return faderView;
        }

        private void InitializePlatform()
        {
#if UNITY_IOS
            currentIdInitialize = _gameIdIos;
            CurrentIdShow = RewardedVideoPlacementIOS;
#elif UNITY_ANDROID
            currentIdInitialize = _gameIdAndroid;
            CurrentIdShow = RewardedVideoPlacementAndroid;
#endif
        }

        private void CreateSingleton()
        {
            if (Instance != null && Instance != this)
                Destroy(gameObject);
            else
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }
    }
}