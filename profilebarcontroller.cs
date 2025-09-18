namespace Project.UI
{
    using UnityEngine;
    using UnityEngine.UI;
    using TMPro;
    using Project.Core;
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
            var bootLoader = BootLoader.Instance;
            if (bootLoader != null)
            {
                usernameService = bootLoader.GetUsernameService();
                avatarService = bootLoader.GetAvatarService();
                audioService = bootLoader.GetAudioService();
            }
            
            if (editButton != null)
                editButton.onClick.AddListener(OnEditUsernameClicked);
            if (confirmButton != null)
                confirmButton.onClick.AddListener(OnConfirmUsernameClicked);
            if (cancelButton != null)
                cancelButton.onClick.AddListener(OnCancelUsernameClicked);
            
            if (avatarService != null)
                avatarService.OnAvatarChanged += UpdateAvatarDisplay;
            if (usernameService != null)
            {
                usernameService.OnUsernameChanged += UpdateUsernameDisplay;
                if (usernameInputField != null)
                {
                    usernameInputField.characterLimit = usernameService.GetMaxLength();
                    usernameInputField.onValueChanged.AddListener(OnUsernameInputChanged);
                }
            }
            
            if (avatarService != null)
                UpdateAvatarDisplay(avatarService.GetCurrentAvatar());
            if (usernameService != null)
                UpdateUsernameDisplay(usernameService.GetUsername());
            
            if (editUsernamePanel != null)
                editUsernamePanel.SetActive(false);
            HideAllErrors();
        }
        
        private void UpdateAvatarDisplay(string avatarName)
        {
            if (avatarImage != null && avatarService != null)
            {
                Sprite avatarSprite = avatarService.LoadAvatarSprite(avatarName);
                if (avatarSprite != null)
                {
                    avatarImage.sprite = avatarSprite;
                }
            }
        }
        
        private void UpdateUsernameDisplay(string username)
        {
            if (usernameText != null)
            {
                usernameText.text = username;
            }
        }
        
        private void OnEditUsernameClicked()
        {
            audioService?.PlayButtonClick();
            if (editUsernamePanel != null && usernameService != null)
            {
                editUsernamePanel.SetActive(true);
                if (usernameInputField != null)
                {
                    usernameInputField.text = usernameService.GetUsername();
                    usernameInputField.Select();
                }
                HideAllErrors();
            }
        }
        
        private void OnConfirmUsernameClicked()
        {
            audioService?.PlayButtonClick();
            if (usernameInputField != null && usernameService != null)
            {
                string newUsername = usernameInputField.text;
                
                UsernameService.ValidationResult result = usernameService.ValidateUsername(newUsername);
                
                if (result == UsernameService.ValidationResult.Valid)
                {
                    usernameService.SetUsername(newUsername);
                    if (editUsernamePanel != null)
                        editUsernamePanel.SetActive(false);
                    HideAllErrors();
                }
                else
                {
                    ShowError(result);
                }
            }
        }
        
        private void OnCancelUsernameClicked()
        {
            audioService?.PlayButtonClick();
            if (editUsernamePanel != null)
                editUsernamePanel.SetActive(false);
            HideAllErrors();
        }
        
        private void OnUsernameInputChanged(string value)
        {
            if (usernameService != null)
            {
                UsernameService.ValidationResult result = usernameService.ValidateUsername(value);
                
                if (result == UsernameService.ValidationResult.Valid)
                {
                    HideAllErrors();
                    if (confirmButton != null)
                        confirmButton.interactable = true;
                }
                else
                {
                    ShowError(result);
                    if (confirmButton != null)
                        confirmButton.interactable = false;
                }
            }
        }
        
        private void ShowError(UsernameService.ValidationResult result)
        {
            HideAllErrors();
            
            switch (result)
            {
                case UsernameService.ValidationResult.ContainsArabic:
                    if (arabicErrorText != null)
                        arabicErrorText.gameObject.SetActive(true);
                    break;
                case UsernameService.ValidationResult.TooLong:
                    if (lengthErrorText != null)
                        lengthErrorText.gameObject.SetActive(true);
                    break;
                case UsernameService.ValidationResult.AlreadyTaken:
                    if (takenErrorText != null)
                        takenErrorText.gameObject.SetActive(true);
                    break;
            }
            
            if (errorPanel != null)
                errorPanel.SetActive(true);
            audioService?.PlayError();
        }
        
        private void HideAllErrors()
        {
            if (errorPanel != null)
                errorPanel.SetActive(false);
            if (arabicErrorText != null)
                arabicErrorText.gameObject.SetActive(false);
            if (lengthErrorText != null)
                lengthErrorText.gameObject.SetActive(false);
            if (takenErrorText != null)
                takenErrorText.gameObject.SetActive(false);
        }
        
        public void Open() { }
        public void Close() { }
        public void Build() { }
        public void ApplyState() { }
    }
}