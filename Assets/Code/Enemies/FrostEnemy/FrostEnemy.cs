using UnityEngine;
using System.Collections;

public class FrostEnemy : Enemy
{
    public GameObject Weapon;

	public override void Start()
	{
		base.Start ();
	}
	// Update is called once per frame
	public override void Update () 
	{
		base.Update ();
	}

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Attack()
    {
        if (Time.time > lastAttackTime + AttackRate)
        {
            GameObject proj = Instantiate(Weapon, transform.position, transform.rotation) as GameObject;
            proj.SendMessage("Direction", dist);
            lastAttackTime = Time.time;
        }
    }
}
