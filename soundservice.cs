namespace Project.Core
{
    using UnityEngine;

    [System.Serializable]
    public class SoundService : MonoBehaviour
    {
        [System.Serializable]
        public class SoundData
        {
            public string name;
            public string icon;
            public string audioFile;
            public int price;
        }
        
        private SoundData[] sounds;
        private string[] ownedSounds;
        private AudioSource audioSource;
        
        public void Init()
        {
            LoadSoundDatabase();
            
            audioSource = gameObject.GetComponent<AudioSource>();
            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
            }
            
            string ownedStr = PlayerPrefs.GetString("OwnedSounds", "");
            ownedSounds = string.IsNullOrEmpty(ownedStr) ? new string[0] : ownedStr.Split(',');
            
            Debug.Log($"SoundService initialized with {sounds?.Length ?? 0} sounds");
        }
        
        private void LoadSoundDatabase()
        {
            TextAsset csvFile = Resources.Load<TextAsset>("DB/sounds");
            if (csvFile != null)
            {
                string[] lines = csvFile.text.Split('\n');
                sounds = new SoundData[lines.Length - 1];
                
                for (int i = 1; i < lines.Length; i++)
                {
                    string[] values = lines[i].Split(',');
                    if (values.Length >= 4)
                    {
                        sounds[i - 1] = new SoundData
                        {
                            name = values[0].Trim(),
                            icon = values[1].Trim(),
                            audioFile = values[2].Trim(),
                            price = int.Parse(values[3].Trim())
                        };
                    }
                }
            }
            else
            {
                Debug.LogError("Sounds CSV file not found at Resources/DB/sounds");
            }
        }
        
        public SoundData[] GetAllSounds() => sounds ?? new SoundData[0];
        public bool IsSoundOwned(string soundName) => System.Array.Exists(ownedSounds, s => s == soundName);
        
        public Sprite LoadSoundIcon(string iconName)
        {
            return Resources.Load<Sprite>($"Icons/{iconName}");
        }
        
        public void PlaySoundPreview(string audioFileName)
        {
            AudioClip clip = Resources.Load<AudioClip>($"Sounds/{audioFileName}");
            if (clip != null && audioSource != null)
            {
                audioSource.PlayOneShot(clip);
            }
        }
        
        public void StopSoundPreview()
        {
            if (audioSource != null)
            {
                audioSource.Stop();
            }
        }
        
        public void PurchaseSound(string soundName)
        {
            var newOwned = new string[ownedSounds.Length + 1];
            ownedSounds.CopyTo(newOwned, 0);
            newOwned[ownedSounds.Length] = soundName;
            ownedSounds = newOwned;
            
            PlayerPrefs.SetString("OwnedSounds", string.Join(",", ownedSounds));
            Debug.Log($"Purchased sound: {soundName}");
        }
        
        public void Open() { }
        public void Close() { }
        public void Build() { }
        public void ApplyState() { }
    }
}
