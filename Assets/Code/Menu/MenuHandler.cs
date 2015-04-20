using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuHandler : MonoBehaviour 
{
  public void OnExitGame()
  {
#if UNITY_EDITOR
    UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif
  }

  private CanvasGroup m_canvasGroup;

  public void OnPlayGame()
  {
    Application.LoadLevel("LevelOne"); 
  }

  public Menu CurrentMenu;

  void Start()
  {
    GameObject go = GameObject.Find("Credits");
    if (go != null)
    {
      m_canvasGroup = go.GetComponent<CanvasGroup>();
    }

    HideCredits();
    ShowMenu(CurrentMenu);
  }

  public void ShowMenu(Menu menu)
  {
    Debug.Log("SHow Menu: " + menu);

    if (CurrentMenu != null)
      CurrentMenu.IsOpen = false;

    CurrentMenu = menu;
    CurrentMenu.IsOpen = true;
  }

  public void ShowCredits()
  {
    m_canvasGroup.alpha = 1.0f;
  }

  public void HideCredits()
  {
    m_canvasGroup.alpha = 0.0f;
  }
}
