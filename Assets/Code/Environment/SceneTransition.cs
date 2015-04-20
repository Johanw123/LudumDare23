using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SceneTransition : MonoBehaviour
{

    public string sceneToLoad;
    private GameObject fader;
    private bool sceneTransit;

	// Use this for initialization
	void Start () 
    {
        fader = GameObject.Find("FadeOut");
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (sceneTransit)
            //fader.SendMessage("Fade", sceneToLoad);
			Application.LoadLevel (sceneToLoad);
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if(!sceneTransit)
            if (col.gameObject.tag == "Player")
                sceneTransit = true;
    }
}
