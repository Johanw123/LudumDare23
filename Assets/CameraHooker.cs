using UnityEngine;
using System.Collections;
using System;

public class CameraHooker : MonoBehaviour {

	public GameObject TargetReticule;
	public float MinCameraSize;
	public float MaxCameraSize;
	public float CameraSizeIncrement;

	private Camera mainCamera;

	// Use this for initialization
	void Start () {
		mainCamera = Camera.main;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 reticulePosition = GetReticulePosition ();

		CameraMoveTo (MiddlePoint (this.transform.position, TargetReticule.transform.position));
		ResizeCamera (MinCameraSize, MaxCameraSize);
	}

	private Vector3 GetReticulePosition()
	{
		return TargetReticule.transform.position;
	}

	private Vector3 MiddlePoint (Vector3 start, Vector3 end)
	{
		Vector3 ret = (end - start) * 0.5f + start;

		ret.z = -10;

		return ret;
	}

	private void CameraMoveTo (Vector3 target)
	{
		mainCamera.transform.position = Vector3.MoveTowards(mainCamera.transform.position, target, Vector3.Distance (mainCamera.transform.position, target)/20);
	}

	private void ResizeCamera (float minSize, float maxSize)
	{
		float objDistanceX, objDistanceY, objDistance;

		objDistanceX = Math.Abs (this.transform.position.x - TargetReticule.transform.position.x);
		objDistanceY = Math.Abs (this.transform.position.y - TargetReticule.transform.position.y);

		if (objDistanceX > objDistanceY)
			objDistance = objDistanceX;
		else
			objDistance = objDistanceY;

		if (objDistance > maxSize)
			objDistance = maxSize;

		if (objDistance < minSize)
			objDistance = minSize;

		if (Camera.main.orthographicSize < (objDistance / 2)) {
			Camera.main.orthographicSize += CameraSizeIncrement;

			if (Camera.main.orthographicSize > (objDistance / 2))
				Camera.main.orthographicSize = objDistance / 2;
		}
		else if (Camera.main.orthographicSize > (objDistance / 2)) 
		{
			Camera.main.orthographicSize -= CameraSizeIncrement;

			if (Camera.main.orthographicSize < (objDistance / 2))
				Camera.main.orthographicSize = objDistance / 2;
		}
	}
}