namespace Project.UI
{
    using UnityEngine;
    using UnityEngine.UI;
    using TMPro;

    public enum ProfileTab
    {
        Statistics,
        Inventory
    }

    public class ProfilePageController : MonoBehaviour
    {
        [Header("Tab Buttons")]
        [SerializeField] private Button statisticsTab;
        [SerializeField] private Button inventoryTab;
        
        [Header("Content Areas")]
        [SerializeField] private GameObject statisticsContent;
        [SerializeField] private GameObject inventoryContent;
        
        [Header("Statistics UI")]
        [SerializeField] private TextMeshProUGUI rankText;
        [SerializeField] private TextMeshProUGUI pointsText;
        [SerializeField] private TextMeshProUGUI correctAnswersText;
        [SerializeField] private TextMeshProUGUI kingOfDayText;
        
        public void Init()
        {
            if (statisticsTab != null)
                statisticsTab.onClick.AddListener(() => SetTab(ProfileTab.Statistics));
            if (inventoryTab != null)
                inventoryTab.onClick.AddListener(() => SetTab(ProfileTab.Inventory));
            
            SetTab(ProfileTab.Statistics);
            LoadStatistics();
        }
        
        private void SetTab(ProfileTab tab)
        {
            if (statisticsContent != null)
                statisticsContent.SetActive(tab == ProfileTab.Statistics);
            if (inventoryContent != null)
                inventoryContent.SetActive(tab == ProfileTab.Inventory);
        }
        
        private void LoadStatistics()
        {
            if (rankText != null)
                rankText.text = "10";
            if (pointsText != null)
                pointsText.text = "10000";
            if (correctAnswersText != null)
                correctAnswersText.text = "100";
            if (kingOfDayText != null)
                kingOfDayText.text = "1";
        }
        
        public void Open() { }
        public void Close() { }
        public void Build() { }
        public void ApplyState() { }
    }
}
