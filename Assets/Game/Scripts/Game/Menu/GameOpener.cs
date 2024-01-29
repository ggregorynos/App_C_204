using CodeHub.OtherUtilities;
using UnityEngine;

namespace Game.Scripts.Game.Menu
{
    public class GameOpener : MonoBehaviour
    {
       // [SerializeField] private PlaceContextData _placeContextData;
        [SerializeField] private GameObject _placeShopBtnToAnim;
        
        private AnimationService _animationService;

        private void Start()
        {
            _animationService = new AnimationService();
        }

        public void TryPlayGame()
        {
            // var currentSelectPlace = _placeContextData.GetCurrentPlace();
            //
            // if (currentSelectPlace.HasOpen)
            // {
            //   // GameDataHolder.SetPlaceData(currentSelectPlace);
            //    SceneLoader.Instance.SwitchToScene("Game");
            // }
            // else
            // {
            //     _animationService.StartShakeAnimation(_placeShopBtnToAnim);
            // }
        }
    }
}