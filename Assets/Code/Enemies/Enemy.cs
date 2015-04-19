using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour 
{
    public float Health, MoveSpeed, MaxSpeed, InvunPeriod, AttackRange, AttackRate, DeathTime;
    public string Type, Weakness;
    private float lastHitTime, timer;
    private string soulLinkType;
    private bool grounded, flip, wall, playerFound, dying;
    protected float lastAttackTime;
	protected bool facingRight;

    public GameObject Weapon;
    public LayerMask mask;
	private Rigidbody2D rig2D;
    protected SpriteRenderer spriRen;
	private Transform flipCheck, wallCheck;
    private GameObject player;
    protected Animator anim;
    protected Vector3 dist;

	// Use this for initialization
	public virtual void Start () 
	{
		flipCheck = transform.Find ("floorCheck");
        wallCheck = transform.Find("wallCheck");
		rig2D = GetComponent<Rigidbody2D> ();
        spriRen = GetComponent<SpriteRenderer>();
        player = GameObject.Find("Player");
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	public virtual void Update () 
	{
		if (Health <= 0)
			Die ();
        if (dying)
            timer += Time.deltaTime;
        if(timer > DeathTime)
            Destroy(gameObject);
             
	}

    public virtual void FixedUpdate()
    {
        DetectPlayer();
        if (playerFound)
        {
            anim.SetBool("Moving", false);
            Attack();
        }
        else
            Move();
    }

	public virtual void Move()
	{
        anim.SetBool("Moving", true);
		Patrol ();
	}

    //Basic patrol movement.
	public void Patrol()
	{
		flip = Physics2D.Linecast (transform.position, flipCheck.position, 1 << LayerMask.NameToLayer ("Ground"));
        wall = Physics2D.Linecast(transform.position, wallCheck.position, 1 << LayerMask.NameToLayer("Ground"));
		if((!flip || wall) && grounded)
			Flip ();
		
		if (facingRight && (rig2D.velocity.x < MaxSpeed)) 
			rig2D.AddForce(-Vector2.right * MoveSpeed);
		
		if (!facingRight && (rig2D.velocity.x < MaxSpeed))
			rig2D.AddForce(Vector2.right * MoveSpeed);
		
		if (Mathf.Abs(rig2D.velocity.x) > MaxSpeed) 
			rig2D.velocity = new Vector2 (Mathf.Sign (rig2D.velocity.x) * MaxSpeed, rig2D.velocity.y);
	}

    //Flips enemy.
	public void Flip()
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.gameObject.tag == "Ground")
            grounded = true;
    }

    private void OnCollisionExit2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Ground")
            grounded = false;
    }

    public void DetectPlayer()
    {
        dist = player.transform.position - transform.position;
        Vector3 enemyDir;
        if(facingRight)
            enemyDir = transform.TransformDirection(-Vector2.right); 
        else
            enemyDir = transform.TransformDirection(Vector2.right); 
        float angleDot = Vector3.Dot(dist, enemyDir); 
        bool playerInFrontOfEnemy = angleDot > 0.0;
        bool playerCloseToEnemy = dist.magnitude < AttackRange;

        if (playerInFrontOfEnemy && playerCloseToEnemy)
        {
            Collider2D col = Physics2D.Raycast(transform.position, dist, AttackRange, mask).collider;
            if (col != null)
                if (col.gameObject.tag.Equals("Player"))
                    playerFound = true;
            else
                playerFound = false;
        }
        else
            playerFound = false;
    }   

	public virtual void Die()
	{
        dying = true;
        anim.SetBool("Dying", dying);
	}

    public void ChangeLinkType(string linkType)
    {
        soulLinkType = linkType;
    }

	public void ApplyDamage(float damage)
	{
        if (Time.time > lastHitTime + InvunPeriod)
        {
            if (soulLinkType.Equals(Weakness))
                Health -= damage * 2;
            else if (soulLinkType.Equals(Type))
                Health -= damage / 2;
            else
                Health -= damage;

            lastHitTime = Time.time;
            if(Health > 0)
                StartCoroutine(damageTaken()); 
        }
	}

    public IEnumerator damageTaken()
    {
          spriRen.enabled = false;
          yield return new WaitForSeconds(0.1f);
          spriRen.enabled = true;
    }

    public virtual void Attack()
    {

    }

    public virtual void SoulLink()
    {

    }
}
