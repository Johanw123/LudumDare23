using UnityEngine;
using System.Collections;

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

  public void OnPlayGame()
  {
    Application.LoadLevel("basicLevel"); 
  }

  public Menu CurrentMenu;

  void Start()
  {
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


}
