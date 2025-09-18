namespace Project.Services
{
    using UnityEngine;

    [System.Serializable]
    public class NetworkService : MonoBehaviour
    {
        public void Init()
        {
            // TODO: Initialize network connections
        }
        
        public void ConnectToServer()
        {
            // TODO: Connect to game server
        }
        
        public void CreateRoom(int maxPlayers)
        {
            // TODO: Create multiplayer room
        }
        
        public void JoinRoom(string roomId)
        {
            // TODO: Join existing room
        }
        
        public void Open() { }
        public void Close() { }
        public void Build() { }
        public void ApplyState() { }
    }
}