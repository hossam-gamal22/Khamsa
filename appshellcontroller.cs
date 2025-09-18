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
            if (headerController != null)
                headerController.Init();
            if (profileBarController != null)
                profileBarController.Init();
            if (footerNavController != null)
                footerNavController.Init();
            if (dailySplashRewardController != null)
                dailySplashRewardController.Init();

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
                dailyRewardService.OnRewardAvailable += ShowDailyReward;
                if (dailyRewardService.ShouldShowReward())
                {
                    ShowDailyReward();
                }
            }
        }
        
        private void ShowDailyReward()
        {
            if (dailySplashRewardController != null)
            {
                dailySplashRewardController.Open();
            }
        }
        
        public void Open() { }
        public void Close() { }
        public void Build() { }
        public void ApplyState() { }
    }
}