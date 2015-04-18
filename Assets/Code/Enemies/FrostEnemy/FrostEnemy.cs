using UnityEngine;
using System.Collections;

public class FrostEnemy : Enemy
{
    private bool firstAttack = true;

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
        if (firstAttack)
            Snowball();
        else
            if (Time.time > lastAttackTime + AttackRate)
                Snowball();
    }

    private void Snowball()
    {
        firstAttack = false;
        GameObject proj = Instantiate(Weapon, transform.position, transform.rotation) as GameObject;
        proj.SendMessage("Direction", dist);
        lastAttackTime = Time.time;
    }
}
