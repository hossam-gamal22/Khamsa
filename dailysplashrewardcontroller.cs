namespace Project.UI
{
    using UnityEngine;
    using UnityEngine.UI;
    using TMPro;
    using Project.Core;
    // Import Project.Systems to access BootLoader
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
            closeButton.onClick.AddListener(Close);

            // Fetch global services from BootLoader if not assigned via the Inspector.
            // This allows the controller to work even if service references are not wired manually.
            if (dailyRewardService == null && BootLoader.Instance != null)
            {
                dailyRewardService = BootLoader.Instance.GetDailyRewardService();
            }
            if (coinsWallet == null && BootLoader.Instance != null)
            {
                coinsWallet = BootLoader.Instance.GetCoinsWallet();
            }
            
            // Initialize day items
            for (int i = 0; i < dayItems.Length; i++)
            {
                int dayIndex = i;
                dayItems[i].Init(dayIndex + 1, this);
            }
            
            Build();
        }
        
        public void Open()
        {
            if (isOpen) return;
            
            isOpen = true;
            splashPanel.SetActive(true);
            Build();
        }
        
        public void Close()
        {
            isOpen = false;
            splashPanel.SetActive(false);
        }
        
        public void Build()
        {
            if (!isOpen) return;
            
            int currentDay = dailyRewardService.GetCurrentDay();
            bool hasClaimedToday = dailyRewardService.HasClaimedToday();
            
            for (int i = 0; i < dayItems.Length; i++)
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
            
            // Update tomorrow countdown if claimed today
            if (hasClaimedToday)
            {
                UpdateTomorrowCountdown();
            }
        }
        
        private void UpdateTomorrowCountdown()
        {
            var tomorrow = System.DateTime.Now.Date.AddDays(1);
            var timeUntilTomorrow = tomorrow - System.DateTime.Now;
            countdownText.text = $"{timeUntilTomorrow.Hours:D2}:{timeUntilTomorrow.Minutes:D2}:{timeUntilTomorrow.Seconds:D2}";
        }
        
        public void OnDayItemClicked(int day)
        {
            if (dailyRewardService.CanClaimReward() && day == dailyRewardService.GetCurrentDay())
            {
                int reward = dailyRewardService.ClaimReward();
                coinsWallet.AddCoins(reward);
                Build(); // Refresh UI
            }
        }
        
        public void OnDoubleReward(int day)
        {
            // TODO: Implement double reward via ad
        }
        
        public void ApplyState() => Build();
    }
}