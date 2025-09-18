namespace Project.UI.Views
{
    using UnityEngine;
    using UnityEngine.UI;
    using TMPro;

    public class FriendCard : MonoBehaviour
    {
        [Header("UI Elements")]
        [SerializeField] private Image avatarImage;
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private TextMeshProUGUI statusText;
        [SerializeField] private Button actionButton;
        [SerializeField] private Button chatButton;
        
        public void Setup(string friendName, string status, bool isOnline)
        {
            nameText.text = friendName;
            statusText.text = status;
            
            actionButton.onClick.AddListener(() => OnActionClicked(friendName));
            chatButton.onClick.AddListener(() => OnChatClicked(friendName));
        }
        
        private void OnActionClicked(string friendName)
        {
            // TODO: Implement friend action (remove, block, etc.)
            Debug.Log($"Action clicked for friend {friendName}");
        }
        
        private void OnChatClicked(string friendName)
        {
            // TODO: Open chat with friend
            Debug.Log($"Chat clicked for friend {friendName}");
        }
    }
}