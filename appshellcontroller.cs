namespace Project.UI
{
    using UnityEngine;
    using Project.Core;
    using Project.Systems;

    public class AppShellController : MonoBehaviour
    {
        [Header("Controllers")]
        [SerializeField] private HeaderController headerController;
        [SerializeField] private ProfileBarController profileBarController;
        [SerializeField] private FooterNavController footerNavController;
        [SerializeField] private DailySplashRewardController dailySplashRewardController;
        
        [Header("Content")]
        [SerializeField] private Transform contentRoot;
        
        [Header("Services")]
        [SerializeField] private DailyRewardService dailyRewardService;
        
        private void Start()
        {
            Init();
        }
        
        public void Init()
        {
            headerController.Init();
            profileBarController.Init();
            footerNavController.Init();
            dailySplashRewardController.Init();

            // Fetch the global DailyRewardService from BootLoader if not assigned via inspector
            if (dailyRewardService == null)
            {
                var boot = BootLoader.Instance;
                if (boot != null)
                {
                    dailyRewardService = boot.GetDailyRewardService();
                }
            }

            if (dailyRewardService != null)
            {
                // Subscribe to daily reward events
                dailyRewardService.OnRewardAvailable += ShowDailyReward;
                // Check if should show daily reward on first entry
                if (dailyRewardService.ShouldShowReward())
                {
                    ShowDailyReward();
                }
            }
        }
        
        private void ShowDailyReward()
        {
            dailySplashRewardController.Open();
        }
        
        public void Open() { }
        public void Close() { }
        public void Build() { }
        public void ApplyState() { }
    }
}