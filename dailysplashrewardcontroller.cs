namespace Project.UI
{
    using UnityEngine;
    using UnityEngine.UI;
    using TMPro;
    using Project.Core;
    using Project.Systems;

    public class DailySplashRewardController : MonoBehaviour
    {
        [Header("UI Elements")]
        [SerializeField] private GameObject splashPanel;
        [SerializeField] private Button closeButton;
        [SerializeField] private DayItem[] dayItems;
        [SerializeField] private TextMeshProUGUI tomorrowText;
        [SerializeField] private TextMeshProUGUI countdownText;
        
        [Header("Services")]
        [SerializeField] private DailyRewardService dailyRewardService;
        [SerializeField] private CoinsWallet coinsWallet;
        
        private bool isOpen = false;
        
        public void Init()
        {
            if (closeButton != null)
                closeButton.onClick.AddListener(Close);

            var bootLoader = BootLoader.Instance;
            if (bootLoader != null)
            {
                if (dailyRewardService == null)
                    dailyRewardService = bootLoader.GetDailyRewardService();
                if (coinsWallet == null)
                    coinsWallet = bootLoader.GetCoinsWallet();
            }
            
            if (dayItems != null)
            {
                for (int i = 0; i < dayItems.Length; i++)
                {
                    if (dayItems[i] != null)
                    {
                        dayItems[i].Init(i + 1, this);
                    }
                }
            }
            
            Build();
        }
        
        public void Open()
        {
            if (isOpen) return;
            
            isOpen = true;
            if (splashPanel != null)
                splashPanel.SetActive(true);
            Build();
        }
        
        public void Close()
        {
            isOpen = false;
            if (splashPanel != null)
                splashPanel.SetActive(false);
        }
        
        public void Build()
        {
            if (!isOpen || dailyRewardService == null) return;
            
            int currentDay = dailyRewardService.GetCurrentDay();
            bool hasClaimedToday = dailyRewardService.HasClaimedToday();
            
            if (dayItems != null)
            {
                for (int i = 0; i < dayItems.Length; i++)
                {
                    if (dayItems[i] != null)
                    {
                        int day = i + 1;
                        DayItemState state;
                        
                        if (day == currentDay)
                        {
                            state = hasClaimedToday ? DayItemState.Claimed : DayItemState.Active;
                        }
                        else if (day < currentDay)
                        {
                            state = DayItemState.Claimed;
                        }
                        else
                        {
                            state = DayItemState.Inactive;
                        }
                        
                        dayItems[i].ApplyState(state);
                    }
                }
            }
            
            if (hasClaimedToday)
            {
                UpdateTomorrowCountdown();
            }
        }
        
        private void UpdateTomorrowCountdown()
        {
            var tomorrow = System.DateTime.Now.Date.AddDays(1);
            var timeUntilTomorrow = tomorrow - System.DateTime.Now;
            if (countdownText != null)
            {
                countdownText.text = $"{timeUntilTomorrow.Hours:D2}:{timeUntilTomorrow.Minutes:D2}:{timeUntilTomorrow.Seconds:D2}";
            }
        }
        
        public void OnDayItemClicked(int day)
        {
            if (dailyRewardService != null && dailyRewardService.CanClaimReward() && day == dailyRewardService.GetCurrentDay())
            {
                int reward = dailyRewardService.ClaimReward();
                coinsWallet?.AddCoins(reward);
                Build();
            }
        }
        
        public void OnDoubleReward(int day)
        {
            // TODO: Implement double reward via ad
        }
        
        public void ApplyState() => Build();
    }
}
