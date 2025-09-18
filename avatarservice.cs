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
            public string id;
            public string key;
            public string name;
            public int price;
            public string rarity;
            public string avatar;
            public bool isPremium;
            public bool free => price == 0 && !isPremium;
        }
        
        private AvatarData[] avatars;
        private string currentAvatar;
        private string[] ownedAvatars;
        
        public event System.Action<string> OnAvatarChanged;
        
        public void Init()
        {
            LoadAvatarDatabase();
            currentAvatar = PlayerPrefs.GetString("CurrentAvatar", "");
            
            string ownedStr = PlayerPrefs.GetString("OwnedAvatars", "");
            ownedAvatars = string.IsNullOrEmpty(ownedStr) ? new string[0] : ownedStr.Split(',');
            
            if (string.IsNullOrEmpty(currentAvatar))
            {
                AssignRandomFreeAvatar();
            }
            
            Debug.Log($"AvatarService initialized. Current avatar: {currentAvatar}");
        }
        
        private void LoadAvatarDatabase()
        {
            TextAsset csvFile = avatarCSVFile ?? Resources.Load<TextAsset>("DB/characters");
            
            if (csvFile != null)
            {
                string[] lines = csvFile.text.Split('\n');
                System.Collections.Generic.List<AvatarData> avatarList = new System.Collections.Generic.List<AvatarData>();
                
                for (int i = 1; i < lines.Length; i++)
                {
                    string line = lines[i].Trim();
                    if (string.IsNullOrEmpty(line)) continue;
                    
                    string[] values = line.Split(new char[] { '،', ',' }, System.StringSplitOptions.RemoveEmptyEntries);
                    
                    if (values.Length >= 7)
                    {
                        try
                        {
                            AvatarData avatarData = new AvatarData
                            {
                                id = values[0].Trim(),
                                key = values[1].Trim(),
                                name = values[2].Trim(),
                                price = int.Parse(values[3].Trim()),
                                rarity = values[4].Trim(),
                                avatar = values[5].Trim(),
                                isPremium = values[6].Trim() == "1"
                            };
                            avatarList.Add(avatarData);
                        }
                        catch (System.Exception e)
                        {
                            Debug.LogWarning($"Failed to parse avatar line {i}: {line}. Error: {e.Message}");
                        }
                    }
                }
                
                avatars = avatarList.ToArray();
                Debug.Log($"Loaded {avatars.Length} avatars from CSV");
            }
            else
            {
                Debug.LogError("Avatar CSV file not found!");
                avatars = new AvatarData[0];
            }
        }
        
        private void AssignRandomFreeAvatar()
        {
            if (avatars == null || avatars.Length == 0)
            {
                Debug.LogError("No avatars available to assign!");
                return;
            }
            
            var freeAvatars = System.Array.FindAll(avatars, a => a != null && a.free);
            if (freeAvatars.Length > 0)
            {
                var randomAvatar = freeAvatars[UnityEngine.Random.Range(0, freeAvatars.Length)];
                SetAvatar(randomAvatar.avatar);
                Debug.Log($"Assigned random free avatar: {randomAvatar.name} ({randomAvatar.avatar})");
            }
            else
            {
                Debug.LogWarning("No free avatars available!");
                if (avatars.Length > 0 && avatars[0] != null)
                {
                    SetAvatar(avatars[0].avatar);
                    Debug.Log($"Assigned fallback avatar: {avatars[0].name}");
                }
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
        public AvatarData[] GetAllAvatars() => avatars ?? new AvatarData[0];
        
        public bool IsAvatarOwned(string avatarName)
        {
            if (ownedAvatars == null) return false;
            return System.Array.Exists(ownedAvatars, a => a == avatarName);
        }
        
        public Sprite LoadAvatarSprite(string avatarName)
        {
            if (string.IsNullOrEmpty(avatarName)) return null;
            return Resources.Load<Sprite>($"{avatarFolderPath}/{avatarName}");
        }
        
        public void PurchaseAvatar(string avatarName)
        {
            if (ownedAvatars == null)
            {
                ownedAvatars = new string[0];
            }
            
            if (IsAvatarOwned(avatarName))
            {
                Debug.Log($"Avatar {avatarName} is already owned!");
                return;
            }
            
            var newOwned = new string[ownedAvatars.Length + 1];
            ownedAvatars.CopyTo(newOwned, 0);
            newOwned[ownedAvatars.Length] = avatarName;
            ownedAvatars = newOwned;
            
            PlayerPrefs.SetString("OwnedAvatars", string.Join(",", ownedAvatars));
            Debug.Log($"Purchased avatar: {avatarName}");
        }
        
        public AvatarData GetAvatarData(string avatarName)
        {
            if (avatars == null) return null;
            return System.Array.Find(avatars, a => a != null && a.avatar == avatarName);
        }
        
        public void Open() { }
        public void Close() { }
        public void Build() { }
        public void ApplyState() { }
    }
}