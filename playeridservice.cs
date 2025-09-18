namespace Project.Core
{
    using UnityEngine;

    [System.Serializable]
    public class PlayerIdService : MonoBehaviour
    {
        private string playerId;
        
        public void Init()
        {
            playerId = PlayerPrefs.GetString("PlayerId", "");
            if (string.IsNullOrEmpty(playerId))
            {
                playerId = System.Guid.NewGuid().ToString();
                PlayerPrefs.SetString("PlayerId", playerId);
            }
        }
        
        public string GetPlayerId() => playerId;
        public void Open() { }
        public void Close() { }
        public void Build() { }
        public void ApplyState() { }
    }
}