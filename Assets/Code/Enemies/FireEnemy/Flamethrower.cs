using UnityEngine;
using System.Collections;

public class Flamethrower : MonoBehaviour {

    public float Damage;

	// Use this for initialization
	void Start () 
    {
 
	}
	
	// Update is called once per frame
	void Update () 
    {
        
	}

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            col.SendMessage("ChangeLinkType", "Fire");
            col.SendMessage("TakeDamage", Damage);
        }
    }
}
