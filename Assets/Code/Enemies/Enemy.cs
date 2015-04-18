using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour 
{
    public float Health, MoveSpeed, MaxSpeed, InvunPeriod, AttackRange, AttackRate;
    public string Weakness;
    private float lastHitTime;
    private string soulLinkType;
    private float[] bounds;
    private bool grounded, flip, playerFound;
    protected float lastAttackTime;
	protected bool facingRight;

    public GameObject Weapon;
	private Rigidbody2D rig2D;
    private SpriteRenderer spriRen;
	private Transform flipCheck;
    private GameObject player;
    private Animator anim;
    protected Vector3 dist;

	// Use this for initialization
	public virtual void Start () 
	{
		flipCheck = transform.Find ("floorCheck");
		rig2D = GetComponent<Rigidbody2D> ();
        spriRen = GetComponent<SpriteRenderer>();
        bounds = new float[]{ spriRen.bounds.size.x / 2, spriRen.bounds.size.y / 2 };
        player = GameObject.Find("Player");
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	public virtual void Update () 
	{
		if (Health <= 0)
			Die ();
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
		if(!flip && grounded)
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
            if (facingRight)
                bounds[0] = -bounds[0];
            else
                bounds[0] = +bounds[0];
            Collider2D col = Physics2D.Raycast(transform.position + new Vector3(bounds[0], bounds[1]), dist).collider;
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
		Destroy (gameObject);
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
            else
                Health -= damage / 2;

            lastHitTime = Time.time;
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
