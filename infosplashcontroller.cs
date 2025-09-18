namespace Project.UI
{
    using UnityEngine;
    using UnityEngine.UI;
    using TMPro;
    using Project.Systems;

    public class InfoSplashController : MonoBehaviour
    {
        [Header("UI Elements")]
        [SerializeField] private GameObject splashPanel;
        [SerializeField] private Button closeButton;
        [SerializeField] private Button facebookButton;
        [SerializeField] private TextMeshProUGUI infoText;
        
        [Header("Facebook Configuration")]
        [SerializeField] private string facebookURL = "https://www.facebook.com/KhamsaL3bGame";
        
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
            if (facebookButton != null)
                facebookButton.onClick.AddListener(OnFacebookClicked);
            
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
        
        private void OnFacebookClicked()
        {
            audioService?.PlayButtonClick();
            Application.OpenURL(facebookURL);
            Debug.Log($"Opening Facebook page: {facebookURL}");
        }
        
        public void Build() { }
        public void ApplyState() { }
    }
}
