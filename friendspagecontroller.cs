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
            friendsListTab.onClick.AddListener(() => SetTab(FriendsTab.FriendsList));
            addFriendTab.onClick.AddListener(() => SetTab(FriendsTab.AddFriend));
            requestsTab.onClick.AddListener(() => SetTab(FriendsTab.Requests));
            
            SetTab(FriendsTab.FriendsList);
        }
        
        private void SetTab(FriendsTab tab)
        {
            // Hide all content
            friendsListContent.SetActive(false);
            addFriendContent.SetActive(false);
            requestsContent.SetActive(false);
            
            // Show selected content
            switch (tab)
            {
                case FriendsTab.FriendsList:
                    friendsListContent.SetActive(true);
                    break;
                case FriendsTab.AddFriend:
                    addFriendContent.SetActive(true);
                    break;
                case FriendsTab.Requests:
                    requestsContent.SetActive(true);
                    break;
            }
        }
        
        public void Open() { }
        public void Close() { }
        public void Build() { }
        public void ApplyState() { }
    }
}