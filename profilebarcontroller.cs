namespace Project.UI
{
    using UnityEngine;
    using UnityEngine.UI;
    using TMPro;
    using Project.Core;
    using Project.Services;
    // Import Project.Systems to access BootLoader
    using Project.Systems;

    public class ProfileBarController : MonoBehaviour
    {
        [Header("Display Elements")]
        [SerializeField] private Image avatarImage;
        [SerializeField] private TextMeshProUGUI usernameText;
        [SerializeField] private Button editButton;
        [SerializeField] private Image kingIcon;
        
        [Header("Edit Username Panel")]
        [SerializeField] private GameObject editUsernamePanel;
        [SerializeField] private TMP_InputField usernameInputField;
        [SerializeField] private Button confirmButton;
        [SerializeField] private Button cancelButton;
        
        [Header("Error Messages")]
        [SerializeField] private GameObject errorPanel;
        [SerializeField] private TextMeshProUGUI errorText;
        [SerializeField] private TextMeshProUGUI arabicErrorText;
        [SerializeField] private TextMeshProUGUI lengthErrorText;
        [SerializeField] private TextMeshProUGUI takenErrorText;
        
        private UsernameService usernameService;
        private AvatarService avatarService;
        private AudioService audioService;
        
        public void Init()
        {
            // Get services from global BootLoader
            usernameService = BootLoader.Instance.GetUsernameService();
            avatarService = BootLoader.Instance.GetAvatarService();
            audioService = BootLoader.Instance.GetAudioService();
            
            // Setup listeners
            editButton.onClick.AddListener(OnEditUsernameClicked);
            confirmButton.onClick.AddListener(OnConfirmUsernameClicked);
            cancelButton.onClick.AddListener(OnCancelUsernameClicked);
            
            // Subscribe to changes
            avatarService.OnAvatarChanged += UpdateAvatarDisplay;
            usernameService.OnUsernameChanged += UpdateUsernameDisplay;
            
            // Setup input field
            usernameInputField.characterLimit = usernameService.GetMaxLength();
            usernameInputField.onValueChanged.AddListener(OnUsernameInputChanged);
            
            // Initial display
            UpdateAvatarDisplay(avatarService.GetCurrentAvatar());
            UpdateUsernameDisplay(usernameService.GetUsername());
            
            // Hide edit panel initially
            editUsernamePanel.SetActive(false);
            HideAllErrors();
        }
        
        private void UpdateAvatarDisplay(string avatarName)
        {
            Sprite avatarSprite = avatarService.LoadAvatarSprite(avatarName);
            if (avatarSprite != null)
            {
                avatarImage.sprite = avatarSprite;
            }
        }
        
        private void UpdateUsernameDisplay(string username)
        {
            usernameText.text = username;
        }
        
        private void OnEditUsernameClicked()
        {
            audioService.PlayButtonClick();
            editUsernamePanel.SetActive(true);
            usernameInputField.text = usernameService.GetUsername();
            usernameInputField.Select();
            HideAllErrors();
        }
        
        private void OnConfirmUsernameClicked()
        {
            audioService.PlayButtonClick();
            string newUsername = usernameInputField.text;
            
            UsernameService.ValidationResult result = usernameService.ValidateUsername(newUsername);
            
            if (result == UsernameService.ValidationResult.Valid)
            {
                usernameService.SetUsername(newUsername);
                editUsernamePanel.SetActive(false);
                HideAllErrors();
            }
            else
            {
                ShowError(result);
            }
        }
        
        private void OnCancelUsernameClicked()
        {
            audioService.PlayButtonClick();
            editUsernamePanel.SetActive(false);
            HideAllErrors();
        }
        
        private void OnUsernameInputChanged(string value)
        {
            // Real-time validation feedback
            UsernameService.ValidationResult result = usernameService.ValidateUsername(value);
            
            if (result == UsernameService.ValidationResult.Valid)
            {
                HideAllErrors();
                confirmButton.interactable = true;
            }
            else
            {
                ShowError(result);
                confirmButton.interactable = false;
            }
        }
        
        private void ShowError(UsernameService.ValidationResult result)
        {
            HideAllErrors();
            
            switch (result)
            {
                case UsernameService.ValidationResult.ContainsArabic:
                    arabicErrorText.gameObject.SetActive(true);
                    break;
                case UsernameService.ValidationResult.TooLong:
                    lengthErrorText.gameObject.SetActive(true);
                    break;
                case UsernameService.ValidationResult.AlreadyTaken:
                    takenErrorText.gameObject.SetActive(true);
                    break;
            }
            
            errorPanel.SetActive(true);
            audioService.PlayError();
        }
        
        private void HideAllErrors()
        {
            errorPanel.SetActive(false);
            arabicErrorText.gameObject.SetActive(false);
            lengthErrorText.gameObject.SetActive(false);
            takenErrorText.gameObject.SetActive(false);
        }
        
        public void Open() { }
        public void Close() { }
        public void Build() { }
        public void ApplyState() { }
    }
}