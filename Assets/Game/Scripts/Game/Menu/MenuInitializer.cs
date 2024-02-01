using UnityEngine;

namespace Game.Scripts.Game.Menu
{
    public class MenuInitializer : MonoBehaviour
    {
       
        [SerializeField] private CoinPlayerBalanceUpdater _playerBalanceUpdater;
        
        private void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            _playerBalanceUpdater.Initialize();
        }
    }
}
