using UnityEngine;
using System.Collections;

public class Snowball : MonoBehaviour {

    private Vector3 dir;
    public float Speed;
    public float Damage;

	// Use this for initialization
	void Start () 
    {
 
	}
	
	// Update is called once per frame
	void Update () {
	
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
}
