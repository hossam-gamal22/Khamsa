namespace Project.UI
{
    using UnityEngine;
    using UnityEngine.UI;
    using Project.Core;
    using Project.UI.Views;

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
            if (charactersTab != null)
                charactersTab.onClick.AddListener(() => SetTab(ShopTab.Characters));
            if (stickersTab != null)
                stickersTab.onClick.AddListener(() => SetTab(ShopTab.Stickers));
            if (soundsTab != null)
                soundsTab.onClick.AddListener(() => SetTab(ShopTab.Sounds));
            if (coinsTab != null)
                coinsTab.onClick.AddListener(() => SetTab(ShopTab.Coins));
            
            SetTab(ShopTab.Characters);
            BuildAllGrids();
        }
        
        private void SetTab(ShopTab tab)
        {
            currentTab = tab;
            
            // Hide all content
            if (charactersContent != null) charactersContent.SetActive(false);
            if (stickersContent != null) stickersContent.SetActive(false);
            if (soundsContent != null) soundsContent.SetActive(false);
            if (coinsContent != null) coinsContent.SetActive(false);
            
            // Reset tab scales
            if (charactersTab != null) charactersTab.transform.localScale = Vector3.one;
            if (stickersTab != null) stickersTab.transform.localScale = Vector3.one;
            if (soundsTab != null) soundsTab.transform.localScale = Vector3.one;
            if (coinsTab != null) coinsTab.transform.localScale = Vector3.one;
            
            // Activate selected tab
            switch (tab)
            {
                case ShopTab.Characters:
                    if (charactersContent != null) charactersContent.SetActive(true);
                    if (charactersTab != null) charactersTab.transform.localScale = Vector3.one * 1.2f;
                    break;
                case ShopTab.Stickers:
                    if (stickersContent != null) stickersContent.SetActive(true);
                    if (stickersTab != null) stickersTab.transform.localScale = Vector3.one * 1.2f;
                    break;
                case ShopTab.Sounds:
                    if (soundsContent != null) soundsContent.SetActive(true);
                    if (soundsTab != null) soundsTab.transform.localScale = Vector3.one * 1.2f;
                    break;
                case ShopTab.Coins:
                    if (coinsContent != null) coinsContent.SetActive(true);
                    if (coinsTab != null) coinsTab.transform.localScale = Vector3.one * 1.2f;
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
            if (avatarService != null && characterGrid != null && avatarCardPrefab != null)
            {
                var avatars = avatarService.GetAllAvatars();
                
                foreach (var avatar in avatars)
                {
                    var card = Instantiate(avatarCardPrefab, characterGrid);
                    card.Setup(avatar, avatarService, coinsWallet);
                }
            }
        }
        
        private void BuildStickerGrid()
        {
            if (stickerService != null && stickerGrid != null && stickerCardPrefab != null)
            {
                var stickers = stickerService.GetAllStickers();
                
                foreach (var sticker in stickers)
                {
                    var card = Instantiate(stickerCardPrefab, stickerGrid);
                    card.Setup(sticker, stickerService, coinsWallet);
                }
            }
        }
        
        private void BuildSoundGrid()
        {
            if (soundService != null && soundGrid != null && soundCardPrefab != null)
            {
                var sounds = soundService.GetAllSounds();
                
                foreach (var sound in sounds)
                {
                    var card = Instantiate(soundCardPrefab, soundGrid);
                    card.Setup(sound, soundService, coinsWallet);
                }
            }
        }
        
        private void BuildCoinGrid()
        {
            if (coinPackageService != null && coinGrid != null && coinCardPrefab != null)
            {
                var coinPackages = coinPackageService.GetAllCoinPackages();
                
                foreach (var package in coinPackages)
                {
                    var card = Instantiate(coinCardPrefab, coinGrid);
                    card.Setup(package, coinPackageService);
                }
            }
        }
        
        public void Open() { }
        public void Close() { }
        public void Build() { }
        public void ApplyState() { }
    }
}
