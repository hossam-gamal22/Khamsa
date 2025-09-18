using UnityEngine;
using System;

namespace Project.Core
{
    public class DailyRewardService : MonoBehaviour
    {
        public event Action OnRewardAvailable;

        public void Init()
        {
            // Simulate reward availability (could use real date-based logic)
            CheckRewardAvailability();
        }

        public void CheckRewardAvailability()
        {
            bool shouldShow = ShouldShowReward();

            if (shouldShow)
            {
                OnRewardAvailable?.Invoke();
            }
        }

        public bool ShouldShowReward()
        {
            // Basic check: has today's reward been claimed?
            int today = GetTodayIndex();
            return !IsDayClaimed(today);
        }

        public int GetTodayIndex()
        {
            return Mathf.Clamp(PlayerPrefs.GetInt("DailyRewardDay", 0), 0, 7);
        }

        public bool IsDayClaimed(int dayIndex)
        {
            return PlayerPrefs.GetInt($"RewardClaimed_{dayIndex}", 0) == 1;
        }

        public void MarkDayClaimed(int dayIndex)
        {
            PlayerPrefs.SetInt($"RewardClaimed_{dayIndex}", 1);
        }

        public int GetRewardAmount(int dayIndex)
        {
            int[] rewards = { 50, 75, 100, 125, 150, 200, 300, 1500 };
            return rewards[Mathf.Clamp(dayIndex, 0, rewards.Length - 1)];
        }

        public TimeSpan GetTimeUntilNextDay()
        {
            DateTime now = DateTime.Now;
            DateTime tomorrow = now.Date.AddDays(1);
            return tomorrow - now;
        }
    }
}
