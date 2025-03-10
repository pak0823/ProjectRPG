using UnityEngine;
using UnityEngine.Audio; // AudioMixer 사용을 위한 네임스페이스
using System.Collections.Generic;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [Header("Sound Settings")]
    public AudioSource musicSource; // 배경 음악용 AudioSource
    public AudioSource sfxSource; // 사운드 효과용 AudioSource
    //public List<AudioClip> soundClips; // 사용할 사운드 클립 리스트
    public AudioMixer audioMixer; // AudioMixer

    private const string MusicVolumeKey = "MusicVolume";
    private const string SFXVolumeKey = "SFXVolume";

    private void Awake()
    {
        // Singleton 패턴 구현
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬 전환 시에도 유지
            musicSource = gameObject.AddComponent<AudioSource>(); // 배경 음악 소스 추가
            sfxSource = gameObject.AddComponent<AudioSource>(); // 사운드 효과 소스 추가
            //LoadSettings(); // 초기 설정 로드
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // 배경 음악 재생
    public void PlayMusic(string clipName)
    {
        AudioClip clip = Resources.Load<AudioClip>($"Sounds/{clipName}"); // Resources 폴더에서 클립 로드
        if (clip != null)
        {
            musicSource.clip = clip;
            musicSource.loop = true; // 반복 재생
            musicSource.Play(); // 배경 음악 재생
        }
        else
        {
            Debug.LogWarning($"Music clip '{clipName}' not found in Resources/Sounds.");
        }
    }

    // 사운드 효과 재생
    // 사운드 클립을 로드하고 재생하는 메서드
    public void PlaySFX(string category, string subCategory, string clipName)
    {
        string path = $"Sounds/{category}/{subCategory}/{clipName}"; // 경로 생성
        AudioClip clip = Resources.Load<AudioClip>(path); // Resources 폴더에서 클립 로드
        if (clip != null)
        {
            sfxSource.PlayOneShot(clip); // 클립 재생
        }
        else
        {
            Debug.LogWarning($"Sound clip '{clipName}' not found in {path}.");
        }
    }

    // Player 사운드 재생 메서드
    public void PlayPlayerSfx(string subCategory, string clipName)
    {
        PlaySFX("Player", subCategory, clipName);
    }

    // Enemy 사운드 재생 메서드
    public void PlayEnemySfx(string subCategory, string clipName)
    {
        PlaySFX("Enemy", subCategory, clipName);
    }

    // UI 사운드 재생 메서드
    public void PlayUISfx(string subCategory, string clipName)
    {
        PlaySFX("Ui", subCategory, clipName);
    }

    // 음악 일시 정지
    public void PauseMusic()
    {
        if (musicSource.isPlaying)
        {
            musicSource.Pause();
        }
    }

    // 음악 재개
    public void ResumeMusic()
    {
        if (!musicSource.isPlaying)
        {
            musicSource.UnPause();
        }
    }

    // 음악 중지
    public void StopMusic()
    {
        musicSource.Stop();
    }

    // 볼륨 조절
    public void SetMusicVolume(float volume)
    {
        musicSource.volume = volume;
        audioMixer.SetFloat(MusicVolumeKey, Mathf.Log10(volume) * 20); // dB로 변환
        PlayerPrefs.SetFloat(MusicVolumeKey, volume); // 설정 저장
    }

    public void SetSFXVolume(float volume)
    {
        sfxSource.volume = volume;
        audioMixer.SetFloat(SFXVolumeKey, Mathf.Log10(volume) * 20); // dB로 변환
        PlayerPrefs.SetFloat(SFXVolumeKey, volume); // 설정 저장
    }

    // 설정 로드
    private void LoadSettings()
    {
        float musicVolume = PlayerPrefs.GetFloat(MusicVolumeKey, 1f);
        float sfxVolume = PlayerPrefs.GetFloat(SFXVolumeKey, 1f);

        SetMusicVolume(musicVolume);
        SetSFXVolume(sfxVolume);
    }
}
