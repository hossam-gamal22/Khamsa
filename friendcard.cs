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
            if (nameText != null)
                nameText.text = friendName;
            if (statusText != null)
                statusText.text = status;
            
            if (actionButton != null)
                actionButton.onClick.AddListener(() => OnActionClicked(friendName));
            if (chatButton != null)
                chatButton.onClick.AddListener(() => OnChatClicked(friendName));
        }
        
        private void OnActionClicked(string friendName)
        {
            Debug.Log($"Action clicked for friend {friendName}");
        }
        
        private void OnChatClicked(string friendName)
        {
            Debug.Log($"Chat clicked for friend {friendName}");
        }
    }
}