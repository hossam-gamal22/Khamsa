namespace Project.Services
{
    using UnityEngine;
    using System;

    [System.Serializable]
    public class RewardedAdService : MonoBehaviour
    {
        [Header("Ad Configuration")]
        [SerializeField] private int maxAdsPerDay = 10;
        [SerializeField] private int cooldownMinutes = 10;
        [SerializeField] private int coinsPerAd = 25;
        
        private int dailyAdCount;
        private DateTime lastAdTime;
        
        public event System.Action<int> OnAdWatched;
        public event System.Action OnAdFailed;
        
        public void Init()
        {
            dailyAdCount = PlayerPrefs.GetInt("DailyAdCount", 0);
            string lastAdStr = PlayerPrefs.GetString("LastAdTime", "");
            
            if (!DateTime.TryParse(lastAdStr, out lastAdTime))
            {
                lastAdTime = DateTime.MinValue;
            }
            
            // Reset daily count if new day
            if (DateTime.Now.Date != lastAdTime.Date)
            {
                dailyAdCount = 0;
                PlayerPrefs.SetInt("DailyAdCount", 0);
            }
            
            Debug.Log($"RewardedAdService initialized - Daily ads: {dailyAdCount}/{maxAdsPerDay}");
        }
        
        public bool CanShowAd()
        {
            // Check daily limit
            if (dailyAdCount >= maxAdsPerDay) return false;
            
            // Check cooldown
            if ((DateTime.Now - lastAdTime).TotalMinutes < cooldownMinutes) return false;
            
            return true;
        }
        
        public System.TimeSpan GetRemainingCooldown()
        {
            if (dailyAdCount >= maxAdsPerDay)
            {
                // Return time until next day
                var tomorrow = DateTime.Now.Date.AddDays(1);
                return tomorrow - DateTime.Now;
            }
            
            var cooldownEnd = lastAdTime.AddMinutes(cooldownMinutes);
            var remaining = cooldownEnd - DateTime.Now;
            
            return remaining.TotalSeconds > 0 ? remaining : TimeSpan.Zero;
        }
        
        public int GetRemainingAdsToday()
        {
            return Mathf.Max(0, maxAdsPerDay - dailyAdCount);
        }
        
        public void ShowRewardedAd(System.Action<int> onSuccess)
        {
            if (!CanShowAd())
            {
                Debug.Log("Cannot show ad - cooldown or daily limit reached");
                OnAdFailed?.Invoke();
                return;
            }
            
            // TODO: Integrate with AdMob
            // For now, simulate ad completion
            Debug.Log("Showing rewarded ad...");
            
            // Simulate ad watching delay
            StartCoroutine(SimulateAdWatching(onSuccess));
        }
        
        private System.Collections.IEnumerator SimulateAdWatching(System.Action<int> onSuccess)
        {
            // Simulate 3 second ad
            yield return new WaitForSeconds(3f);
            
            // Simulate successful ad completion
            dailyAdCount++;
            lastAdTime = DateTime.Now;
            PlayerPrefs.SetInt("DailyAdCount", dailyAdCount);
            PlayerPrefs.SetString("LastAdTime", lastAdTime.ToString());
            
            Debug.Log($"Ad completed! Rewarding {coinsPerAd} coins. Ads today: {dailyAdCount}/{maxAdsPerDay}");
            
            OnAdWatched?.Invoke(coinsPerAd);
            onSuccess?.Invoke(coinsPerAd);
        }
        
        // Inspector methods for testing
        [ContextMenu("Reset Daily Ads")]
        private void ResetDailyAds()
        {
            dailyAdCount = 0;
            PlayerPrefs.SetInt("DailyAdCount", 0);
            Debug.Log("Daily ads reset");
        }
        
        [ContextMenu("Reset Cooldown")]
        private void ResetCooldown()
        {
            lastAdTime = DateTime.MinValue;
            PlayerPrefs.SetString("LastAdTime", "");
            Debug.Log("Ad cooldown reset");
        }
        
        public void Open() { }
        public void Close() { }
        public void Build() { }
        public void ApplyState() { }
    }
}