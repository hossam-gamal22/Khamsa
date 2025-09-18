namespace Project.Systems
{
    using UnityEngine;
    using UnityEngine.SceneManagement;
    using System.Collections;
    using Project.Core;

    public class BootLoader : MonoBehaviour
    {
        public static BootLoader Instance { get; private set; }

        [Header("Service References")]
        [SerializeField] private PlayerIdService playerIdService;
        [SerializeField] private UsernameService usernameService;
        [SerializeField] private CoinsWallet coinsWallet;
        [SerializeField] private DailyRewardService dailyRewardService;
        [SerializeField] private AudioService audioService;
        [SerializeField] private DataSyncService dataSyncService;
        [SerializeField] private AvatarService avatarService;
        [SerializeField] private RewardedAdService rewardedAdService;
        
        [Header("Scene Configuration")]
        [SerializeField] private string nextSceneName = "App Shell";
        
        [Header("Initialization Settings")]
        [SerializeField] private float initializationDelay = 0.1f;
        [SerializeField] private bool showDebugLogs = true;
        
        [Header("Service Prefabs")]
        [SerializeField] private GameObject audioServicePrefab;
        
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);

            AutoFindServices();
        }

        private void Start()
        {
            StartCoroutine(InitializeApplication());
        }

        private void AutoFindServices()
        {
            if (playerIdService == null)
                playerIdService = GetComponent<PlayerIdService>();
            if (usernameService == null)
                usernameService = GetComponent<UsernameService>();
            if (coinsWallet == null)
                coinsWallet = GetComponent<CoinsWallet>();
            if (dailyRewardService == null)
                dailyRewardService = GetComponent<DailyRewardService>();
            if (audioService == null)
                audioService = GetComponent<AudioService>();
            if (dataSyncService == null)
                dataSyncService = GetComponent<DataSyncService>();
            if (avatarService == null)
                avatarService = GetComponent<AvatarService>();
            if (rewardedAdService == null)
                rewardedAdService = GetComponent<RewardedAdService>();

            CreateMissingServices();
        }

        private void CreateMissingServices()
        {
            if (playerIdService == null)
            {
                GameObject serviceObj = new GameObject("PlayerIdService");
                DontDestroyOnLoad(serviceObj);
                playerIdService = serviceObj.AddComponent<PlayerIdService>();
            }

            if (usernameService == null)
            {
                GameObject serviceObj = new GameObject("UsernameService");
                DontDestroyOnLoad(serviceObj);
                usernameService = serviceObj.AddComponent<UsernameService>();
            }

            if (coinsWallet == null)
            {
                GameObject serviceObj = new GameObject("CoinsWallet");
                DontDestroyOnLoad(serviceObj);
                coinsWallet = serviceObj.AddComponent<CoinsWallet>();
            }

            if (dailyRewardService == null)
            {
                GameObject serviceObj = new GameObject("DailyRewardService");
                DontDestroyOnLoad(serviceObj);
                dailyRewardService = serviceObj.AddComponent<DailyRewardService>();
            }

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

            if (dataSyncService == null)
            {
                GameObject serviceObj = new GameObject("DataSyncService");
                DontDestroyOnLoad(serviceObj);
                dataSyncService = serviceObj.AddComponent<DataSyncService>();
            }

            if (avatarService == null)
            {
                GameObject serviceObj = new GameObject("AvatarService");
                DontDestroyOnLoad(serviceObj);
                avatarService = serviceObj.AddComponent<AvatarService>();
            }

            if (rewardedAdService == null)
            {
                GameObject serviceObj = new GameObject("RewardedAdService");
                DontDestroyOnLoad(serviceObj);
                rewardedAdService = serviceObj.AddComponent<RewardedAdService>();
            }
        }

        private IEnumerator InitializeApplication()
        {
            if (showDebugLogs)
                Debug.Log("[BootLoader] Starting application initialization...");

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

            yield return null;
            
            LoadNextScene();
        }

        private IEnumerator InitializePlayerIdService()
        {
            if (showDebugLogs)
                Debug.Log("[BootLoader] Initializing PlayerIdService...");
                
            if (playerIdService != null)
            {
                playerIdService.Init();
            }
            else
            {
                Debug.LogError("[BootLoader] PlayerIdService not found!");
            }
            yield return null;
        }

        private IEnumerator InitializeUsernameService()
        {
            if (showDebugLogs)
                Debug.Log("[BootLoader] Initializing UsernameService...");
                
            if (usernameService != null)
            {
                usernameService.Init();
            }
            else
            {
                Debug.LogError("[BootLoader] UsernameService not found!");
            }
            yield return null;
        }

        private IEnumerator InitializeCoinsWallet()
        {
            if (showDebugLogs)
                Debug.Log("[BootLoader] Initializing CoinsWallet...");
                
            if (coinsWallet != null)
            {
                coinsWallet.Init();
            }
            else
            {
                Debug.LogError("[BootLoader] CoinsWallet not found!");
            }
            yield return null;
        }

        private IEnumerator InitializeDailyRewardService()
        {
            if (showDebugLogs)
                Debug.Log("[BootLoader] Initializing DailyRewardService...");
                
            if (dailyRewardService != null)
            {
                dailyRewardService.Init();
            }
            else
            {
                Debug.LogError("[BootLoader] DailyRewardService not found!");
            }
            yield return null;
        }

        private IEnumerator InitializeAudioService()
        {
            if (showDebugLogs)
                Debug.Log("[BootLoader] Initializing AudioService...");
                
            if (audioService != null)
            {
                audioService.Init();
            }
            else
            {
                Debug.LogError("[BootLoader] AudioService not found!");
            }
            yield return null;
        }

        private IEnumerator InitializeDataSyncService()
        {
            if (showDebugLogs)
                Debug.Log("[BootLoader] Initializing DataSyncService...");
                
            if (dataSyncService != null)
            {
                dataSyncService.Init();
            }
            else
            {
                Debug.LogError("[BootLoader] DataSyncService not found!");
            }
            yield return null;
        }

        private IEnumerator InitializeAvatarService()
        {
            if (showDebugLogs)
                Debug.Log("[BootLoader] Initializing AvatarService...");
                
            if (avatarService != null)
            {
                avatarService.Init();
            }
            else
            {
                Debug.LogError("[BootLoader] AvatarService not found!");
            }
            yield return null;
        }

        private IEnumerator InitializeRewardedAdService()
        {
            if (showDebugLogs)
                Debug.Log("[BootLoader] Initializing RewardedAdService...");
                
            if (rewardedAdService != null)
            {
                rewardedAdService.Init();
            }
            else
            {
                Debug.LogError("[BootLoader] RewardedAdService not found!");
            }
            yield return null;
        }

        private void LoadNextScene()
        {
            if (showDebugLogs)
                Debug.Log($"[BootLoader] All services initialized. Loading scene: {nextSceneName}");
                
            if (string.IsNullOrEmpty(nextSceneName))
            {
                Debug.LogError("[BootLoader] Next scene name is empty! Please set it in the inspector.");
                return;
            }
                
            SceneManager.LoadScene(nextSceneName);
        }

        public static void Init()
        {
            Debug.Log("[BootLoader] Init() called - BootLoader should be in Boot scene");
        }

        #region Service Getters
        public PlayerIdService GetPlayerIdService() => playerIdService;
        public UsernameService GetUsernameService() => usernameService;
        public CoinsWallet GetCoinsWallet() => coinsWallet;
        public DailyRewardService GetDailyRewardService() => dailyRewardService;
        public AudioService GetAudioService() => audioService;
        public DataSyncService GetDataSyncService() => dataSyncService;
        public AvatarService GetAvatarService() => avatarService;
        public RewardedAdService GetRewardedAdService() => rewardedAdService;
        #endregion
    }
}
