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
            statisticsTab.onClick.AddListener(() => SetTab(ProfileTab.Statistics));
            inventoryTab.onClick.AddListener(() => SetTab(ProfileTab.Inventory));
            
            SetTab(ProfileTab.Statistics);
            LoadStatistics();
        }
        
        private void SetTab(ProfileTab tab)
        {
            statisticsContent.SetActive(tab == ProfileTab.Statistics);
            inventoryContent.SetActive(tab == ProfileTab.Inventory);
        }
        
        private void LoadStatistics()
        {
            // TODO: Load actual statistics from save data
            rankText.text = "10";
            pointsText.text = "10000";
            correctAnswersText.text = "100";
            kingOfDayText.text = "1";
        }
        
        public void Open() { }
        public void Close() { }
        public void Build() { }
        public void ApplyState() { }
    }
}