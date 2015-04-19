using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SceneTransition : MonoBehaviour
{
    public float FadeTime = 5f;
    public string sceneToLoad;
    private Image fader;
    private float AlphaFadeValue = 0f;
    private bool sceneTransit;
    private Player input;
	// Use this for initialization
	void Start () 
    {
        fader = GameObject.Find("FadeOut").GetComponent<Image>();
        input = GameObject.Find("Player").GetComponent<Player>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (sceneTransit)
        {
            input.enabled = false;
            AlphaFadeValue = Mathf.Clamp01(AlphaFadeValue + (Time.deltaTime / FadeTime));
            fader.color = new Color(0, 0, 0, AlphaFadeValue);
        }

        if (AlphaFadeValue == 1f)
            Application.LoadLevel(sceneToLoad);
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if(!sceneTransit)
            if (col.gameObject.tag == "Player")
                sceneTransit = true;
    }
}
