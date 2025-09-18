namespace Project.UI.Views
{
    using UnityEngine;
    using UnityEngine.UI;
    using TMPro;
    using Project.Core;

    public class StickerCard : MonoBehaviour
    {
        [Header("UI Elements")]
        [SerializeField] private Image stickerImage;
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private TextMeshProUGUI quantityText;
        [SerializeField] private TextMeshProUGUI priceText;
        [SerializeField] private Button buyButton;
        [SerializeField] private GameObject ownedOverlay;
        [SerializeField] private TextMeshProUGUI statusText;
        
        private StickerService.StickerData stickerData;
        private StickerService stickerService;
        private CoinsWallet coinsWallet;
        
        public void Setup(StickerService.StickerData data, StickerService service, CoinsWallet wallet)
        {
            stickerData = data;
            stickerService = service;
            coinsWallet = wallet;
            
            if (nameText != null)
                nameText.text = data.name;
            if (quantityText != null)
                quantityText.text = $"x{data.quantity}";
            if (priceText != null)
                priceText.text = data.price.ToString();
            
            if (stickerImage != null && stickerService != null)
            {
                Sprite icon = stickerService.LoadStickerIcon(data.icon);
                if (icon != null)
                {
                    stickerImage.sprite = icon;
                }
            }
            
            if (buyButton != null)
                buyButton.onClick.AddListener(OnBuyClicked);
            UpdateVisuals();
        }
        
        private void UpdateVisuals()
        {
            bool isOwned = stickerService != null && stickerService.IsStickerOwned(stickerData.name);
            bool canAfford = coinsWallet != null && coinsWallet.GetCoins() >= stickerData.price;
            
            if (isOwned)
            {
                if (ownedOverlay != null) ownedOverlay.SetActive(true);
                if (statusText != null) statusText.text = "مملوك";
                if (buyButton != null) buyButton.interactable = false;
            }
            else
            {
                if (ownedOverlay != null) ownedOverlay.SetActive(false);
                if (statusText != null) statusText.text = canAfford ? "شراء" : "لا يكفي";
                if (buyButton != null) buyButton.interactable = canAfford;
            }
        }
        
        private void OnBuyClicked()
        {
            if (coinsWallet != null && coinsWallet.SpendCoins(stickerData.price))
            {
                stickerService?.PurchaseSticker(stickerData.name);
                UpdateVisuals();
            }
        }
    }
}
