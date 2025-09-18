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

            streak = PlayerPrefs.GetInt("DailyRewardStreak", 0);
            
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
            hasClaimedToday = true;

            DateTime now = DateTime.Now;
            DateTime previousClaimDate = lastClaimDate;

            if (previousClaimDate != DateTime.MinValue && previousClaimDate.Date.AddDays(1) == now.Date)
            {
                streak++;
            }
            else
            {
                streak = 1;
            }
            PlayerPrefs.SetInt("DailyRewardStreak", streak);

            lastClaimDate = now;
            PlayerPrefs.SetString("LastClaimDate", lastClaimDate.ToString());

            currentDay = (currentDay % 7) + 1;
            PlayerPrefs.SetInt("DailyRewardDay", currentDay);

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
        public int TodayIndex => currentDay;
        public bool ClaimedToday => hasClaimedToday;
        public int Streak => streak;
        
        public void Open() { }
        public void Close() { }
        public void Build() { }
        public void ApplyState() { }
    }
}