namespace Project.UI
{
    using UnityEngine;
    using UnityEngine.UI;

    public class SettingsSplashController : MonoBehaviour
    {
        [Header("UI Elements")]
        [SerializeField] private GameObject splashPanel;
        [SerializeField] private Button closeButton;
        [SerializeField] private Toggle soundToggle;
        [SerializeField] private Toggle musicToggle;
        [SerializeField] private Button googleButton;
        [SerializeField] private Button appleButton;
        [SerializeField] private Button deleteAccountButton;
        
        public void Init()
        {
            closeButton.onClick.AddListener(Close);
            soundToggle.onValueChanged.AddListener(OnSoundToggled);
            musicToggle.onValueChanged.AddListener(OnMusicToggled);
            googleButton.onClick.AddListener(OnGoogleLogin);
            appleButton.onClick.AddListener(OnAppleLogin);
            deleteAccountButton.onClick.AddListener(OnDeleteAccount);
            
            LoadSettings();
        }
        
        private void LoadSettings()
        {
            soundToggle.isOn = PlayerPrefs.GetInt("SoundEnabled", 1) == 1;
            musicToggle.isOn = PlayerPrefs.GetInt("MusicEnabled", 1) == 1;
        }
        
        private void OnSoundToggled(bool enabled)
        {
            PlayerPrefs.SetInt("SoundEnabled", enabled ? 1 : 0);
            // TODO: Apply sound setting
        }
        
        private void OnMusicToggled(bool enabled)
        {
            PlayerPrefs.SetInt("MusicEnabled", enabled ? 1 : 0);
            // TODO: Apply music setting
        }
        
        private void OnGoogleLogin()
        {
            // TODO: Implement Google login
            Debug.Log("Google login clicked");
        }
        
        private void OnAppleLogin()
        {
            // TODO: Implement Apple login
            Debug.Log("Apple login clicked");
        }
        
        private void OnDeleteAccount()
        {
            // TODO: Show confirmation and delete account
            Debug.Log("Delete account clicked");
        }
        
        public void Open()
        {
            splashPanel.SetActive(true);
        }
        
        public void Close()
        {
            splashPanel.SetActive(false);
        }
        
        public void Build() { }
        public void ApplyState() { }
    }
}