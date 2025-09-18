using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using Project.Core;
using Project.Services;

namespace Project.Systems
{
    /// <summary>
    /// BootLoader handles the initialization of all core services and systems.
    /// Entry point of the application that sets up everything before loading the main app.
    /// </summary>
    public class BootLoader : MonoBehaviour
    {
        // Singleton instance to provide global access to initialized services
        public static BootLoader Instance { get; private set; }

        // Cached service references for easy access from other systems/UI
        private PlayerIdService playerIdService;
        private UsernameService usernameService;
        private CoinsWallet coinsWallet;
        private DailyRewardService dailyRewardService;
        private AudioService audioService;
        private DataSyncService dataSyncService;
        private AvatarService avatarService;
        private RewardedAdService rewardedAdService;
        [Header("Initialization Settings")]
        [SerializeField] private float initializationDelay = 0.1f;
        [SerializeField] private bool showDebugLogs = true;
        
        [Header("Service Prefabs")]
        [SerializeField] private GameObject audioServicePrefab;
        
        private void Awake()
        {
            // Ensure a single BootLoader instance persists across scenes
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            StartCoroutine(InitializeApplication());
        }

        /// <summary>
        /// Initialize all core services in the correct order
        /// </summary>
        private IEnumerator InitializeApplication()
        {
            if (showDebugLogs)
                Debug.Log("[BootLoader] Starting application initialization...");

            // Initialize services in dependency order
            yield return InitializePlayerIdService();
            yield return new WaitForSeconds(initializationDelay);
            
            yield return InitializeUsernameService();
            yield return new WaitForSeconds(initializationDelay);
            
            yield return InitializeCoinsWallet();
            yield return new WaitForSeconds(initializationDelay);
            
            yield return InitializeDailyRewardService();
            yield return new WaitForSeconds(initializationDelay);
            
            yield return InitializeAudioService();
            yield return new WaitForSeconds(initializationDelay);
            
            yield return InitializeDataSyncService();
            yield return new WaitForSeconds(initializationDelay);
            
            yield return InitializeAvatarService();
            yield return new WaitForSeconds(initializationDelay);
            
            yield return InitializeRewardedAdService();
            yield return new WaitForSeconds(initializationDelay);

            // Wait one frame before loading App Shell
            yield return null;
            
            LoadAppShell();
        }

        private IEnumerator InitializePlayerIdService()
        {
            if (showDebugLogs)
                Debug.Log("[BootLoader] Initializing PlayerIdService...");
                
            // Find or create PlayerIdService
            PlayerIdService playerIdService = UnityEngine.Object.FindFirstObjectByType<PlayerIdService>();
            if (playerIdService == null)
            {
                GameObject serviceObj = new GameObject("PlayerIdService");
                DontDestroyOnLoad(serviceObj);
                playerIdService = serviceObj.AddComponent<PlayerIdService>();
            }
            
            playerIdService.Init();
            // Cache reference for global access
            this.playerIdService = playerIdService;
            yield return null;
        }

        private IEnumerator InitializeUsernameService()
        {
            if (showDebugLogs)
                Debug.Log("[BootLoader] Initializing UsernameService...");
                
            // Find or create UsernameService
            UsernameService usernameService = UnityEngine.Object.FindFirstObjectByType<UsernameService>();
            if (usernameService == null)
            {
                GameObject serviceObj = new GameObject("UsernameService");
                DontDestroyOnLoad(serviceObj);
                usernameService = serviceObj.AddComponent<UsernameService>();
            }
            
            usernameService.Init();
            // Cache reference for global access
            this.usernameService = usernameService;
            yield return null;
        }

        private IEnumerator InitializeCoinsWallet()
        {
            if (showDebugLogs)
                Debug.Log("[BootLoader] Initializing CoinsWallet...");
                
            // Find or create CoinsWallet
            CoinsWallet coinsWallet = UnityEngine.Object.FindFirstObjectByType<CoinsWallet>();
            if (coinsWallet == null)
            {
                GameObject serviceObj = new GameObject("CoinsWallet");
                DontDestroyOnLoad(serviceObj);
                coinsWallet = serviceObj.AddComponent<CoinsWallet>();
            }
            
            coinsWallet.Init();
            // Cache reference for global access
            this.coinsWallet = coinsWallet;
            yield return null;
        }

        private IEnumerator InitializeDailyRewardService()
        {
            if (showDebugLogs)
                Debug.Log("[BootLoader] Initializing DailyRewardService...");
                
            // Find or create DailyRewardService
            DailyRewardService dailyRewardService = UnityEngine.Object.FindFirstObjectByType<DailyRewardService>();
            if (dailyRewardService == null)
            {
                GameObject serviceObj = new GameObject("DailyRewardService");
                DontDestroyOnLoad(serviceObj);
                dailyRewardService = serviceObj.AddComponent<DailyRewardService>();
            }
            
            dailyRewardService.Init();
            // Cache reference for global access
            this.dailyRewardService = dailyRewardService;
            yield return null;
        }

