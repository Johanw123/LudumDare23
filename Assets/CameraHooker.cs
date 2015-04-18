using UnityEngine;
using System.Collections;

public class CameraHooker : MonoBehaviour {

	public GameObject TargetReticule;

	private Camera mainCamera;

	// Use this for initialization
	void Start () {
		mainCamera = Camera.main;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 reticulePosition = GetReticulePosition ();

		CameraMoveTo (MiddlePoint (this.transform.position, TargetReticule.transform.position));
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
		mainCamera.transform.position = Vector3.MoveTowards(mainCamera.transform.position, target, 0.03F);
	}
}