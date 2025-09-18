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
            
            buyButton.onClick.AddListener(OnBuyClicked);
            
            // Load avatar sprite
            Sprite sprite = avatarService.LoadAvatarSprite(avatarData.avatar);
            if (sprite != null)
            {
                avatarImage.sprite = sprite;
            }
            
            UpdateVisuals();
        }
        
        private void UpdateVisuals()
        {
            bool isOwned = avatarService.IsAvatarOwned(avatarData.avatar) || avatarData.free;
            bool isActive = avatarService.GetCurrentAvatar() == avatarData.avatar;
            
            if (avatarData.free)
            {
                if (isActive)
                {
                    // Free + Active
                    activeBorder.SetActive(true);
                    lockedOverlay.SetActive(false);
                    statusText.text = "مفعل";
                    buyButton.interactable = false;
                }
                else
                {
                    // Free + Inactive
                    activeBorder.SetActive(false);
                    lockedOverlay.SetActive(false);
                    statusText.text = "تفعيل";
                    buyButton.interactable = true;
                }
            }
            else
            {
                if (isOwned)
                {
                    // Paid + Owned
                    activeBorder.SetActive(isActive);
                    lockedOverlay.SetActive(false);
                    statusText.text = isActive ? "مفعل" : "تفعيل";
                    buyButton.interactable = !isActive;
                }
                else
                {
                    // Paid + Locked
                    activeBorder.SetActive(false);
                    lockedOverlay.SetActive(true);
                    priceText.text = avatarData.price.ToString();
                    buyButton.interactable = coinsWallet.GetCoins() >= avatarData.price;
                }
            }
        }
        
        private void OnBuyClicked()
        {
            if (avatarData.free || avatarService.IsAvatarOwned(avatarData.avatar))
            {
                // Activate avatar
                avatarService.SetAvatar(avatarData.avatar);
            }
            else
            {
                // Purchase avatar
                if (coinsWallet.SpendCoins(avatarData.price))
                {
                    // TODO: Add avatar to owned list
                    avatarService.SetAvatar(avatarData.avatar);
                }
                else
                {
                    // Show insufficient funds popup
                    // TODO: Implement NoMoney popup
                }
            }
            
            UpdateVisuals();
        }
    }
}