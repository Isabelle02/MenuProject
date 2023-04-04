using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _soundSource;

    private const string MusicKey = "Music";
    private const string SoundKey = "Sound";

    private static SoundManager _instance;
    
    public static bool IsMusicOn
    {
        get => PlayerPrefs.GetInt(MusicKey, 0) == 1;
        set
        {
            PlayerPrefs.SetInt(MusicKey, value ? 1 : 0);
            _instance._musicSource.volume = value ? 1 : 0;
        }
    }

    public static bool IsSoundOn
    {
        get => PlayerPrefs.GetInt(SoundKey, 0) == 1;
        set
        {
            PlayerPrefs.SetInt(SoundKey, value ? 1 : 0);
            _instance._soundSource.volume = value ? 1 : 0;
        }
    }

    private void Awake()
    {
        _instance = this;
        DontDestroyOnLoad(this);

        _musicSource.volume = IsMusicOn ? 1 : 0;
        _soundSource.volume = IsSoundOn ? 1 : 0;
    }

    public static void Play(Sound sound, bool loop)
    {
        _instance._musicSource.loop = loop;
        _instance._musicSource.clip = SoundClipConfig.GetClip(sound);
        _instance._musicSource.Play();
    }

    public static void PlayOneShot(Sound sound)
    {
        _instance._soundSource.PlayOneShot(SoundClipConfig.GetClip(sound));
    }
}