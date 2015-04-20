using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

  private bool m_isFacingRight, m_takeFallDamage;
  private CharacterController2D m_controller;
  private float m_normalizedHorizontalSpeed;

  public float MaxSpeed = 2f;
  public float fallDist, minY;
  public float SpeedAccelerationOnGround = 10f;
  public float SpeedAccelerationInAir = 5f;
  public float FallDamage = 1f;

  public GameObject TargetReticule;
  private SoulLink m_soulLink;
  private Animator m_animator;
  private PlayerStats m_stats;

  public AudioClip JumpSound;
  public AudioClip LinkSound;
  public AudioClip UnlinkSound;

  private AudioSource m_audioSource;

  void Start()
  {
      m_stats = GetComponent<PlayerStats>();
    m_controller = GetComponent<CharacterController2D>();
    var gameObject = GameObject.Find("Soul Link");
    if(gameObject != null)
      m_soulLink = gameObject.GetComponent<SoulLink>();
    m_animator = GetComponent<Animator>();
    m_isFacingRight = transform.localScale.x > 0;
    m_audioSource = GetComponent<AudioSource>();
  }

  void Update()
  {
      if(transform.position.y < minY)
          DeathByFall();

      if (m_controller.Velocity.y < -fallDist)
      {
          m_takeFallDamage = true;
          m_animator.SetBool("Falling", true);
      }

       if (m_controller.Velocity.y > -fallDist)
           m_animator.SetBool("Falling", false);

      if (!m_soulLink.Linked)
      ReticuleToMousePosition();

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

	if (Input.GetAxis ("Mouse ScrollWheel") != 0)
			this.SendMessage ("ResizeCamera", -Input.GetAxis ("Mouse ScrollWheel"));

    if (Input.GetButtonDown("Fire1"))
      EnemyClick();

	if (Input.GetButtonDown ("Fire2") && m_soulLink.Linked)
			m_soulLink.Unlink ();

	if (Input.GetButtonDown ("Fire3"))
			GameObject.Find ("Player").SendMessage ("Die");

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

    private void DeathByFall()
    {
        m_animator.SetBool("Falling", true);
        m_stats.Die();
    }

    public void ChangeLinkType(string type)
    {
      m_soulLink.ChangeLinkType(type);
    }

    public void TakeDamage(float damage)
    {
      m_soulLink.TakeDamage(damage);
    }
}
