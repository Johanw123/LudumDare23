using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour 
{
	public float Health, MoveSpeed, MaxSpeed, InvunPeriod;
	public string Weakness;

	protected bool soulLinked;
    private string soulLinkType;
	private bool facingRight;

	private Rigidbody2D rig2D;
	private Transform groundCheck;
	private bool grounded;

	// Use this for initialization
	public void Start () 
	{
		groundCheck = transform.Find ("floorCheck");
		rig2D = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	public void Update () 
	{
		if (Health <= 0)
			Die ();
		Move ();
	}

	public virtual void SoulLink()
	{

	}

	public virtual void Move()
	{
		Patrol ();
	}

	public void Patrol()
	{
		grounded = Physics2D.Linecast (transform.position, groundCheck.position, 1 << LayerMask.NameToLayer ("Ground"));
		if(!grounded)
			Flip ();
		
		if (facingRight && (rig2D.velocity.x < MaxSpeed)) 
			rig2D.AddForce(-Vector2.right * MoveSpeed);
		
		if (!facingRight && (rig2D.velocity.x < MaxSpeed))
			rig2D.AddForce(Vector2.right * MoveSpeed);
		
		if (Mathf.Abs(rig2D.velocity.x) > MaxSpeed) 
			rig2D.velocity = new Vector2 (Mathf.Sign (rig2D.velocity.x) * MaxSpeed, rig2D.velocity.y);
	}

	public void Flip()
	{
		facingRight = !facingRight;

		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	public virtual void Attack()
	{
            
	}

	public virtual void Die()
	{
		Destroy (gameObject);
	}

    public void ChangeLinkType(string linkType)
    {
        soulLinkType = linkType;
    }

	public void ApplyDamage(float Damage)
	{
        if (soulLinkType.Equals(Weakness))
            Health -= Damage * 2;
        else
            Health -= Damage / 2;
	}
}
