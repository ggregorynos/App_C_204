using UnityEngine;

namespace Game.Scripts.Game.ShopElementsLogic
{
    public class ShopContext : MonoBehaviour
    {
        private const string CurrentAdsAlias = "CurrentAdsAlias";
        
        //public CurrentAds
    }

    public class ShopElement : MonoBehaviour
    {
        [SerializeField] private GameObject _activeAdsBtn;
        [SerializeField] private GameObject _nonActiveAdsBtn;

        public void EnableAdsBtn(bool enable)
        {
            _activeAdsBtn.gameObject.SetActive(enable);
            _nonActiveAdsBtn.gameObject.SetActive(!enable);
        }
    }
}
