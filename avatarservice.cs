namespace Project.Core
{
    using UnityEngine;

    [System.Serializable]
    public class AvatarService : MonoBehaviour
    {
        [Header("Database Configuration")]
        [SerializeField] private TextAsset avatarCSVFile;
        [SerializeField] private string avatarFolderPath = "Avatars";
        
        [System.Serializable]
        public class AvatarData
        {
            public string name;
            public string avatar;
            public bool free;
            public int price;
        }
        
        private AvatarData[] avatars;
        private string currentAvatar;
        private string[] ownedAvatars;
        
        public event System.Action<string> OnAvatarChanged;
        
        public void Init()
        {
            LoadAvatarDatabase();
            currentAvatar = PlayerPrefs.GetString("CurrentAvatar", "");
            
            // Load owned avatars
            string ownedStr = PlayerPrefs.GetString("OwnedAvatars", "");
            ownedAvatars = string.IsNullOrEmpty(ownedStr) ? new string[0] : ownedStr.Split(',');
            
            // If no avatar set, assign random free avatar
            if (string.IsNullOrEmpty(currentAvatar))
            {
                AssignRandomFreeAvatar();
            }
            
            Debug.Log($"AvatarService initialized. Current avatar: {currentAvatar}");
        }
        
        private void LoadAvatarDatabase()
        {
            // Use assigned CSV file or try to load from Resources
            TextAsset csvFile = avatarCSVFile ?? Resources.Load<TextAsset>("DB/characters");
            
            if (csvFile != null)
            {
                string[] lines = csvFile.text.Split('\n');
                avatars = new AvatarData[lines.Length - 1]; // Skip header
                
                for (int i = 1; i < lines.Length; i++)
                {
                    string[] values = lines[i].Split(',');
                    if (values.Length >= 4)
                    {
                        avatars[i - 1] = new AvatarData
                        {
                            name = values[0].Trim(),
                            avatar = values[1].Trim(),
                            free = values[2].Trim().ToLower() == "true",
                            price = int.Parse(values[3].Trim())
                        };
                    }
                }
                Debug.Log($"Loaded {avatars.Length} avatars from CSV");
            }
            else
            {
                Debug.LogError("Avatar CSV file not found!");
            }
        }
        
        private void AssignRandomFreeAvatar()
        {
            var freeAvatars = System.Array.FindAll(avatars, a => a.free);
            if (freeAvatars.Length > 0)
            {
                var randomAvatar = freeAvatars[UnityEngine.Random.Range(0, freeAvatars.Length)];
                SetAvatar(randomAvatar.avatar);
                Debug.Log($"Assigned random free avatar: {randomAvatar.name}");
            }
        }
        
        public void SetAvatar(string avatarName)
        {
            currentAvatar = avatarName;
            PlayerPrefs.SetString("CurrentAvatar", avatarName);
            OnAvatarChanged?.Invoke(avatarName);
            Debug.Log($"Avatar changed to: {avatarName}");
        }
        
        public string GetCurrentAvatar() => currentAvatar;
        public AvatarData[] GetAllAvatars() => avatars;
        public bool IsAvatarOwned(string avatarName) => System.Array.Exists(ownedAvatars, a => a == avatarName);
        
        public Sprite LoadAvatarSprite(string avatarName)
        {
            return Resources.Load<Sprite>($"{avatarFolderPath}/{avatarName}");
        }
        
        public void PurchaseAvatar(string avatarName)
        {
            // Add to owned avatars
            var newOwned = new string[ownedAvatars.Length + 1];
            ownedAvatars.CopyTo(newOwned, 0);
            newOwned[ownedAvatars.Length] = avatarName;
            ownedAvatars = newOwned;
            
            // Save to PlayerPrefs
            PlayerPrefs.SetString("OwnedAvatars", string.Join(",", ownedAvatars));
            Debug.Log($"Purchased avatar: {avatarName}");
        }
        
        public void Open() { }
        public void Close() { }
        public void Build() { }
        public void ApplyState() { }
    }
}