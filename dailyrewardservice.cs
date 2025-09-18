namespace Project.Core
{
    using UnityEngine;
    using System;

    [System.Serializable]
    public class DailyRewardService : MonoBehaviour
    {
        [Header("Reward Configuration")]
        [SerializeField] private int[] dailyRewards = { 20, 50, 150, 200, 300, 500, 1500 };
        [SerializeField] private int bonusReward = 1500;
        
        private int currentDay;
        private bool hasClaimedToday;
        private DateTime lastClaimDate;

        // Tracks the player's consecutive daily reward claim streak. Updated whenever a reward is claimed.
        private int streak;
        
        public event System.Action<int> OnRewardClaimed;
        public event System.Action OnRewardAvailable;
        
        public void Init()
        {
            currentDay = PlayerPrefs.GetInt("DailyRewardDay", 1);
            string lastClaimStr = PlayerPrefs.GetString("LastClaimDate", "");
            
            if (DateTime.TryParse(lastClaimStr, out lastClaimDate))
            {
                hasClaimedToday = (DateTime.Now.Date == lastClaimDate.Date);
            }
            else
            {
                hasClaimedToday = false;
                lastClaimDate = DateTime.MinValue;
            }

            // Load current streak from prefs. If no streak saved, defaults to 0.
            streak = PlayerPrefs.GetInt("DailyRewardStreak", 0);
            
            // Check if reward is available
            if (!hasClaimedToday && ShouldShowReward())
            {
                OnRewardAvailable?.Invoke();
            }
        }
        
        public bool ShouldShowReward()
        {
            return !hasClaimedToday;
        }
        
        public bool CanClaimReward()
        {
            return !hasClaimedToday;
        }
        
        public int ClaimReward()
        {
            if (!CanClaimReward()) return 0;

            int reward = GetCurrentDayReward();

            // Mark that the reward has been claimed today
            hasClaimedToday = true;

            // Determine the current time and the previous claim date
            DateTime now = DateTime.Now;
            DateTime previousClaimDate = lastClaimDate;

            // Update streak: if the previous claim was exactly yesterday, increment; otherwise reset to 1.
            if (previousClaimDate != DateTime.MinValue && previousClaimDate.Date.AddDays(1) == now.Date)
            {
                streak++;
            }
            else
            {
                streak = 1;
            }
            PlayerPrefs.SetInt("DailyRewardStreak", streak);

            // Update last claim date and persist
            lastClaimDate = now;
            PlayerPrefs.SetString("LastClaimDate", lastClaimDate.ToString());

            // Advance day for the next reward cycle
            currentDay = (currentDay % 7) + 1;
            PlayerPrefs.SetInt("DailyRewardDay", currentDay);

            // Invoke event
            OnRewardClaimed?.Invoke(reward);
            return reward;
        }
        
        public int GetCurrentDayReward()
        {
            if (currentDay <= dailyRewards.Length)
                return dailyRewards[currentDay - 1];
            return bonusReward;
        }
        
        public int GetCurrentDay() => currentDay;
        public bool HasClaimedToday() => hasClaimedToday;

        /// <summary>
        /// Returns the index of today's reward (1-7), equal to the current day.
        /// </summary>
        public int TodayIndex => currentDay;

        /// <summary>
        /// Returns whether the daily reward has been claimed today.
        /// </summary>
        public bool ClaimedToday => hasClaimedToday;

        /// <summary>
        /// Returns the player's current daily reward streak (number of consecutive days claimed).
        /// </summary>
        public int Streak => streak;
        
        public void Open() { }
        public void Close() { }
        public void Build() { }
        public void ApplyState() { }
    }
}