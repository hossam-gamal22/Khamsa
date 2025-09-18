namespace Project.UI
{
    using UnityEngine;
    using UnityEngine.UI;
    using Project.Systems;

    public class MusicToggle : MonoBehaviour
    {
        [Header("Toggle Elements")]
        [SerializeField] private Button toggleButton;
        [SerializeField] private RectTransform handle;
        [SerializeField] private Image background;
        [SerializeField] private Image handleImage;

        [Header("Positions")]
        [SerializeField] private float onPositionX = 20f;
        [SerializeField] private float offPositionX = -20f;

        [Header("Colors")]
        [SerializeField] private Color onBackgroundColor = Color.green;
        [SerializeField] private Color offBackgroundColor = Color.gray;
        [SerializeField] private Color onHandleColor = Color.white;
        [SerializeField] private Color offHandleColor = Color.white;

        [Header("Animation")]
        [SerializeField] private float animationDuration = 0.3f;
        [SerializeField] private LeanTweenType easeType = LeanTweenType.easeOutBack;

        private bool isOn;
        private AudioService audioService;

        private void Start()
        {
            var bootLoader = BootLoader.Instance;
            if (bootLoader != null)
            {
                audioService = bootLoader.GetAudioService();
            }

            if (audioService != null)
            {
                isOn = audioService.IsMusicEnabled();
            }

            if (toggleButton != null)
                toggleButton.onClick.AddListener(OnToggleClicked);

            SetVisualState(false);
        }

        private void OnToggleClicked()
        {
            isOn = !isOn;

            audioService?.SetMusicEnabled(isOn);
            audioService?.PlayButtonClick();

            SetVisualState(true);
        }

        private void SetVisualState(bool animate)
        {
            float targetPositionX = isOn ? onPositionX : offPositionX;
            Color targetBackgroundColor = isOn ? onBackgroundColor : offBackgroundColor;
            Color targetHandleColor = isOn ? onHandleColor : offHandleColor;

            if (animate && handle != null && background != null && handleImage != null)
            {
                LeanTween.moveLocalX(handle.gameObject, targetPositionX, animationDuration)
                    .setEase(easeType);

                LeanTween.value(gameObject, UpdateBackgroundColor, background.color, targetBackgroundColor, animationDuration)
                    .setEase(LeanTweenType.easeOutQuart);

                LeanTween.value(gameObject, UpdateHandleColor, handleImage.color, targetHandleColor, animationDuration)
                    .setEase(LeanTweenType.easeOutQuart);

                LeanTween.scale(handle.gameObject, Vector3.one * 1.2f, animationDuration * 0.5f)
                    .setEase(LeanTweenType.easeOutBack)
                    .setOnComplete(() =>
                    {
                        LeanTween.scale(handle.gameObject, Vector3.one, animationDuration * 0.5f)
                            .setEase(LeanTweenType.easeOutBack);
                    });
            }
            else if (handle != null && background != null && handleImage != null)
            {
                Vector3 pos = handle.localPosition;
                pos.x = targetPositionX;
                handle.localPosition = pos;

                background.color = targetBackgroundColor;
                handleImage.color = targetHandleColor;
            }
        }

        private void UpdateBackgroundColor(Color color)
        {
            if (background != null)
                background.color = color;
        }

        private void UpdateHandleColor(Color color)
        {
            if (handleImage != null)
                handleImage.color = color;
        }

        public void SetState(bool enabled)
        {
            isOn = enabled;
            SetVisualState(true);
        }
    }
}
