using UnityEngine;

namespace Project.Services
{
    /// <summary>
    /// AudioService handles all music and sound effect playback for the game. It also stores user settings
    /// for enabling/disabling music and SFX.
    /// </summary>
    public class AudioService : MonoBehaviour
    {
        private static AudioService instance;
        public static AudioService Instance => instance;

        [Header("Audio Settings")]
        [SerializeField] private bool musicEnabled = true;
        [SerializeField] private bool sfxEnabled = true;
        [SerializeField] private float musicVolume = 1f;
        [SerializeField] private float sfxVolume = 1f;

        private AudioSource musicSource;
        private AudioSource sfxSource;

        [Header("Audio Clips")]
        [Tooltip("Default music clip to play when no specific clip is provided.")]
        [SerializeField] private AudioClip defaultMusicClip;
        [Tooltip("SFX clip for button click sounds.")]
        [SerializeField] private AudioClip buttonClickClip;
        [Tooltip("SFX clip for UI selection sounds.")]
        [SerializeField] private AudioClip uiSelectClip;
        [Tooltip("SFX clip for error feedback sounds.")]
        [SerializeField] private AudioClip errorClip;

        // Added to play a coin collection sound when players receive coins from rewards or ads.
        [Tooltip("SFX clip for coin collection.")]
        [SerializeField] private AudioClip coinCollectClip;

        private void Awake()
        {
            // Ensure a single instance persists across scenes
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
                return;
            }
            instance = this;
            DontDestroyOnLoad(gameObject);

            // Create audio sources for music and SFX if not assigned
            musicSource = gameObject.AddComponent<AudioSource>();
            musicSource.loop = true;
            musicSource.volume = musicVolume;

            sfxSource = gameObject.AddComponent<AudioSource>();
            sfxSource.loop = false;
            sfxSource.volume = sfxVolume;
        }

        public void Init()
        {
            Debug.Log("[AudioService] Initialized");
        }

        /// <summary>
        /// Returns whether music is currently enabled.
        /// </summary>
        public bool IsMusicEnabled() => musicEnabled;

        /// <summary>
        /// Enables or disables music playback and saves the setting.
        /// </summary>
        public void SetMusicEnabled(bool enabled)
        {
            musicEnabled = enabled;
            PlayerPrefs.SetInt("MusicEnabled", enabled ? 1 : 0);
            if (!enabled)
            {
                StopMusic();
            }
        }

        /// <summary>
        /// Returns whether sound effects are currently enabled.
        /// </summary>
        public bool IsSFXEnabled() => sfxEnabled;

        /// <summary>
        /// Enables or disables SFX playback and saves the setting.
        /// </summary>
        public void SetSFXEnabled(bool enabled)
        {
            sfxEnabled = enabled;
            PlayerPrefs.SetInt("SoundEnabled", enabled ? 1 : 0);
        }

        /// <summary>
        /// Plays a generic button click sound. Checks SFX enabled state.
        /// </summary>
        public void PlayButtonClick()
        {
            if (!sfxEnabled) return;
            if (buttonClickClip != null && sfxSource != null)
            {
                sfxSource.PlayOneShot(buttonClickClip, sfxVolume);
            }
            else
            {
                Debug.Log("[AudioService] Button click");
            }
        }

        /// <summary>
        /// Plays a UI selection sound. Checks SFX enabled state.
        /// </summary>
        public void PlayUISelect()
        {
            if (!sfxEnabled) return;
            if (uiSelectClip != null && sfxSource != null)
            {
                sfxSource.PlayOneShot(uiSelectClip, sfxVolume);
            }
            else
            {
                Debug.Log("[AudioService] UI select");
            }
        }

        /// <summary>
        /// Plays an error feedback sound. Checks SFX enabled state.
        /// </summary>
        public void PlayError()
        {
            if (!sfxEnabled) return;
            if (errorClip != null && sfxSource != null)
            {
                sfxSource.PlayOneShot(errorClip, sfxVolume);
            }
            else
            {
                Debug.Log("[AudioService] Error sound");
            }
        }

        /// <summary>
        /// Plays a coin collection sound. Checks SFX enabled state.
        /// </summary>
        public void PlayCoinCollect()
        {
            if (!sfxEnabled) return;
            if (coinCollectClip != null && sfxSource != null)
            {
                sfxSource.PlayOneShot(coinCollectClip, sfxVolume);
            }
            else
            {
                Debug.Log("[AudioService] Coin collect");
            }
        }

        /// <summary>
        /// Plays a specific sound effect clip.
        /// </summary>
        public void PlaySfx(AudioClip clip, float volume = 1f)
        {
            if (!sfxEnabled || clip == null) return;
            sfxSource.PlayOneShot(clip, volume * sfxVolume);
        }

        /// <summary>
        /// Starts music playback. Stops current music if different clip provided.
        /// </summary>
        public void PlayMusic(AudioClip clip = null, bool loop = true)
        {
            if (!musicEnabled) return;
            // Use provided clip or fallback to default
            AudioClip target = clip != null ? clip : defaultMusicClip;
            if (target == null)
            {
                Debug.LogWarning("[AudioService] No music clip provided or assigned in the inspector.");
                return;
            }
            if (musicSource.clip != target)
            {
                musicSource.clip = target;
            }
            musicSource.loop = loop;
            musicSource.volume = musicVolume;
            musicSource.Play();
        }

        /// <summary>
        /// Stops currently playing music.
        /// </summary>
        public void StopMusic()
        {
            musicSource.Stop();
        }

        // Required API methods (currently unused in this project but kept for consistency)
        public void Open() { }
        public void Close() { }
        public void Build() { }
        public void ApplyState() { }
    }
}