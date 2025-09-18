namespace Project.UI
{
    using UnityEngine;
    using UnityEngine.UI;
    using TMPro;
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
        private RewardedAdService rewardedAdService;
        private bool isOpen = false;

        public void Init()
        {
            var bootLoader = BootLoader.Instance;
            if (bootLoader != null)
            {
                audioService = bootLoader.GetAudioService();
                rewardedAdService = bootLoader.GetRewardedAdService();
            }

            if (exitButton != null)
                exitButton.onClick.AddListener(Close);

            if (popupPanel != null)
                popupPanel.SetActive(false);

            if (messageText != null)
            {
                messageText.text = "يجب الانتظار قبل مشاهدة إعلان آخر";
            }
        }

        public void ShowCooldown(int remainingMinutes, int remainingSeconds)
        {
            if (isOpen) return;

            isOpen = true;

            if (cooldownText != null)
            {
                cooldownText.text = $"{remainingMinutes:D2}:{remainingSeconds:D2}";
            }

            if (popupPanel != null)
                popupPanel.SetActive(true);

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

            audioService?.PlayError();
            StartCountdownUpdate();
        }

        public void Close()
        {
            if (!isOpen) return;

            audioService?.PlayButtonClick();

            if (canvasGroup != null && popupPanel != null)
            {
                LeanTween.alphaCanvas(canvasGroup, 0f, 0.2f)
                    .setEase(LeanTweenType.easeInQuart)
                    .setOnComplete(() =>
                    {
                        popupPanel.SetActive(false);
                        isOpen = false;
                    });
            }

            if (contentPanel != null)
            {
                LeanTween.scale(contentPanel.gameObject, Vector3.zero, 0.3f)
                    .setEase(LeanTweenType.easeInBack);
            }

            StopCountdownUpdate();
        }

        private void StartCountdownUpdate()
        {
            InvokeRepeating(nameof(UpdateCountdown), 1f, 1f);
        }

        private void StopCountdownUpdate()
        {
            CancelInvoke(nameof(UpdateCountdown));
        }

        private void UpdateCountdown()
        {
            if (!isOpen || rewardedAdService == null) return;

            if (rewardedAdService.CanShowAd())
            {
                Close();
                return;
            }

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
