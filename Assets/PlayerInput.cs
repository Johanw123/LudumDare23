using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour {

	public float ForceMultiplier;

	public GameObject TargetReticule;

	private bool grounded;
	private float hForce;
	private float vForce;
	private SoulLink sLink;
	
	// Use this for initialization
	void Start () {
		sLink = GetComponent<SoulLink> ();
	}
	
	// Update is called once per frame
	void Update () {
	
		if (!sLink.Linked)
			ReticuleToMousePosition ();
		else
			ReticuleToLinkedEntity ();

	
		CheckNeighbouringEnemies ();
		GetInput ();
	
	}

	void FixedUpdate() {

		ApplyForce ();

	}

	private void ReticuleToMousePosition()
	{
		Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		mouseWorldPosition.z = 0;

		TargetReticule.transform.position = mouseWorldPosition;
	}

	private void ReticuleToLinkedEntity()
	{
		if (sLink.LinkedEntity == null)
			return;

		Vector3 entityPosition = sLink.LinkedEntity.transform.position;

		TargetReticule.transform.position = entityPosition;
	}

	private void CheckNeighbouringEnemies()
	{
		GameObject[] enemyList = GameObject.FindGameObjectsWithTag ("Enemy");

		Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		mouseWorldPosition.z = 0;
		
		foreach (GameObject enemy in enemyList)
		{
			if (Vector3.Distance(enemy.transform.position,mouseWorldPosition) < 0.5)
				TargetReticule.transform.position = enemy.transform.position;
		}
	}

	private void GetInput()
	{
		hForce = Input.GetAxisRaw ("Horizontal");

		if (Input.GetButtonDown ("Fire1"))
			EnemyClick ();

		if (!grounded)
			return;

		if (Input.GetButtonDown ("Jump"))
			vForce = 20F;
		else
			vForce = 0;
	}

	void EnemyClick () 
	{
		RaycastHit2D hitInfo = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (Input.mousePosition), Camera.main.ScreenToWorldPoint (Input.mousePosition));

		if (hitInfo == true) {
			if (hitInfo.transform.gameObject.tag != "Enemy")
				return;

			sLink.ToggleLink (hitInfo.transform.gameObject);
		} 
		else if (sLink.Linked)
			sLink.Unlink ();
	}

	void OnCollisionEnter2D(Collision2D coll) {
		grounded = true;	
	}

	void OnCollisionExit2D(Collision2D coll) {
		grounded = false;
	}

	private void ApplyForce ()
	{
		this.GetComponent<Rigidbody2D> ().AddForce (new Vector2(hForce, vForce) * ForceMultiplier);
	}
}
