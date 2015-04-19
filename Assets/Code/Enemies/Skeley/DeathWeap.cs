using UnityEngine;
using System.Collections;

public class DeathWeap : MonoBehaviour 
{
    public float Speed, Damage, initialTime;
    private float timer;
    private GameObject player;

	// Use this for initialization
	void Start () 
    {
       player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update () 
    {
        Fire();
	}

    public void Fire()
    {
        timer += Time.deltaTime;
        if (timer < initialTime)
            transform.position = Vector3.MoveTowards(transform.position, transform.position - new Vector3(-30, -30), Speed * Time.deltaTime);
        else
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, Speed * Time.deltaTime);
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log(col.gameObject.tag);

        if (col.gameObject.tag == "Player")
        {   
            col.SendMessage("ChangeLinkType", "Death");
            col.SendMessage("ApplyDamage", Damage);
            Destroy(gameObject);    
        }

        if (col.gameObject.tag != "Enemy")
            Destroy(gameObject);
    }
}
