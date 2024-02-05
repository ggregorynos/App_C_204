using Game.Scripts.Game.GameLogic.BetLogic;
using UnityEngine;

namespace Game.Scripts.Game.GameLogic.GameData
{
    public class GameContext : MonoBehaviour
    {
        [SerializeField] private BetContext _betContext;
        [SerializeField] private BetContext _betInputController;

        private void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            _betContext.Initialize();
            _betInputController.Initialize();
        }
    }
}