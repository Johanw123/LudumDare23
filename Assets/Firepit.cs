using UnityEngine;
using System.Collections;

public class Firepit : MonoBehaviour {

	public string LinkType = "Fire";
	public float damage = 0.1F;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D coll)
	{
		coll.gameObject.SendMessage ("ChangeLinkType", LinkType);
	}

	void OnCollisionStay2D(Collision2D coll)
	{
		coll.gameObject.SendMessage ("TakeDamage", damage);
	}
}
