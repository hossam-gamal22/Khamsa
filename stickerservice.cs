namespace Project.Core
{
    using UnityEngine;

    [System.Serializable]
    public class StickerService : MonoBehaviour
    {
        [System.Serializable]
        public class StickerData
        {
            public string name;
            public string icon;
            public int quantity;
            public int price;
        }
        
        private StickerData[] stickers;
        private string[] ownedStickers;
        
        public void Init()
        {
            LoadStickerDatabase();
            
            // Load owned stickers
            string ownedStr = PlayerPrefs.GetString("OwnedStickers", "");
            ownedStickers = string.IsNullOrEmpty(ownedStr) ? new string[0] : ownedStr.Split(',');
            
            Debug.Log($"StickerService initialized with {stickers?.Length ?? 0} stickers");
        }
        
        private void LoadStickerDatabase()
        {
            // Load from Resources/DB/stickers.csv
            TextAsset csvFile = Resources.Load<TextAsset>("DB/stickers");
            if (csvFile != null)
            {
                string[] lines = csvFile.text.Split('\n');
                stickers = new StickerData[lines.Length - 1]; // Skip header
                
                for (int i = 1; i < lines.Length; i++)
                {
                    string[] values = lines[i].Split(',');
                    if (values.Length >= 4)
                    {
                        stickers[i - 1] = new StickerData
                        {
                            name = values[0].Trim(),
                            icon = values[1].Trim(),
                            quantity = int.Parse(values[2].Trim()),
                            price = int.Parse(values[3].Trim())
                        };
                    }
                }
            }
            else
            {
                Debug.LogError("Stickers CSV file not found at Resources/DB/stickers");
            }
        }
        
        public StickerData[] GetAllStickers() => stickers ?? new StickerData[0];
        public bool IsStickerOwned(string stickerName) => System.Array.Exists(ownedStickers, s => s == stickerName);
        
        public Sprite LoadStickerIcon(string iconName)
        {
            return Resources.Load<Sprite>($"Stickers/{iconName}");
        }
        
        public void PurchaseSticker(string stickerName)
        {
            // Add to owned stickers
            var newOwned = new string[ownedStickers.Length + 1];
            ownedStickers.CopyTo(newOwned, 0);
            newOwned[ownedStickers.Length] = stickerName;
            ownedStickers = newOwned;
            
            // Save to PlayerPrefs
            PlayerPrefs.SetString("OwnedStickers", string.Join(",", ownedStickers));
            Debug.Log($"Purchased sticker: {stickerName}");
        }
        
        public void Open() { }
        public void Close() { }
        public void Build() { }
        public void ApplyState() { }
    }
}