using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class VolumeHandler : MonoBehaviour 
{
  Text m_volumeText;
  Slider m_volumeSlider;
  Toggle m_volumeToggle;
  string m_startingText;
  Color m_startingColor;
  bool m_isMusic;

	void Start() 
  {
    m_volumeText = GetComponentInChildren<Text>();
    m_volumeSlider = GetComponentInChildren<Slider>();
    m_volumeToggle = GetComponentInChildren<Toggle>();

    m_startingColor = m_volumeText.color;
    m_startingText = m_volumeText.text;
    m_isMusic = m_startingText.Contains("Music");

    m_volumeSlider.enabled = m_volumeToggle.isOn = m_isMusic ? AudioHandler.MusicEnabled : AudioHandler.SfxEnabled;
    m_volumeSlider.value = m_isMusic ? AudioHandler.MusicVolume * 100 : AudioHandler.SfxVolume * 100;

    SetVolumeText();
	}

  public void OnVolumeChanged()
  {
    SetVolume(m_volumeSlider.value);
    SetVolumeText();

    UpdateMenuMusicVolume();
  }

  private void UpdateMenuMusicVolume()
  {
    float convertedValue = m_volumeSlider.value / 100;

    var gameObject = GameObject.Find("/Music");
    if (m_isMusic && gameObject != null)
    {
      var audioSource = gameObject.GetComponent<AudioSource>();
      if (audioSource != null)
      {
        audioSource.volume = AudioHandler.MusicEnabled ? convertedValue : 0;
        Debug.Log(string.Format("enabled: {0}  -  {1}", AudioHandler.MusicEnabled, convertedValue));
      }
    }
  }

  public void OnToggle()
  {
    m_volumeSlider.enabled = !m_volumeSlider.enabled;

    if (m_volumeSlider.enabled)
    {
      m_volumeText.color = m_startingColor;
    }
    else
    {
      m_volumeText.color = new Color(0.5f, 0.5f, 0.5f);
    }

    ToggleMute();
    UpdateMenuMusicVolume();
  }

  private void SetVolume(float value)
  {
    float convertedValue = value / 100;

    if (m_isMusic)
      AudioHandler.SetMusicVolume(convertedValue);
    else
      AudioHandler.SetSfxVolume(convertedValue);
  }

  private void ToggleMute()
  {
    if (m_isMusic)
      AudioHandler.SetMuteMusic(m_volumeSlider.enabled);
    else
      AudioHandler.SetMuteSfx(m_volumeSlider.enabled);
  }

  private void SetVolumeText()
  {
    m_volumeText.text = m_startingText + " " + m_volumeSlider.value;
  }
}
