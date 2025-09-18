namespace Project.UI
{
    using UnityEngine;
    using UnityEngine.UI;
    using Project.Core;
    using Project.Systems;

    public class SettingsSplashController : MonoBehaviour
    {
        [Header("UI Elements")]
        [SerializeField] private GameObject splashPanel;
        [SerializeField] private Button closeButton;
        [SerializeField] private MusicToggle musicToggle;
        [SerializeField] private SFXToggle sfxToggle;
        [SerializeField] private Button googleButton;
        [SerializeField] private Button appleButton;
        [SerializeField] private Button deleteAccountButton;
        
        [Header("Animation")]
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private RectTransform contentPanel;
        
        private AudioService audioService;
        
        public void Init()
        {
            var bootLoader = BootLoader.Instance;
            if (bootLoader != null)
            {
                audioService = bootLoader.GetAudioService();
            }
            
            if (closeButton != null)
                closeButton.onClick.AddListener(Close);
            if (googleButton != null)
                googleButton.onClick.AddListener(OnGoogleLogin);
            if (appleButton != null)
                appleButton.onClick.AddListener(OnAppleLogin);
            if (deleteAccountButton != null)
                deleteAccountButton.onClick.AddListener(OnDeleteAccount);
            
            if (splashPanel != null)
                splashPanel.SetActive(false);
        }
        
        public void Open()
        {
            if (splashPanel != null)
                splashPanel.SetActive(true);
                
            if (canvasGroup != null)
            {
                canvasGroup.alpha = 0f;
                LeanTween.alphaCanvas(canvasGroup, 1f, 0.3f)
                    .setEase(LeanTweenType.easeOutQuart);
            }
            
            if (contentPanel != null)
            {
                contentPanel.localScale = Vector3.zero;
                LeanTween.scale(contentPanel.gameObject, Vector3.one, 0.4f)
                    .setEase(LeanTweenType.easeOutBack);
            }
        }
        
        public void Close()
        {
            audioService?.PlayButtonClick();
            
            if (canvasGroup != null && splashPanel != null)
            {
                LeanTween.alphaCanvas(canvasGroup, 0f, 0.2f)
                    .setEase(LeanTweenType.easeInQuart)
                    .setOnComplete(() => splashPanel.SetActive(false));
            }
            
            if (contentPanel != null)
            {
                LeanTween.scale(contentPanel.gameObject, Vector3.zero, 0.3f)
                    .setEase(LeanTweenType.easeInBack);
            }
        }
        
        private void OnGoogleLogin()
        {
            audioService?.PlayButtonClick();
            Debug.Log("Google login clicked");
        }
        
        private void OnAppleLogin()
        {
            audioService?.PlayButtonClick();
            Debug.Log("Apple login clicked");
        }
        
        private void OnDeleteAccount()
        {
            audioService?.PlayButtonClick();
            Debug.Log("Delete account clicked");
        }
        
        public void Build() { }
        public void ApplyState() { }
    }
}