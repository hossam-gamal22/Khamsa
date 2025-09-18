namespace Project.UI
{
    using UnityEngine;
    using UnityEngine.UI;
    using TMPro;
    using Project.Services;
    // Import Project.Systems to access BootLoader
    using Project.Systems;

    public enum DayItemState
    {
        Inactive,
        Active,
        Claimed
    }
    
    public class DayItem : MonoBehaviour
    {
        [Header("UI Elements")]
        [SerializeField] private Button claimButton;
        [SerializeField] private Button doubleButton;
        [SerializeField] private TextMeshProUGUI rewardText;
        [SerializeField] private TextMeshProUGUI dayText;
        [SerializeField] private GameObject dimOverlay;
        [SerializeField] private GameObject checkMark;
        [SerializeField] private CanvasGroup canvasGroup;
        
        [Header("Colors")]
        [SerializeField] private Color normalDayColor = Color.white;
        [SerializeField] private Color activeDayColor = Color.green;
        
        private int dayNumber;
        private DailySplashRewardController controller;
        private DayItemState currentState;
        private AudioService audioService;
        
        public void Init(int day, DailySplashRewardController rewardController)
        {
            dayNumber = day;
            controller = rewardController;
            audioService = BootLoader.Instance.GetAudioService();
            
            // Set day text
            if (dayText != null)
            {
                if (day <= 7)
                {
                    dayText.text = $"اليوم {day}";
                }
                else
                {
                    dayText.text = "بونص";
                }
            }
            
            claimButton.onClick.AddListener(() => {
                audioService?.PlayButtonClick();
                controller.OnDayItemClicked(dayNumber);
            });
            
            doubleButton.onClick.AddListener(() => {
                audioService?.PlayButtonClick();
                controller.OnDoubleReward(dayNumber);
            });
        }
        
        public void ApplyState(DayItemState state)
        {
            currentState = state;
            
            switch (state)
            {
                case DayItemState.Active:
                    SetActiveState();
                    break;
                    
                case DayItemState.Claimed:
                    SetClaimedState();
                    break;
                    
                case DayItemState.Inactive:
                    SetInactiveState();
                    break;
            }
        }
        
        private void SetActiveState()
        {
            // Visual setup
            dimOverlay.SetActive(false);
            checkMark.SetActive(false);
            claimButton.interactable = true;
            doubleButton.interactable = false;
            canvasGroup.alpha = 1f;
            
            // Day text color (green for active)
            if (dayText != null)
            {
                dayText.color = activeDayColor;
            }
            
            // Scale animation for active day
            LeanTween.scale(gameObject, Vector3.one * 1.2f, 0.3f)
                .setEase(LeanTweenType.easeOutBack);
            
            // Pulsing glow effect
            StartPulseEffect();
        }
        
        private void SetClaimedState()
        {
            // Visual setup
            dimOverlay.SetActive(true);
            checkMark.SetActive(true);
            claimButton.interactable = false;
            doubleButton.interactable = true;
            canvasGroup.alpha = 0.7f;
            
            // Day text color (normal for claimed)
            if (dayText != null)
            {
                dayText.color = normalDayColor;
            }
            
            // Update text
            if (rewardText != null)
            {
                rewardText.text = "استلمت";
            }
            
            // Reset scale
            transform.localScale = Vector3.one;
            StopPulseEffect();
        }
        
        private void SetInactiveState()
        {
            // Visual setup
            dimOverlay.SetActive(true);
            checkMark.SetActive(false);
            claimButton.interactable = false;
            doubleButton.interactable = false;
            canvasGroup.alpha = 0.5f;
            
            // Day text color (normal for inactive)
            if (dayText != null)
            {
                dayText.color = normalDayColor;
            }
            
            // Reset scale
            transform.localScale = Vector3.one;
            StopPulseEffect();
        }
        
        private void StartPulseEffect()
        {
            // Gentle pulsing effect for active day
            LeanTween.alpha(canvasGroup.gameObject, 0.8f, 1f)
                .setLoopPingPong()
                .setEase(LeanTweenType.easeInOutSine);
        }
        
        private void StopPulseEffect()
        {
            LeanTween.cancel(canvasGroup.gameObject);
        }
        
        private void OnDestroy()
        {
            StopPulseEffect();
            LeanTween.cancel(gameObject);
        }
    }
}