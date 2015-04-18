using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour {

	public float ForceMultiplier;

	public GameObject TargetReticule;

	private bool grounded;
	private float hForce;
	private float vForce;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		ReticuleToMousePosition ();

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

	private void GetInput()
	{
		hForce = Input.GetAxisRaw ("Horizontal");

		if (!grounded)
			return;

		if (Input.GetButtonDown ("Jump"))
			vForce = 20F;
		else
			vForce = 0;
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
