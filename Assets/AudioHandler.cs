using UnityEngine;
using System.Collections;

public class AudioHandler : MonoBehaviour 
{
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
    AudioListener.volume = SfxVolume;
  }

	void Start() 
  {
    SetSfxVolume(SfxVolume);
    SetMusicVolume(MusicVolume);

    BackgroundMusic.volume = MusicVolume;
    BackgroundMusic.ignoreListenerVolume = true;
    BackgroundMusic.Play();
	}
}
