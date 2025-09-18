using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Project.Core;
using Project.UI.Views;

namespace Project.UI
{
    public class DailySplashRewardController : MonoBehaviour
    {
        [Header("UI References")]
        public GameObject splashPanel;
        public Button closeButton;
        public DayItem[] dayItems;
        public TextMeshProUGUI tomorrowText;
        public TextMeshProUGUI countdownText;

        [Header("Services")]
        public DailyRewardService dailyRewardService;
        public CoinsWallet coinsWallet;
        public RewardedAdService adService;

        private void Start()
        {
            Build();
            closeButton.onClick.AddListener(CloseSplash);
        }

        public void Init()
        {
            Build();
        }

        public void Open()
        {
            splashPanel.SetActive(true);
            Build();
        }

        public void Build()
        {
            int todayIndex = dailyRewardService.GetTodayIndex();

            for (int i = 0; i < dayItems.Length; i++)
            {
                var item = dayItems[i];

                bool isClaimed = dailyRewardService.IsDayClaimed(i);
                bool isActive = i == todayIndex;
                bool isFuture = i > todayIndex;
                int reward = dailyRewardService.GetRewardAmount(i);

                item.Init(i, reward);
                item.ApplyState(isActive, isClaimed, isFuture, reward);

                int indexCopy = i;

                item.claimButton.onClick.RemoveAllListeners();
                item.claimButton.onClick.AddListener(() =>
                {
                    ClaimReward(indexCopy);
                });

                item.doubleButton.onClick.RemoveAllListeners();
                item.doubleButton.onClick.AddListener(() =>
                {
                    ClaimDoubleReward(indexCopy);
                });
            }

            UpdateCountdown();
        }

        private void ClaimReward(int dayIndex)
        {
            if (dailyRewardService.IsDayClaimed(dayIndex))
                return;

            int reward = dailyRewardService.GetRewardAmount(dayIndex);
            coinsWallet.AddCoins(reward);
            dailyRewardService.MarkDayClaimed(dayIndex);

            Build();
        }

        private void ClaimDoubleReward(int dayIndex)
        {
            if (!dailyRewardService.IsDayClaimed(dayIndex))
                return;

            if (!adService.IsAdReady())
            {
                Debug.Log("Ad not ready");
                return;
            }

            // ✅ FIXED: Action<int> delegate
            adService.ShowRewardedAd((rewardAmount) =>
            {
                coinsWallet.AddCoins(rewardAmount);
                Debug.Log($"Double reward claimed: +{rewardAmount}");
            });
        }

        private void UpdateCountdown()
        {
            System.TimeSpan timeUntilTomorrow = dailyRewardService.GetTimeUntilNextDay();

            if (timeUntilTomorrow.TotalSeconds > 0)
            {
                tomorrowText.gameObject.SetActive(true);
                countdownText.gameObject.SetActive(true);
                countdownText.text = $"{timeUntilTomorrow.Hours:D2}:{timeUntilTomorrow.Minutes:D2}:{timeUntilTomorrow.Seconds:D2}";
            }
            else
            {
                tomorrowText.gameObject.SetActive(false);
                countdownText.gameObject.SetActive(false);
            }
        }

        private void CloseSplash()
        {
            splashPanel.SetActive(false);
        }
    }
}
