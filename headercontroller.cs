namespace Project.UI
{
    using UnityEngine;
    using UnityEngine.UI;
    using TMPro;
    using Project.Core;
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
            var bootLoader = BootLoader.Instance;
            if (bootLoader != null)
            {
                coinsWallet = bootLoader.GetCoinsWallet();
                rewardedAdService = bootLoader.GetRewardedAdService();
                audioService = bootLoader.GetAudioService();
            }
            
            if (addCoinsButton != null)
                addCoinsButton.onClick.AddListener(OnAddCoinsClicked);
            if (infoButton != null && infoSplash != null)
                infoButton.onClick.AddListener(() => {
                    audioService?.PlayButtonClick();
                    infoSplash.Open();
                });
            if (giftButton != null && dailyRewardSplash != null)
                giftButton.onClick.AddListener(() => {
                    audioService?.PlayButtonClick();
                    dailyRewardSplash.Open();
                });
            if (settingsButton != null && settingsSplash != null)
                settingsButton.onClick.AddListener(() => {
                    audioService?.PlayButtonClick();
                    settingsSplash.Open();
                });
            
            if (coinsWallet != null)
            {
                coinsWallet.OnCoinsChanged += UpdateCoinsDisplay;
                UpdateCoinsDisplay(coinsWallet.GetCoins());
            }
            
            if (cooldownPopup != null)
                cooldownPopup.Init();
        }
        
        private void OnAddCoinsClicked()
        {
            audioService?.PlayButtonClick();
            
            if (rewardedAdService != null && rewardedAdService.CanShowAd())
            {
                rewardedAdService.ShowRewardedAd((coins) =>
                {
                    coinsWallet?.AddCoins(coins);
                    audioService?.PlayCoinCollect();
                });
            }
            else if (cooldownPopup != null && rewardedAdService != null)
            {
                var remainingTime = rewardedAdService.GetRemainingCooldown();
                cooldownPopup.ShowCooldown((int)remainingTime.TotalMinutes, remainingTime.Seconds);
            }
        }
        
        private void UpdateCoinsDisplay(int coins)
        {
            if (coinsText != null)
            {
                coinsText.text = coins.ToString();
            }
            
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