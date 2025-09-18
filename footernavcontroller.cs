namespace Project.UI
{
    using UnityEngine;
    using UnityEngine.UI;
    using TMPro;
    using Project.Main_Screen;
    using Project.Services;
    // Import Project.Systems to access BootLoader
    using Project.Systems;

    public class FooterNavController : MonoBehaviour
    {
        [Header("Navigation Buttons")]
        [SerializeField] private Button homeButton;
        [SerializeField] private Button shopButton;
        [SerializeField] private Button friendsButton;
        [SerializeField] private Button profileButton;

        [Header("Button Icons")]
        [SerializeField] private Image homeIcon;
        [SerializeField] private Image shopIcon;
        [SerializeField] private Image friendsIcon;
        [SerializeField] private Image profileIcon;

        [Header("Button Labels")]
        [SerializeField] private TextMeshProUGUI homeLabel;
        [SerializeField] private TextMeshProUGUI shopLabel;
        [SerializeField] private TextMeshProUGUI friendsLabel;
        [SerializeField] private TextMeshProUGUI profileLabel;

        [Header("Normal Sprites")]
        [SerializeField] private Sprite homeNormalSprite;
        [SerializeField] private Sprite shopNormalSprite;
        [SerializeField] private Sprite friendsNormalSprite;
        [SerializeField] private Sprite profileNormalSprite;

        [Header("Active Sprites")]
        [SerializeField] private Sprite homeActiveSprite;
        [SerializeField] private Sprite shopActiveSprite;
        [SerializeField] private Sprite friendsActiveSprite;
        [SerializeField] private Sprite profileActiveSprite;

        [Header("Colors")]
        [SerializeField] private Color normalColor = Color.white;
        [SerializeField] private Color activeColor = Color.green;

        [Header("Navigation")]
        [SerializeField] private NavigationManager navigationManager;

        private Button currentActiveButton;
        private AudioService audioService;

        public void Init()
        {
            // Get audio service
            audioService = BootLoader.Instance.GetAudioService();

            // Setup button listeners
            homeButton.onClick.AddListener(() => SetActiveTab(homeButton, PageType.Home));
            shopButton.onClick.AddListener(() => SetActiveTab(shopButton, PageType.Shop));
            friendsButton.onClick.AddListener(() => SetActiveTab(friendsButton, PageType.Friends));
            profileButton.onClick.AddListener(() => SetActiveTab(profileButton, PageType.Profile));

            // Set home as default
            SetActiveTab(homeButton, PageType.Home);
        }

        private void SetActiveTab(Button selectedButton, PageType pageType)
        {
            audioService?.PlayButtonClick();

            // Reset all buttons to normal state
            SetButtonState(homeButton, homeIcon, homeLabel, homeNormalSprite, false);
            SetButtonState(shopButton, shopIcon, shopLabel, shopNormalSprite, false);
            SetButtonState(friendsButton, friendsIcon, friendsLabel, friendsNormalSprite, false);
            SetButtonState(profileButton, profileIcon, profileLabel, profileNormalSprite, false);

            // Set selected button to active state
            if (selectedButton == homeButton)
                SetButtonState(homeButton, homeIcon, homeLabel, homeActiveSprite, true);
            else if (selectedButton == shopButton)
                SetButtonState(shopButton, shopIcon, shopLabel, shopActiveSprite, true);
            else if (selectedButton == friendsButton)
                SetButtonState(friendsButton, friendsIcon, friendsLabel, friendsActiveSprite, true);
            else if (selectedButton == profileButton)
                SetButtonState(profileButton, profileIcon, profileLabel, profileActiveSprite, true);

            currentActiveButton = selectedButton;

            // Navigate to page
            if (navigationManager != null)
            {
                navigationManager.NavigateToPage(pageType);
            }
        }

        private void SetButtonState(Button button, Image icon, TextMeshProUGUI label, Sprite sprite, bool isActive)
        {
            // Set sprite
            if (sprite != null)
            {
                icon.sprite = sprite;
            }

            // Set label color
            if (label != null)
            {
                label.color = isActive ? activeColor : normalColor;
            }

            // Optional: Add scale animation for active state
            if (isActive)
            {
                LeanTween.scale(button.gameObject, Vector3.one * 1.1f, 0.2f)
                    .setEase(LeanTweenType.easeOutBack);
            }
            else
            {
                LeanTween.scale(button.gameObject, Vector3.one, 0.2f)
                    .setEase(LeanTweenType.easeOutQuart);
            }
        }

        // Method to programmatically set active tab
        public void SetActiveTab(PageType pageType)
        {
            switch (pageType)
            {
                case PageType.Home:
                    SetActiveTab(homeButton, pageType);
                    break;
                case PageType.Shop:
                    SetActiveTab(shopButton, pageType);
                    break;
                case PageType.Friends:
                    SetActiveTab(friendsButton, pageType);
                    break;
                case PageType.Profile:
                    SetActiveTab(profileButton, pageType);
                    break;
            }
        }

        public void Open() { }
        public void Close() { }
        public void Build() { }
        public void ApplyState() { }
    }
}