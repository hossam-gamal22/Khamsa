namespace Project.UI
{
    using UnityEngine;
    using UnityEngine.UI;
    using TMPro;
    using Project.Services;
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
            audioService = BootLoader.Instance.GetAudioService();
            
            closeButton.onClick.AddListener(Close);
            facebookButton.onClick.AddListener(OnFacebookClicked);
            
            // Setup initial state
            splashPanel.SetActive(false);
        }
        
        public void Open()
        {
            splashPanel.SetActive(true);
            
            // Animate in
            canvasGroup.alpha = 0f;
            LeanTween.alphaCanvas(canvasGroup, 1f, 0.3f)
                .setEase(LeanTweenType.easeOutQuart);
            
            contentPanel.localScale = Vector3.zero;
            LeanTween.scale(contentPanel.gameObject, Vector3.one, 0.4f)
                .setEase(LeanTweenType.easeOutBack);
        }
        
        public void Close()
        {
            audioService?.PlayButtonClick();
            
            // Animate out
            LeanTween.alphaCanvas(canvasGroup, 0f, 0.2f)
                .setEase(LeanTweenType.easeInQuart)
                .setOnComplete(() => splashPanel.SetActive(false));
            
            LeanTween.scale(contentPanel.gameObject, Vector3.zero, 0.3f)
                .setEase(LeanTweenType.easeInBack);
        }
        
        private void OnFacebookClicked()
        {
            audioService?.PlayButtonClick();
            
            // Open Facebook URL
            Application.OpenURL(facebookURL);
            
            Debug.Log($"Opening Facebook page: {facebookURL}");
        }
        
        public void Build() { }
        public void ApplyState() { }
    }
}