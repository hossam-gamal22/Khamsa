namespace Project.Core
{
    using UnityEngine;

    [System.Serializable]
    public class CoinPackageService : MonoBehaviour
    {
        [System.Serializable]
        public class CoinPackageData
        {
            public string name;
            public string icon;
            public int coinAmount;
            public string realPrice; // "$2.99", "200L.E", etc.
            public string productId; // For IAP integration
        }
        
        private CoinPackageData[] coinPackages;
        
        public void Init()
        {
            LoadCoinPackageDatabase();
        }
        
        private void LoadCoinPackageDatabase()
        {
            // Load from Resources/DB/coin_packages.csv
            TextAsset csvFile = Resources.Load<TextAsset>("DB/coin_packages");
            if (csvFile != null)
            {
                string[] lines = csvFile.text.Split('\n');
                coinPackages = new CoinPackageData[lines.Length - 1]; // Skip header
                
                for (int i = 1; i < lines.Length; i++)
                {
                    string[] values = lines[i].Split(',');
                    if (values.Length >= 5)
                    {
                        coinPackages[i - 1] = new CoinPackageData
                        {
                            name = values[0],
                            icon = values[1],
                            coinAmount = int.Parse(values[2]),
                            realPrice = values[3],
                            productId = values[4]
                        };
                    }
                }
            }
        }
        
        public CoinPackageData[] GetAllCoinPackages() => coinPackages;
        
        public Sprite LoadCoinPackageIcon(string iconName)
        {
            return Resources.Load<Sprite>($"Buy Coins/{iconName}");
        }
        
        public void PurchaseCoinPackage(string productId, int coinAmount)
        {
            // TODO: Integrate with Unity IAP
            // For now, just add coins (for testing)
            Debug.Log($"Purchasing {coinAmount} coins with product ID: {productId}");
            
            // In real implementation, this would be called after successful IAP
            // CoinsWallet.Instance.AddCoins(coinAmount);
        }
        
        public void Open() { }
        public void Close() { }
        public void Build() { }
        public void ApplyState() { }
    }
}