using UnityEngine;
using System.Collections;

public class SoulLink : MonoBehaviour {

	public GameObject LinkedEntity;
	public string LinkType;

	public bool Linked = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (LinkedEntity == null)
			Unlink ();

	}

	public void Link(GameObject entity) {
		LinkedEntity = entity;
		Linked = true;
	}

	public void Unlink () {
		LinkedEntity = null;
		Linked = false;
	}

	public void ToggleLink (GameObject entity) {
		if (entity == LinkedEntity)
			Unlink ();
		else
			Link (entity);
	}

	void ChangeLinkType (string type) {
		LinkType = type;

		if (Linked)
			LinkedEntity.SendMessage ("ChangeLinkType", type);
	}
}
