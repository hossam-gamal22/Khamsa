namespace Project.UI.Views
{
    using UnityEngine;
    using UnityEngine.UI;
    using TMPro;
    using Project.Core;

    public class SoundCard : MonoBehaviour
    {
        [Header("UI Elements")]
        [SerializeField] private Image soundIcon;
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private TextMeshProUGUI priceText;
        [SerializeField] private Button buyButton;
        [SerializeField] private Button playButton;
        [SerializeField] private GameObject ownedOverlay;
        [SerializeField] private TextMeshProUGUI statusText;
        
        private SoundService.SoundData soundData;
        private SoundService soundService;
        private CoinsWallet coinsWallet;
        
        public void Setup(SoundService.SoundData data, SoundService service, CoinsWallet wallet)
        {
            soundData = data;
            soundService = service;
            coinsWallet = wallet;
            
            if (nameText != null)
                nameText.text = data.name;
            if (priceText != null)
                priceText.text = data.price.ToString();
            
            if (soundIcon != null && soundService != null)
            {
                Sprite icon = soundService.LoadSoundIcon(data.icon);
                if (icon != null)
                {
                    soundIcon.sprite = icon;
                }
            }
            
            if (buyButton != null)
                buyButton.onClick.AddListener(OnBuyClicked);
            if (playButton != null)
                playButton.onClick.AddListener(OnPlayClicked);
            UpdateVisuals();
        }
        
        private void UpdateVisuals()
        {
            bool isOwned = soundService != null && soundService.IsSoundOwned(soundData.name);
            bool canAfford = coinsWallet != null && coinsWallet.GetCoins() >= soundData.price;
            
            if (isOwned)
            {
                if (ownedOverlay != null) ownedOverlay.SetActive(true);
                if (statusText != null) statusText.text = "مملوك";
                if (buyButton != null) buyButton.interactable = false;
                if (playButton != null) playButton.interactable = true;
            }
            else
            {
                if (ownedOverlay != null) ownedOverlay.SetActive(false);
                if (statusText != null) statusText.text = canAfford ? "شراء" : "لا يكفي";
                if (buyButton != null) buyButton.interactable = canAfford;
                if (playButton != null) playButton.interactable = true;
            }
        }
        
        private void OnBuyClicked()
        {
            if (coinsWallet != null && coinsWallet.SpendCoins(soundData.price))
            {
                soundService?.PurchaseSound(soundData.name);
                UpdateVisuals();
            }
        }
        
        private void OnPlayClicked()
        {
            soundService?.PlaySoundPreview(soundData.audioFile);
        }
        
        private void OnDisable()
        {
            soundService?.StopSoundPreview();
        }
    }
}