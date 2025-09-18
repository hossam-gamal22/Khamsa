namespace Project.UI
{
    using UnityEngine;
    using UnityEngine.UI;
    using TMPro;
    using Project.Services;
    // Import Project.Systems to access BootLoader
    using Project.Systems;

    public class CooldownPopupController : MonoBehaviour
    {
        [Header("UI Elements")]
        [SerializeField] private GameObject popupPanel;
        [SerializeField] private Button exitButton;
        [SerializeField] private TextMeshProUGUI cooldownText;
        [SerializeField] private TextMeshProUGUI messageText;

        [Header("Animation")]
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private RectTransform contentPanel;

        private AudioService audioService;
        private bool isOpen = false;

        public void Init()
        {
            audioService = BootLoader.Instance.GetAudioService();

            // Setup button
            exitButton.onClick.AddListener(Close);

            // Setup initial state
            popupPanel.SetActive(false);

            // Setup message text
            if (messageText != null)
            {
                messageText.text = "يجب الانتظار قبل مشاهدة إعلان آخر";
            }
        }

        public void ShowCooldown(int remainingMinutes, int remainingSeconds)
        {
            if (isOpen) return;

            isOpen = true;

            // Update countdown text
            if (cooldownText != null)
            {
                cooldownText.text = $"{remainingMinutes:D2}:{remainingSeconds:D2}";
            }

            // Show popup with animation
            popupPanel.SetActive(true);

            // Fade in animation
            canvasGroup.alpha = 0f;
            LeanTween.alphaCanvas(canvasGroup, 1f, 0.3f)
                .setEase(LeanTweenType.easeOutQuart);

            // Scale in animation
            contentPanel.localScale = Vector3.zero;
            LeanTween.scale(contentPanel.gameObject, Vector3.one, 0.4f)
                .setEase(LeanTweenType.easeOutBack);

            // Play error sound
            audioService?.PlayError();

            // Start countdown update
            StartCountdownUpdate();
        }

        public void Close()
        {
            if (!isOpen) return;

            audioService?.PlayButtonClick();

            // Animate out
            LeanTween.alphaCanvas(canvasGroup, 0f, 0.2f)
                .setEase(LeanTweenType.easeInQuart)
                .setOnComplete(() =>
                {
                    popupPanel.SetActive(false);
                    isOpen = false;
                });

            LeanTween.scale(contentPanel.gameObject, Vector3.zero, 0.3f)
                .setEase(LeanTweenType.easeInBack);

            // Stop countdown
            StopCountdownUpdate();
        }

        private void StartCountdownUpdate()
        {
            // Update countdown every second
            InvokeRepeating(nameof(UpdateCountdown), 1f, 1f);
        }

        private void StopCountdownUpdate()
        {
            CancelInvoke(nameof(UpdateCountdown));
        }

        private void UpdateCountdown()
        {
            if (!isOpen) return;

            // Get remaining time from RewardedAdService
            var rewardedAdService = BootLoader.Instance.GetRewardedAdService();

            if (rewardedAdService.CanShowAd())
            {
                // Cooldown finished
                Close();
                return;
            }

            // Calculate remaining time (simplified - you'd get this from the service)
            System.TimeSpan remainingTime = rewardedAdService.GetRemainingCooldown();

            if (cooldownText != null)
            {
                cooldownText.text = $"{remainingTime.Minutes:D2}:{remainingTime.Seconds:D2}";
            }
        }

        private void OnDestroy()
        {
            StopCountdownUpdate();
        }

        public void Open() => throw new System.NotImplementedException("Use ShowCooldown instead");
        public void Build() { }
        public void ApplyState() { }
    }
}