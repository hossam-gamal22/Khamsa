namespace Project.UI
{
    using UnityEngine;
    using UnityEngine.UI;
    using TMPro;
    using Project.Core;
    using Project.Services;
    // Import Project.Systems to access BootLoader
    using Project.Systems;

    public class HeaderController : MonoBehaviour
    {
        [Header("UI Elements")]
        [SerializeField] private TextMeshProUGUI coinsText;
        [SerializeField] private Button addCoinsButton;
        [SerializeField] private Button infoButton;
        [SerializeField] private Button giftButton;
        [SerializeField] private Button settingsButton;
        
        [Header("Splash Controllers")]
        [SerializeField] private InfoSplashController infoSplash;
        [SerializeField] private DailySplashRewardController dailyRewardSplash;
        [SerializeField] private SettingsSplashController settingsSplash;
        [SerializeField] private CooldownPopupController cooldownPopup;
        
        private CoinsWallet coinsWallet;
        private RewardedAdService rewardedAdService;
        private AudioService audioService;
        
        public void Init()
        {
            // Get services from global BootLoader
            coinsWallet = BootLoader.Instance.GetCoinsWallet();
            rewardedAdService = BootLoader.Instance.GetRewardedAdService();
            audioService = BootLoader.Instance.GetAudioService();
            
            // Setup button listeners
            addCoinsButton.onClick.AddListener(OnAddCoinsClicked);
            infoButton.onClick.AddListener(() => {
                audioService.PlayButtonClick();
                infoSplash.Open();
            });
            giftButton.onClick.AddListener(() => {
                audioService.PlayButtonClick();
                dailyRewardSplash.Open();
            });
            settingsButton.onClick.AddListener(() => {
                audioService.PlayButtonClick();
                settingsSplash.Open();
            });
            
            // Subscribe to coins changes
            coinsWallet.OnCoinsChanged += UpdateCoinsDisplay;
            UpdateCoinsDisplay(coinsWallet.GetCoins());
            
            // Initialize cooldown popup
            cooldownPopup.Init();
        }
        
        private void OnAddCoinsClicked()
        {
            audioService.PlayButtonClick();
            
            if (rewardedAdService.CanShowAd())
            {
                // Show rewarded ad
                rewardedAdService.ShowRewardedAd((coins) =>
                {
                    coinsWallet.AddCoins(coins);
                    audioService.PlayCoinCollect();
                });
            }
            else
            {
                // Show cooldown popup
                var remainingTime = rewardedAdService.GetRemainingCooldown();
                cooldownPopup.ShowCooldown((int)remainingTime.TotalMinutes, remainingTime.Seconds);
            }
        }
        
        private void UpdateCoinsDisplay(int coins)
        {
            coinsText.text = coins.ToString();
            
            // Optional: Animate coin change
            LeanTween.scale(coinsText.gameObject, Vector3.one * 1.1f, 0.2f)
                .setEase(LeanTweenType.easeOutBack)
                .setOnComplete(() => {
                    LeanTween.scale(coinsText.gameObject, Vector3.one, 0.2f)
                        .setEase(LeanTweenType.easeOutBack);
                });
        }
        
        public void Open() { }
        public void Close() { }
        public void Build() { }
        public void ApplyState() { }
    }
}