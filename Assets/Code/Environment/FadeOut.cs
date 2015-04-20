using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeOut : MonoBehaviour 
{
    private bool fadeOut = false;
    private string sceneToLoad;
    private float AlphaFadeValue = 0f;
    private float AlphaFadeValue2 = 2f;
    private SpriteRenderer playerRen;
    private Player playerScr;
    private Image fader;
    public float FadeTime = 0.5f;

	// Use this for initialization
	void Start () 
    {
        fader = GetComponent<Image>();
        playerScr = GameObject.Find("Player").GetComponent<Player>();
        playerRen = GameObject.Find("Player").GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (fadeOut)
        {
            AlphaFadeValue = Mathf.Clamp01(AlphaFadeValue + (Time.deltaTime / FadeTime));
            AlphaFadeValue2 = Mathf.Clamp01(AlphaFadeValue2 - (Time.deltaTime /(FadeTime/4)));
            fader.color = new Color(0, 0, 0, AlphaFadeValue);
            playerRen.color = new Color(0, 0, 0, AlphaFadeValue2);
            playerScr.enabled = false;

            if (sceneToLoad != Application.loadedLevelName)
            {
                playerScr.m_controller.SetHorizontalForce(0f);
                playerScr.m_controller.SetVerticalForce(0f);
            }
        }

        if (AlphaFadeValue == 1)
            StartCoroutine(LoadLevel()); 
	}

    void Fade(string sceneToLoad)
    {
        this.sceneToLoad = sceneToLoad;
        fadeOut = true;
    }

    private IEnumerator LoadLevel()
    {
        yield return new WaitForSeconds(0.2f);
        Application.LoadLevel(sceneToLoad);
    }
}
