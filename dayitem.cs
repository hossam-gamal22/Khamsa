namespace Project.UI
{
    using UnityEngine;
    using UnityEngine.UI;
    using TMPro;
    using Project.Core;
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
            
            var bootLoader = BootLoader.Instance;
            if (bootLoader != null)
            {
                audioService = bootLoader.GetAudioService();
            }
            
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
            
            if (claimButton != null)
            {
                claimButton.onClick.AddListener(() => {
                    audioService?.PlayButtonClick();
                    controller.OnDayItemClicked(dayNumber);
                });
            }
            
            if (doubleButton != null)
            {
                doubleButton.onClick.AddListener(() => {
                    audioService?.PlayButtonClick();
                    controller.OnDoubleReward(dayNumber);
                });
            }
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
            if (dimOverlay != null) dimOverlay.SetActive(false);
            if (checkMark != null) checkMark.SetActive(false);
            if (claimButton != null) claimButton.interactable = true;
            if (doubleButton != null) doubleButton.interactable = false;
            if (canvasGroup != null) canvasGroup.alpha = 1f;
            
            if (dayText != null)
            {
                dayText.color = activeDayColor;
            }
            
            LeanTween.scale(gameObject, Vector3.one * 1.2f, 0.3f)
                .setEase(LeanTweenType.easeOutBack);
            
            StartPulseEffect();
        }
        
        private void SetClaimedState()
        {
            if (dimOverlay != null) dimOverlay.SetActive(true);
            if (checkMark != null) checkMark.SetActive(true);
            if (claimButton != null) claimButton.interactable = false;
            if (doubleButton != null) doubleButton.interactable = true;
            if (canvasGroup != null) canvasGroup.alpha = 0.7f;
            
            if (dayText != null)
            {
                dayText.color = normalDayColor;
            }
            
            if (rewardText != null)
            {
                rewardText.text = "استلمت";
            }
            
            transform.localScale = Vector3.one;
            StopPulseEffect();
        }
        
        private void SetInactiveState()
        {
            if (dimOverlay != null) dimOverlay.SetActive(true);
            if (checkMark != null) checkMark.SetActive(false);
            if (claimButton != null) claimButton.interactable = false;
            if (doubleButton != null) doubleButton.interactable = false;
            if (canvasGroup != null) canvasGroup.alpha = 0.5f;
            
            if (dayText != null)
            {
                dayText.color = normalDayColor;
            }
            
            transform.localScale = Vector3.one;
            StopPulseEffect();
        }
        
        private void StartPulseEffect()
        {
            if (canvasGroup != null)
            {
                LeanTween.alpha(canvasGroup.gameObject, 0.8f, 1f)
                    .setLoopPingPong()
                    .setEase(LeanTweenType.easeInOutSine);
            }
        }
        
        private void StopPulseEffect()
        {
            if (canvasGroup != null)
            {
                LeanTween.cancel(canvasGroup.gameObject);
            }
        }
        
        private void OnDestroy()
        {
            StopPulseEffect();
            LeanTween.cancel(gameObject);
        }
    }
}