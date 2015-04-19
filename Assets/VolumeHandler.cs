using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class VolumeHandler : MonoBehaviour 
{
  Text m_volumeText;
  Slider m_volumeSlider;
  string m_startingText;
  Color m_startingColor;
  bool m_isMusic;

	void Start() 
  {
    m_volumeText = GetComponentInChildren<Text>();
    m_volumeSlider = GetComponentInChildren<Slider>();

    m_startingColor = m_volumeText.color;
    m_startingText = m_volumeText.text;
    m_isMusic = m_startingText.Contains("Music");

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
    var gameObject = GameObject.Find("/Music");
    if(gameObject != null)
    {
      var audioSource = gameObject.GetComponent<AudioSource>();
      if (audioSource != null)
        audioSource.volume = m_volumeSlider.value / 100;
    }
  }

  public void OnToggle()
  {
    m_volumeSlider.enabled = !m_volumeSlider.enabled;

    if (m_volumeSlider.enabled)
    {
      m_volumeText.color = m_startingColor;
      SetVolume(m_volumeSlider.value);
    }
    else
    {
      m_volumeText.color = new Color(0.5f, 0.5f, 0.5f);
      SetVolume(0);
    }
  }

  private void SetVolume(float value)
  {
    float convertedValue = value / 100;

    if (m_isMusic)
      AudioHandler.SetMusicVolume(convertedValue);
    else
      AudioHandler.SetSfxVolume(convertedValue);
  }

  private void SetVolumeText()
  {
    m_volumeText.text = m_startingText + " " + m_volumeSlider.value;
  }
}
