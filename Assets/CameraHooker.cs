using UnityEngine;
using System.Collections;
using System;

public class CameraHooker : MonoBehaviour {

	public GameObject TargetReticule;
	public float MinCameraSize;
	public float MaxCameraSize;
	public float CameraSizeIncrement;

  private SoulLink soulLink;
	private Camera mainCamera;

	// Use this for initialization
	void Start () {
		mainCamera = Camera.main;
        Cursor.visible = false;

        var gameObject = GameObject.Find("/Soul Link");
        if (gameObject != null)
          soulLink = gameObject.GetComponent<SoulLink>();
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 reticulePosition = GetReticulePosition ();

	    if ((Vector3.Distance(this.transform.position, reticulePosition) > MaxCameraSize) && soulLink.Linked)
			CameraMoveTo (this.transform.position + new Vector3 (0, 0, -10));
		else
			CameraMoveTo (MiddlePoint (this.transform.position, reticulePosition));

//
	}

	private Vector3 GetReticulePosition()
	{
		return TargetReticule.transform.position;
	}

	private bool IsOnScreen ()
	{
		Vector3 viewport = Camera.main.WorldToViewportPoint (this.transform.position);

		if (Math.Abs (viewport.x) > 1)
			return false;
		else if (Math.Abs (viewport.y) > 1)
			return false;
		else
			return true;
	}

	private Vector3 MiddlePoint (Vector3 start, Vector3 end)
	{
		Vector3 ret = (end - start) * 0.5f + start;

		ret.z = -10;
		ret.x = Mathf.Floor (ret.x * 10) / 10;
		ret.y = Mathf.Floor (ret.y * 10) / 10;

		return ret;
	}

	private void CameraMoveTo (Vector3 target)
	{
		mainCamera.transform.position = Vector3.MoveTowards(mainCamera.transform.position, target, Vector3.Distance (mainCamera.transform.position, target)/5);
	}

	public void ResizeCamera (float change)
	{
		if ((change > 0) && (Camera.main.orthographicSize >= MaxCameraSize))
			return;

		if ((change < 0) && (Camera.main.orthographicSize <= MinCameraSize))
			return;

		Camera.main.orthographicSize += change;
	}
}