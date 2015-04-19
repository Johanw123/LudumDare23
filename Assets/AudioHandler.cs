using UnityEngine;
using System.Collections;

public class AudioHandler : MonoBehaviour 
{
  public static bool SfxEnabled = true;
  public static bool MusicEnabled = true;
  public static float SfxVolume = 0.5f;
  public static float MusicVolume = 0.5f;

  public AudioSource BackgroundMusic;

  public static void SetMusicVolume(float volume)
  {
    MusicVolume = volume;
  }

  public static void SetSfxVolume(float sfx)
  {
    SfxVolume = sfx;
    AudioListener.volume = SfxEnabled ? SfxVolume : 0;
  }

  public static void SetMuteMusic(bool mute)
  {
    MusicEnabled = mute;
  }

  public static void SetMuteSfx(bool mute)
  {
    SfxEnabled = mute;
    AudioListener.volume = SfxEnabled ? SfxVolume : 0;
  }

  public void SaveVolumeValues()
  {
    PlayerPrefs.SetFloat("sfx_volume", SfxVolume);
    PlayerPrefs.SetFloat("music_volume", MusicVolume);
    PlayerPrefs.SetInt("sfx_enabled", SfxEnabled ? 1 : 0);
    PlayerPrefs.SetInt("music_enabled", MusicEnabled ? 1 : 0);

    PlayerPrefs.Save();
  }

	void Awake()
  {
    SfxVolume = PlayerPrefs.GetFloat("sfx_volume");
    MusicVolume = PlayerPrefs.GetFloat("music_volume");

    SfxEnabled = PlayerPrefs.GetInt("sfx_enabled") == 1;
    MusicEnabled = PlayerPrefs.GetInt("music_enabled") == 1;

    Debug.Log(SfxEnabled);
    Debug.Log(PlayerPrefs.GetFloat("sfx_volume"));
    
    Debug.Log(MusicEnabled);
    Debug.Log(PlayerPrefs.GetFloat("music_volume"));

    SetSfxVolume(SfxVolume);
    SetMusicVolume(MusicVolume);

    BackgroundMusic.volume = MusicEnabled ? MusicVolume : 0;
    BackgroundMusic.ignoreListenerVolume = true;
    BackgroundMusic.Play();
	}
}
