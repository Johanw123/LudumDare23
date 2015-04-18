using UnityEngine;
using System.Collections;

public class Skeley : Enemy
{
    private GameObject proj;
    private bool firstAttack = true;

    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        base.Update();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Attack()
    {
        if (firstAttack)
            Death();
        else if (Time.time > lastAttackTime + AttackRate)
            Death();
    }

    private void Death()
    {
        firstAttack = false;
        if (proj != null)
            Destroy(proj);
        proj = Instantiate(Weapon, transform.position, transform.rotation) as GameObject;
        lastAttackTime = Time.time;
    }
}
