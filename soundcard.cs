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
            
            // Setup UI
            nameText.text = data.name;
            priceText.text = data.price.ToString();
            
            // Load sound icon
            Sprite icon = soundService.LoadSoundIcon(data.icon);
            if (icon != null)
            {
                soundIcon.sprite = icon;
            }
            
            buyButton.onClick.AddListener(OnBuyClicked);
            playButton.onClick.AddListener(OnPlayClicked);
            UpdateVisuals();
        }
        
        private void UpdateVisuals()
        {
            bool isOwned = soundService.IsSoundOwned(soundData.name);
            bool canAfford = coinsWallet.GetCoins() >= soundData.price;
            
            if (isOwned)
            {
                ownedOverlay.SetActive(true);
                statusText.text = "مملوك";
                buyButton.interactable = false;
                playButton.interactable = true; // Can preview owned sounds
            }
            else
            {
                ownedOverlay.SetActive(false);
                statusText.text = canAfford ? "شراء" : "لا يكفي";
                buyButton.interactable = canAfford;
                playButton.interactable = true; // Can preview before buying
            }
        }
        
        private void OnBuyClicked()
        {
            if (coinsWallet.SpendCoins(soundData.price))
            {
                soundService.PurchaseSound(soundData.name);
                UpdateVisuals();
            }
        }
        
        private void OnPlayClicked()
        {
            soundService.PlaySoundPreview(soundData.audioFile);
        }
        
        private void OnDisable()
        {
            // Stop sound when card is disabled
            if (soundService != null)
            {
                soundService.StopSoundPreview();
            }
        }
    }
}