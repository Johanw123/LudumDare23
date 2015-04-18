using UnityEngine;
using System.Collections;

public class FireEnemy : Enemy
{
    private GameObject weapon;
    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        base.Update();
        if (weapon != null && dist.magnitude > AttackRange)
            Destroy(weapon);
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Attack()
    {
        if (Time.time > lastAttackTime + AttackRate)
        {
            if (weapon != null)
                Destroy(weapon);
            
            if (!facingRight)
                weapon = Instantiate(Weapon, transform.position, transform.rotation) as GameObject;
            else
                weapon = Instantiate(Weapon, transform.position, Quaternion.Euler(0, 180, 0)) as GameObject;
            weapon.transform.parent = transform;
            lastAttackTime = Time.time;
        }
    }
}
