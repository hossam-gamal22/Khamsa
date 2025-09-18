namespace Project.UI.Views
{
    using UnityEngine;
    using UnityEngine.UI;
    using TMPro;
    using Project.Core;

    public class AvatarCard : MonoBehaviour
    {
        [Header("UI Elements")]
        [SerializeField] private Image avatarImage;
        [SerializeField] private TextMeshProUGUI priceText;
        [SerializeField] private Button buyButton;
        [SerializeField] private GameObject activeBorder;
        [SerializeField] private GameObject lockedOverlay;
        [SerializeField] private TextMeshProUGUI statusText;
        
        private AvatarService.AvatarData avatarData;
        private AvatarService avatarService;
        private CoinsWallet coinsWallet;
        
        public void Setup(AvatarService.AvatarData data, AvatarService service, CoinsWallet wallet)
        {
            avatarData = data;
            avatarService = service;
            coinsWallet = wallet;
            
            if (buyButton != null)
                buyButton.onClick.AddListener(OnBuyClicked);
            
            if (avatarImage != null && avatarService != null)
            {
                Sprite sprite = avatarService.LoadAvatarSprite(avatarData.avatar);
                if (sprite != null)
                {
                    avatarImage.sprite = sprite;
                }
            }
            
            UpdateVisuals();
        }
        
        private void UpdateVisuals()
        {
            bool isOwned = (avatarService != null && avatarService.IsAvatarOwned(avatarData.avatar)) || avatarData.free;
            bool isActive = avatarService != null && avatarService.GetCurrentAvatar() == avatarData.avatar;
            
            if (avatarData.free)
            {
                if (isActive)
                {
                    if (activeBorder != null) activeBorder.SetActive(true);
                    if (lockedOverlay != null) lockedOverlay.SetActive(false);
                    if (statusText != null) statusText.text = "مفعل";
                    if (buyButton != null) buyButton.interactable = false;
                }
                else
                {
                    if (activeBorder != null) activeBorder.SetActive(false);
                    if (lockedOverlay != null) lockedOverlay.SetActive(false);
                    if (statusText != null) statusText.text = "تفعيل";
                    if (buyButton != null) buyButton.interactable = true;
                }
            }
            else
            {
                if (isOwned)
                {
                    if (activeBorder != null) activeBorder.SetActive(isActive);
                    if (lockedOverlay != null) lockedOverlay.SetActive(false);
                    if (statusText != null) statusText.text = isActive ? "مفعل" : "تفعيل";
                    if (buyButton != null) buyButton.interactable = !isActive;
                }
                else
                {
                    if (activeBorder != null) activeBorder.SetActive(false);
                    if (lockedOverlay != null) lockedOverlay.SetActive(true);
                    if (priceText != null) priceText.text = avatarData.price.ToString();
                    if (buyButton != null && coinsWallet != null)
                        buyButton.interactable = coinsWallet.GetCoins() >= avatarData.price;
                }
            }
        }
        
        private void OnBuyClicked()
        {
            if (avatarData.free || (avatarService != null && avatarService.IsAvatarOwned(avatarData.avatar)))
            {
                avatarService?.SetAvatar(avatarData.avatar);
            }
            else
            {
                if (coinsWallet != null && coinsWallet.SpendCoins(avatarData.price))
                {
                    avatarService?.PurchaseAvatar(avatarData.avatar);
                    avatarService?.SetAvatar(avatarData.avatar);
                }
            }
            
            UpdateVisuals();
        }
    }
}