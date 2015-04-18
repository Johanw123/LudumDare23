using UnityEngine;
using System.Collections;

public class DamageEnviron : MonoBehaviour {

    public string LinkType;
    public float Damage;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerStay2D(Collider2D col)
    {
        col.gameObject.SendMessage("ChangeLinkType", LinkType);
        col.gameObject.SendMessage("TakeDamage", Damage);
    }

    void OnCollisionStay2D(Collision2D coll)
    {
        coll.gameObject.SendMessage("ChangeLinkType", LinkType);
        coll.gameObject.SendMessage("TakeDamage", Damage);
    }
}
