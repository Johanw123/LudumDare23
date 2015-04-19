using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeOut : MonoBehaviour 
{
    private bool fadeOut = false;
    private string sceneToLoad;
    private float AlphaFadeValue = 0f;
    private Player input;
    private Image fader;
    public float FadeTime = 0.5f;

	// Use this for initialization
	void Start () 
    {
        fader = GetComponent<Image>();
        input = GameObject.Find("Player").GetComponent<Player>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (fadeOut)
        {
            input.enabled = false;
            AlphaFadeValue = Mathf.Clamp01(AlphaFadeValue + (Time.deltaTime / FadeTime));
            fader.color = new Color(0, 0, 0, AlphaFadeValue);
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
