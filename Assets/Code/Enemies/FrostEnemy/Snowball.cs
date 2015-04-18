using UnityEngine;
using System.Collections;

public class Snowball : MonoBehaviour {

    private Vector3 dir;
    public float Speed, Damage, DespawnTime;
    private float spawnTime;

	// Use this for initialization
	void Start () 
    {
        spawnTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (Time.time > spawnTime + DespawnTime)
            Destroy(gameObject);
	}
    
    public void Direction(Vector3 dir)
    {
        this.dir = dir;
        Fire();
    }

    public void Fire()
    {
        GetComponent<Rigidbody2D>().AddForce(dir * Speed);
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            col.SendMessage("ChangeLinkType", "Ice");
            col.SendMessage("ApplyDamage", Damage);
            Destroy(gameObject);
        }
    }
}
