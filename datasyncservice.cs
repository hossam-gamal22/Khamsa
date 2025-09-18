namespace Project.Core
{
    using UnityEngine;

    [System.Serializable]
    public class DataSyncService : MonoBehaviour
    {
        public void Init()
        {
            // Stub for online data sync
        }
        
        public void SyncToServer()
        {
            // TODO: Upload player data to server
        }
        
        public void SyncFromServer()
        {
            // TODO: Download player data from server
        }
        
        public void Open() { }
        public void Close() { }
        public void Build() { }
        public void ApplyState() { }
    }
}