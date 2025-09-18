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
            
            // Setup UI
            nameText.text = data.name;
            quantityText.text = $"x{data.quantity}";
            priceText.text = data.price.ToString();
            
            // Load sticker icon
            Sprite icon = stickerService.LoadStickerIcon(data.icon);
            if (icon != null)
            {
                stickerImage.sprite = icon;
            }
            
            buyButton.onClick.AddListener(OnBuyClicked);
            UpdateVisuals();
        }
        
        private void UpdateVisuals()
        {
            bool isOwned = stickerService.IsStickerOwned(stickerData.name);
            bool canAfford = coinsWallet.GetCoins() >= stickerData.price;
            
            if (isOwned)
            {
                ownedOverlay.SetActive(true);
                statusText.text = "مملوك";
                buyButton.interactable = false;
            }
            else
            {
                ownedOverlay.SetActive(false);
                statusText.text = canAfford ? "شراء" : "لا يكفي";
                buyButton.interactable = canAfford;
            }
        }
        
        private void OnBuyClicked()
        {
            if (coinsWallet.SpendCoins(stickerData.price))
            {
                stickerService.PurchaseSticker(stickerData.name);
                UpdateVisuals();
            }
        }
    }
}