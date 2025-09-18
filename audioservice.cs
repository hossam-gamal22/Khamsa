namespace Project.Core
{
    using UnityEngine;

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
        [SerializeField] private AudioClip defaultMusicClip;
        [SerializeField] private AudioClip buttonClickClip;
        [SerializeField] private AudioClip uiSelectClip;
        [SerializeField] private AudioClip errorClip;
        [SerializeField] private AudioClip coinCollectClip;

        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
                return;
            }
            instance = this;
            DontDestroyOnLoad(gameObject);

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

        public bool IsMusicEnabled() => musicEnabled;

        public void SetMusicEnabled(bool enabled)
        {
            musicEnabled = enabled;
            PlayerPrefs.SetInt("MusicEnabled", enabled ? 1 : 0);
            if (!enabled)
            {
                StopMusic();
            }
        }

        public bool IsSFXEnabled() => sfxEnabled;

        public void SetSFXEnabled(bool enabled)
        {
            sfxEnabled = enabled;
            PlayerPrefs.SetInt("SoundEnabled", enabled ? 1 : 0);
        }

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

        public void PlaySfx(AudioClip clip, float volume = 1f)
        {
            if (!sfxEnabled || clip == null) return;
            sfxSource.PlayOneShot(clip, volume * sfxVolume);
        }

        public void PlayMusic(AudioClip clip = null, bool loop = true)
        {
            if (!musicEnabled) return;
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

        public void StopMusic()
        {
            musicSource.Stop();
        }

        public void Open() { }
        public void Close() { }
        public void Build() { }
        public void ApplyState() { }
    }
}
