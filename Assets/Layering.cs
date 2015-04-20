using UnityEngine;
using System.Collections;

public class Layering : MonoBehaviour {

	// Use this for initialization
	void Start () {
		this.GetComponent<ParticleSystem> ().GetComponent<Renderer> ().sortingLayerName = "Foreground";
	}
	// Update is called once per frame
	void Update () {
	
	}
}
