namespace Project.UI.Views
{
    using UnityEngine;
    using UnityEngine.UI;
    using TMPro;
    using Project.Core;

    public class CoinCard : MonoBehaviour
    {
        [Header("UI Elements")]
        [SerializeField] private Image packageIcon;
        [SerializeField] private TextMeshProUGUI packageNameText;
        [SerializeField] private TextMeshProUGUI coinAmountText;
        [SerializeField] private TextMeshProUGUI realPriceText;
        [SerializeField] private Button buyButton;
        [SerializeField] private TextMeshProUGUI buyButtonText;
        
        private CoinPackageService.CoinPackageData packageData;
        private CoinPackageService coinPackageService;
        
        public void Setup(CoinPackageService.CoinPackageData data, CoinPackageService service)
        {
            packageData = data;
            coinPackageService = service;
            
            if (packageNameText != null)
                packageNameText.text = data.name;
            if (coinAmountText != null)
                coinAmountText.text = $"+{data.coinAmount}";
            if (realPriceText != null)
                realPriceText.text = data.realPrice;
            if (buyButtonText != null)
                buyButtonText.text = "شراء";
            
            if (packageIcon != null && coinPackageService != null)
            {
                Sprite icon = coinPackageService.LoadCoinPackageIcon(data.icon);
                if (icon != null)
                {
                    packageIcon.sprite = icon;
                }
            }
            
            if (buyButton != null)
                buyButton.onClick.AddListener(OnBuyClicked);
        }
        
        private void OnBuyClicked()
        {
            coinPackageService?.PurchaseCoinPackage(packageData.productId, packageData.coinAmount);
            Debug.Log($"Attempting to purchase {packageData.name} for {packageData.realPrice}");
        }
    }
}
