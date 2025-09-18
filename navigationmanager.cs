namespace Project.Main_Screen
{
    using UnityEngine;
    using Project.UI;

    public enum PageType
    {
        Home,
        Shop,
        Friends,
        Profile
    }

    public class NavigationManager : MonoBehaviour
    {
        [Header("Page Controllers")]
        [SerializeField] private HomePageController homePageController;
        [SerializeField] private ShopPageController shopPageController;
        [SerializeField] private FriendsPageController friendsPageController;
        [SerializeField] private ProfilePageController profilePageController;
        
        [Header("Page GameObjects")]
        [SerializeField] private GameObject homePage;
        [SerializeField] private GameObject shopPage;
        [SerializeField] private GameObject friendsPage;
        [SerializeField] private GameObject profilePage;
        
        private PageType currentPage = PageType.Home;
        
        public void Init()
        {
            if (homePageController != null)
                homePageController.Init();
            if (shopPageController != null)
                shopPageController.Init();
            if (friendsPageController != null)
                friendsPageController.Init();
            if (profilePageController != null)
                profilePageController.Init();
            
            NavigateToPage(PageType.Home);
        }
        
        public void NavigateToPage(PageType page)
        {
            // Close current page
            switch (currentPage)
            {
                case PageType.Home:
                    if (homePageController != null) homePageController.Close();
                    if (homePage != null) homePage.SetActive(false);
                    break;
                case PageType.Shop:
                    if (shopPageController != null) shopPageController.Close();
                    if (shopPage != null) shopPage.SetActive(false);
                    break;
                case PageType.Friends:
                    if (friendsPageController != null) friendsPageController.Close();
                    if (friendsPage != null) friendsPage.SetActive(false);
                    break;
                case PageType.Profile:
                    if (profilePageController != null) profilePageController.Close();
                    if (profilePage != null) profilePage.SetActive(false);
                    break;
            }
            
            // Open new page
            currentPage = page;
            switch (page)
            {
                case PageType.Home:
                    if (homePage != null) homePage.SetActive(true);
                    if (homePageController != null) homePageController.Open();
                    break;
                case PageType.Shop:
                    if (shopPage != null) shopPage.SetActive(true);
                    if (shopPageController != null) shopPageController.Open();
                    break;
                case PageType.Friends:
                    if (friendsPage != null) friendsPage.SetActive(true);
                    if (friendsPageController != null) friendsPageController.Open();
                    break;
                case PageType.Profile:
                    if (profilePage != null) profilePage.SetActive(true);
                    if (profilePageController != null) profilePageController.Open();
                    break;
            }
        }
        
        public PageType GetCurrentPage() => currentPage;
    }
}