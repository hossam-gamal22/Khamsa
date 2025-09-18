namespace Project.UI
{
    using UnityEngine;
    using UnityEngine.UI;

    public class HomePageController : MonoBehaviour
    {
        [Header("Game Mode Buttons")]
        [SerializeField] private Button manyGamesButton;
        [SerializeField] private Button dailyChallengeButton;
        
        public void Init()
        {
            if (manyGamesButton != null)
                manyGamesButton.onClick.AddListener(OnManyGamesClicked);
            if (dailyChallengeButton != null)
                dailyChallengeButton.onClick.AddListener(OnDailyChallengeClicked);
        }
        
        private void OnManyGamesClicked()
        {
            Debug.Log("Many Games clicked");
        }
        
        private void OnDailyChallengeClicked()
        {
            Debug.Log("Daily Challenge clicked");
        }
        
        public void Open() { }
        public void Close() { }
        public void Build() { }
        public void ApplyState() { }
    }
}
