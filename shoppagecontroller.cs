namespace Project.UI
{
    using UnityEngine;
    using UnityEngine.UI;
    using Project.Core;
    using Project.UI.Views;  // ADD THIS LINE

    public enum ShopTab
    {
        Characters,
        Stickers, 
        Sounds,
        Coins
    }

    public class ShopPageController : MonoBehaviour
    {
        [Header("Tab Buttons")]
        [SerializeField] private Button charactersTab;
        [SerializeField] private Button stickersTab;
        [SerializeField] private Button soundsTab;
        [SerializeField] private Button coinsTab;
        
        [Header("Content Areas")]
        [SerializeField] private GameObject charactersContent;
        [SerializeField] private GameObject stickersContent;
        [SerializeField] private GameObject soundsContent;
        [SerializeField] private GameObject coinsContent;
        
        [Header("Character Grid")]
        [SerializeField] private Transform characterGrid;
        [SerializeField] private AvatarCard avatarCardPrefab;
        
        [Header("Sticker Grid")]
        [SerializeField] private Transform stickerGrid;
        [SerializeField] private StickerCard stickerCardPrefab;
        
        [Header("Sound Grid")]
        [SerializeField] private Transform soundGrid;
        [SerializeField] private SoundCard soundCardPrefab;
        
        [Header("Coin Grid")]
        [SerializeField] private Transform coinGrid;
        [SerializeField] private CoinCard coinCardPrefab;
        
        [Header("Services")]
        [SerializeField] private AvatarService avatarService;
        [SerializeField] private StickerService stickerService;
        [SerializeField] private SoundService soundService;
        [SerializeField] private CoinPackageService coinPackageService;
        [SerializeField] private CoinsWallet coinsWallet;
        
        private ShopTab currentTab = ShopTab.Characters;
        
        public void Init()
        {
            charactersTab.onClick.AddListener(() => SetTab(ShopTab.Characters));
            stickersTab.onClick.AddListener(() => SetTab(ShopTab.Stickers));
            soundsTab.onClick.AddListener(() => SetTab(ShopTab.Sounds));
            coinsTab.onClick.AddListener(() => SetTab(ShopTab.Coins));
            
            SetTab(ShopTab.Characters);
            BuildAllGrids();
        }
        
        private void SetTab(ShopTab tab)
        {
            currentTab = tab;
            
            // Hide all content
            charactersContent.SetActive(false);
            stickersContent.SetActive(false);
            soundsContent.SetActive(false);
            coinsContent.SetActive(false);
            
            // Reset tab scales
            charactersTab.transform.localScale = Vector3.one;
            stickersTab.transform.localScale = Vector3.one;
            soundsTab.transform.localScale = Vector3.one;
            coinsTab.transform.localScale = Vector3.one;
            
            // Activate selected tab
            switch (tab)
            {
                case ShopTab.Characters:
                    charactersContent.SetActive(true);
                    charactersTab.transform.localScale = Vector3.one * 1.2f;
                    break;
                case ShopTab.Stickers:
                    stickersContent.SetActive(true);
                    stickersTab.transform.localScale = Vector3.one * 1.2f;
                    break;
                case ShopTab.Sounds:
                    soundsContent.SetActive(true);
                    soundsTab.transform.localScale = Vector3.one * 1.2f;
                    break;
                case ShopTab.Coins:
                    coinsContent.SetActive(true);
                    coinsTab.transform.localScale = Vector3.one * 1.2f;
                    break;
            }
        }
        
        private void BuildAllGrids()
        {
            BuildCharacterGrid();
            BuildStickerGrid();
            BuildSoundGrid();
            BuildCoinGrid();
        }
        
        private void BuildCharacterGrid()
        {
            var avatars = avatarService.GetAllAvatars();
            
            foreach (var avatar in avatars)
            {
                var card = Instantiate(avatarCardPrefab, characterGrid);
                card.Setup(avatar, avatarService, coinsWallet);
            }
        }
        
        private void BuildStickerGrid()
        {
            var stickers = stickerService.GetAllStickers();
            
            foreach (var sticker in stickers)
            {
                var card = Instantiate(stickerCardPrefab, stickerGrid);
                card.Setup(sticker, stickerService, coinsWallet);
            }
        }
        
        private void BuildSoundGrid()
        {
            var sounds = soundService.GetAllSounds();
            
            foreach (var sound in sounds)
            {
                var card = Instantiate(soundCardPrefab, soundGrid);
                card.Setup(sound, soundService, coinsWallet);
            }
        }
        
        private void BuildCoinGrid()
        {
            var coinPackages = coinPackageService.GetAllCoinPackages();
            
            foreach (var package in coinPackages)
            {
                var card = Instantiate(coinCardPrefab, coinGrid);
                card.Setup(package, coinPackageService);
            }
        }
        
        public void Open() { }
        public void Close() { }
        public void Build() { }
        public void ApplyState() { }
    }
}