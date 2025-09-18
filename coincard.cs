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
            
            // Setup UI
            packageNameText.text = data.name;
            coinAmountText.text = $"+{data.coinAmount}";
            realPriceText.text = data.realPrice;
            buyButtonText.text = "شراء";
            
            // Load package icon
            Sprite icon = coinPackageService.LoadCoinPackageIcon(data.icon);
            if (icon != null)
            {
                packageIcon.sprite = icon;
            }
            
            buyButton.onClick.AddListener(OnBuyClicked);
        }
        
        private void OnBuyClicked()
        {
            // TODO: Integrate with Unity IAP
            coinPackageService.PurchaseCoinPackage(packageData.productId, packageData.coinAmount);
            
            // For testing purposes, show a message
            Debug.Log($"Attempting to purchase {packageData.name} for {packageData.realPrice}");
        }
    }
}