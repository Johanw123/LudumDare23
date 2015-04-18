using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class VolumeHandler : MonoBehaviour 
{
  Text m_volumeText;
  Slider m_volumeSlider;
  string m_startingText;
  Color m_startingColor;

	void Start() 
  {
    m_volumeText = GetComponentInChildren<Text>();
    m_volumeSlider = GetComponentInChildren<Slider>();

    m_startingColor = m_volumeText.color;
    m_startingText = m_volumeText.text;
    SetVolumeText();
	}

  public void OnVolumeChanged()
  {
    SetVolumeText();
  }

  public void OnToggle()
  {
    m_volumeSlider.enabled = !m_volumeSlider.enabled;

    if (m_volumeSlider.enabled)
      m_volumeText.color = m_startingColor;
    else
      m_volumeText.color = new Color(0.5f, 0.5f, 0.5f);
      
  }

  private void SetVolumeText()
  {
    m_volumeText.text = m_startingText + " " + m_volumeSlider.value;
  }
}
