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
            homePageController.Init();
            shopPageController.Init();
            friendsPageController.Init();
            profilePageController.Init();
            
            NavigateToPage(PageType.Home);
        }
        
        public void NavigateToPage(PageType page)
        {
            // Close current page
            switch (currentPage)
            {
                case PageType.Home:
                    homePageController.Close();
                    homePage.SetActive(false);
                    break;
                case PageType.Shop:
                    shopPageController.Close();
                    shopPage.SetActive(false);
                    break;
                case PageType.Friends:
                    friendsPageController.Close();
                    friendsPage.SetActive(false);
                    break;
                case PageType.Profile:
                    profilePageController.Close();
                    profilePage.SetActive(false);
                    break;
            }
            
            // Open new page
            currentPage = page;
            switch (page)
            {
                case PageType.Home:
                    homePage.SetActive(true);
                    homePageController.Open();
                    break;
                case PageType.Shop:
                    shopPage.SetActive(true);
                    shopPageController.Open();
                    break;
                case PageType.Friends:
                    friendsPage.SetActive(true);
                    friendsPageController.Open();
                    break;
                case PageType.Profile:
                    profilePage.SetActive(true);
                    profilePageController.Open();
                    break;
            }
        }
        
        public PageType GetCurrentPage() => currentPage;
    }
}