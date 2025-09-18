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
            manyGamesButton.onClick.AddListener(OnManyGamesClicked);
            dailyChallengeButton.onClick.AddListener(OnDailyChallengeClicked);
        }
        
        private void OnManyGamesClicked()
        {
            // TODO: Navigate to game selection
            Debug.Log("Many Games clicked");
        }
        
        private void OnDailyChallengeClicked()
        {
            // TODO: Navigate to daily challenge
            Debug.Log("Daily Challenge clicked");
        }
        
        public void Open() { }
        public void Close() { }
        public void Build() { }
        public void ApplyState() { }
    }
}