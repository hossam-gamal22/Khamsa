namespace Project.Core
{
    using UnityEngine;

    [System.Serializable]
    public class CoinsWallet : MonoBehaviour
    {
        [Header("Configuration")]
        [SerializeField] private int startingCoins = 100;
        [SerializeField] private bool resetCoinsOnStart = false; // For testing
        
        private int coins;
        public event System.Action<int> OnCoinsChanged;
        
        public void Init()
        {
            if (resetCoinsOnStart)
            {
                PlayerPrefs.DeleteKey("Coins");
            }
            
            coins = PlayerPrefs.GetInt("Coins", startingCoins);
            Debug.Log($"CoinsWallet initialized with {coins} coins");
        }
        
        public int GetCoins() => coins;
        
        public bool SpendCoins(int amount)
        {
            if (coins >= amount)
            {
                coins -= amount;
                PlayerPrefs.SetInt("Coins", coins);
                OnCoinsChanged?.Invoke(coins);
                Debug.Log($"Spent {amount} coins. Remaining: {coins}");
                return true;
            }
            Debug.Log($"Cannot spend {amount} coins. Only have: {coins}");
            return false;
        }
        
        public void AddCoins(int amount)
        {
            coins += amount;
            PlayerPrefs.SetInt("Coins", coins);
            OnCoinsChanged?.Invoke(coins);
            Debug.Log($"Added {amount} coins. Total: {coins}");
        }
        
        // Inspector method for testing
        [ContextMenu("Add 1000 Coins")]
        private void AddTestCoins()
        {
            AddCoins(1000);
        }
        
        [ContextMenu("Reset Coins")]
        private void ResetCoins()
        {
            coins = startingCoins;
            PlayerPrefs.SetInt("Coins", coins);
            OnCoinsChanged?.Invoke(coins);
        }
        
        public void Open() { }
        public void Close() { }
        public void Build() { }
        public void ApplyState() { }
    }
}