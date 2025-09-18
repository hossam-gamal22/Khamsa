using UnityEngine;
using System;

namespace Project.Core
{
    public class RewardedAdService : MonoBehaviour
    {
        private const string LastAdTimeKey = "LastRewardedAdTime";
        private const int cooldownMinutes = 10;

        public void Init()
        {
            // Placeholder: Init any ad SDK if needed
        }

        public bool IsAdReady()
        {
            return CanShowAd();
        }

        public bool CanShowAd()
        {
            return GetRemainingCooldown().TotalSeconds <= 0;
        }

        public TimeSpan GetRemainingCooldown()
        {
            string lastShown = PlayerPrefs.GetString(LastAdTimeKey, "");

            if (string.IsNullOrEmpty(lastShown))
                return TimeSpan.Zero;

            DateTime lastTime = DateTime.Parse(lastShown);
            TimeSpan elapsed = DateTime.Now - lastTime;
            return TimeSpan.FromMinutes(cooldownMinutes) - elapsed;
        }

        public void ShowRewardedAd(Action<int> onComplete)
        {
            if (!CanShowAd())
            {
                Debug.Log("Ad cooldown still active.");
                return;
            }

            // Simulate ad watched successfully
            int rewardAmount = 25; // or pull from config later
            PlayerPrefs.SetString(LastAdTimeKey, DateTime.Now.ToString());
            Debug.Log("Rewarded Ad completed. Reward: " + rewardAmount);

            onComplete?.Invoke(rewardAmount);
        }
    }
}