        private IEnumerator InitializeAudioService()
        {
            if (showDebugLogs)
                Debug.Log("[BootLoader] Initializing AudioService...");
                
            // Find or create AudioService
            AudioService audioService = UnityEngine.Object.FindFirstObjectByType<AudioService>();
            if (audioService == null)
            {
                if (audioServicePrefab != null)
                {
                    GameObject serviceObj = Instantiate(audioServicePrefab);
                    DontDestroyOnLoad(serviceObj);
                    audioService = serviceObj.GetComponent<AudioService>();
                }
                else
                {
                    GameObject serviceObj = new GameObject("AudioService");
                    DontDestroyOnLoad(serviceObj);
                    audioService = serviceObj.AddComponent<AudioService>();
                }
            }
            
            audioService.Init();
            // Cache reference for global access
            this.audioService = audioService;
            yield return null;
        }

        private IEnumerator InitializeDataSyncService()
        {
            if (showDebugLogs)
                Debug.Log("[BootLoader] Initializing DataSyncService...");
                
            // Find or create DataSyncService
            DataSyncService dataSyncService = UnityEngine.Object.FindFirstObjectByType<DataSyncService>();
            if (dataSyncService == null)
            {
                GameObject serviceObj = new GameObject("DataSyncService");
                DontDestroyOnLoad(serviceObj);
                dataSyncService = serviceObj.AddComponent<DataSyncService>();
            }
            
            dataSyncService.Init();
            // Cache reference for global access
            this.dataSyncService = dataSyncService;
            yield return null;
        }

        private IEnumerator InitializeAvatarService()
        {
            if (showDebugLogs)
                Debug.Log("[BootLoader] Initializing AvatarService...");
                
            // Find or create AvatarService
            AvatarService avatarService = UnityEngine.Object.FindFirstObjectByType<AvatarService>();
            if (avatarService == null)
            {
                GameObject serviceObj = new GameObject("AvatarService");
                DontDestroyOnLoad(serviceObj);
                avatarService = serviceObj.AddComponent<AvatarService>();
            }
            
            avatarService.Init();
            // Cache reference for global access
            this.avatarService = avatarService;
            yield return null;
        }

        private IEnumerator InitializeRewardedAdService()
        {
            if (showDebugLogs)
                Debug.Log("[BootLoader] Initializing RewardedAdService...");
                
            // Find or create RewardedAdService
            RewardedAdService rewardedAdService = UnityEngine.Object.FindFirstObjectByType<RewardedAdService>();
            if (rewardedAdService == null)
            {
                GameObject serviceObj = new GameObject("RewardedAdService");
                DontDestroyOnLoad(serviceObj);
                rewardedAdService = serviceObj.AddComponent<RewardedAdService>();
            }
            
            rewardedAdService.Init();
            // Cache reference for global access
            this.rewardedAdService = rewardedAdService;
            yield return null;
        }

        private void LoadAppShell()
        {
            if (showDebugLogs)
                Debug.Log("[BootLoader] All services initialized. Loading App Shell...");
                
            SceneManager.LoadScene("App Shell");
        }

        /// <summary>
        /// Public entry point for initialization
        /// </summary>
        public static void Init()
        {
            // This method can be called externally if needed
            Debug.Log("[BootLoader] Init() called - BootLoader should be in Boot scene");
        }

        #region Service Getters
        /// <summary>
        /// Provides access to the initialized PlayerIdService.
        /// </summary>
        public PlayerIdService GetPlayerIdService() => playerIdService;

        /// <summary>
        /// Provides access to the initialized UsernameService.
        /// </summary>
        public UsernameService GetUsernameService() => usernameService;

        /// <summary>
        /// Provides access to the initialized CoinsWallet.
        /// </summary>
        public CoinsWallet GetCoinsWallet() => coinsWallet;

        /// <summary>
        /// Provides access to the initialized DailyRewardService.
        /// </summary>
        public DailyRewardService GetDailyRewardService() => dailyRewardService;

        /// <summary>
        /// Provides access to the initialized AudioService.
        /// </summary>
        public AudioService GetAudioService() => audioService;

        /// <summary>
        /// Provides access to the initialized DataSyncService.
        /// </summary>
        public DataSyncService GetDataSyncService() => dataSyncService;

        /// <summary>
        /// Provides access to the initialized AvatarService.
        /// </summary>
        public AvatarService GetAvatarService() => avatarService;

        /// <summary>
        /// Provides access to the initialized RewardedAdService.
        /// </summary>
        public RewardedAdService GetRewardedAdService() => rewardedAdService;
        #endregion
    }
}