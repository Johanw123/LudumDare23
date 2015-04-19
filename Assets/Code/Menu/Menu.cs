using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour 
{
  private Animator m_animator;
  private CanvasGroup m_canvasGroup;

  public bool IsOpen
  {
    get { return m_animator.GetBool("IsOpen"); }
    set { m_animator.SetBool("IsOpen", value); }
  }
  
  public void Awake()
  {
    m_animator = GetComponent<Animator>();
    m_canvasGroup = GetComponent<CanvasGroup>();

    var rect = GetComponent<RectTransform>();
    rect.offsetMax = rect.offsetMin = Vector2.zero;
  }
}
