using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    private bool m_isFacingRight, m_takeFallDamage;
  private CharacterController2D m_controller;
  private float m_normalizedHorizontalSpeed;

  public float MaxSpeed = 2f;
  public float SpeedAccelerationOnGround = 10f;
  public float SpeedAccelerationInAir = 5f;
  public float FallDamage = 1f;

  public GameObject TargetReticule;
  private SoulLink m_soulLink;
  private Animator m_animator;

<<<<<<< HEAD
	void Start () 
    {
         m_controller = GetComponent<CharacterController2D>();
         m_soulLink = GetComponent<SoulLink>();
         m_animator = GetComponent<Animator>(); 
         m_isFacingRight = transform.localScale.x > 0;
	}
	
	void Update() 
    {
        if (m_controller.Velocity.y < -25)
            m_takeFallDamage = true;
        if (!m_soulLink.Linked)
            ReticuleToMousePosition();
=======
  public AudioClip JumpSound;
  public AudioClip LinkSound;
  public AudioClip UnlinkSound;

  private AudioSource m_audioSource;

  void Start()
  {
    m_controller = GetComponent<CharacterController2D>();
    m_soulLink = GetComponent<SoulLink>();
    m_animator = GetComponent<Animator>();
    m_isFacingRight = transform.localScale.x > 0;
    m_rBod = GetComponent<Rigidbody2D>();
    m_audioSource = GetComponent<AudioSource>();
  }

  void Update()
  {
    if (!m_soulLink.Linked)
      ReticuleToMousePosition();
>>>>>>> origin/master
    else
      ReticuleToLinkedEntity();

    CheckNeighbouringEnemies();
    HandleInput();

    if (m_controller.State.IsGrounded && m_takeFallDamage)
        TakeFallDamage();
    var movementFactor = m_controller.State.IsGrounded ? SpeedAccelerationOnGround : SpeedAccelerationInAir;
    m_controller.SetHorizontalForce(Mathf.Lerp(m_controller.Velocity.x, m_normalizedHorizontalSpeed * MaxSpeed, Time.deltaTime * movementFactor));
  }

  void HandleInput()
  {
    if(Input.GetKey(KeyCode.D))
    {
      m_normalizedHorizontalSpeed = 1;
      if (m_isFacingRight)
        Flip();
    }
    else if(Input.GetKey(KeyCode.A))
    {
      m_normalizedHorizontalSpeed = -1;
      if (!m_isFacingRight)
        Flip();
    }
    else
    {
      m_normalizedHorizontalSpeed = 0;
    }

    if (Input.GetButtonDown("Fire1"))
      EnemyClick();

    m_animator.SetBool("Moving", m_normalizedHorizontalSpeed != 0);
    m_animator.SetBool("Grounded", m_controller.State.IsGrounded);

    if(m_controller.CanJump && Input.GetButtonDown("Jump"))
    {
      m_controller.Jump();
      m_audioSource.PlayOneShot(JumpSound);
    }
  }

  private void ReticuleToMousePosition()
  {
    Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    mouseWorldPosition.z = 0;

    TargetReticule.transform.position = mouseWorldPosition + new Vector3(0, Mathf.Sin(Time.time) * 0.01f, 0);
  }

  private void ReticuleToLinkedEntity()
  {
    if (m_soulLink.LinkedEntity == null)
      return;

    Vector3 entityPosition = m_soulLink.LinkedEntity.transform.position;

    TargetReticule.transform.position = entityPosition;
  }

  private void CheckNeighbouringEnemies()
  {
    GameObject[] enemyList = GameObject.FindGameObjectsWithTag("Enemy");

    Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    mouseWorldPosition.z = 0;

    foreach (GameObject enemy in enemyList)
    {
      if (Vector3.Distance(enemy.transform.position, mouseWorldPosition) < 0.5)
        TargetReticule.transform.position = enemy.transform.position;
    }
  }

  void EnemyClick()
  {
    RaycastHit2D hitInfo = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Camera.main.ScreenToWorldPoint(Input.mousePosition));

    if (hitInfo == true)
    {
      if (hitInfo.transform.gameObject.tag != "Enemy")
        return;

      m_soulLink.ToggleLink(hitInfo.transform.gameObject);
      if(m_soulLink.Linked)
        m_audioSource.PlayOneShot(LinkSound);
      else
        m_audioSource.PlayOneShot(UnlinkSound);
    }
    else if (m_soulLink.Linked)
    {
      m_soulLink.Unlink();
      m_audioSource.PlayOneShot(UnlinkSound);
    }
  }

  private void Flip()
  {
    transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    m_isFacingRight = transform.localScale.x > 0;
  }

    private void TakeFallDamage()
  {
      m_takeFallDamage = false;
      m_soulLink.ChangeLinkType("Crushing");
      m_soulLink.TakeDamage(FallDamage);
  }
}
