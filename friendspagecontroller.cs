namespace Project.UI
{
    using UnityEngine;
    using UnityEngine.UI;

    public enum FriendsTab
    {
        FriendsList,
        AddFriend,
        Requests
    }

    public class FriendsPageController : MonoBehaviour
    {
        [Header("Tab Buttons")]
        [SerializeField] private Button friendsListTab;
        [SerializeField] private Button addFriendTab;
        [SerializeField] private Button requestsTab;
        
        [Header("Content Areas")]
        [SerializeField] private GameObject friendsListContent;
        [SerializeField] private GameObject addFriendContent;
        [SerializeField] private GameObject requestsContent;
        
        public void Init()
        {
            if (friendsListTab != null)
                friendsListTab.onClick.AddListener(() => SetTab(FriendsTab.FriendsList));
            if (addFriendTab != null)
                addFriendTab.onClick.AddListener(() => SetTab(FriendsTab.AddFriend));
            if (requestsTab != null)
                requestsTab.onClick.AddListener(() => SetTab(FriendsTab.Requests));
            
            SetTab(FriendsTab.FriendsList);
        }
        
        private void SetTab(FriendsTab tab)
        {
            // Hide all content
            if (friendsListContent != null) friendsListContent.SetActive(false);
            if (addFriendContent != null) addFriendContent.SetActive(false);
            if (requestsContent != null) requestsContent.SetActive(false);
            
            // Show selected content
            switch (tab)
            {
                case FriendsTab.FriendsList:
                    if (friendsListContent != null) friendsListContent.SetActive(true);
                    break;
                case FriendsTab.AddFriend:
                    if (addFriendContent != null) addFriendContent.SetActive(true);
                    break;
                case FriendsTab.Requests:
                    if (requestsContent != null) requestsContent.SetActive(true);
                    break;
            }
        }
        
        public void Open() { }
        public void Close() { }
        public void Build() { }
        public void ApplyState() { }
    }
}
