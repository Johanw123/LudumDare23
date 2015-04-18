using UnityEngine;
using System.Collections;

public class SoulLink : MonoBehaviour {

	public GameObject LinkedEntity;
	public string LinkType;
	public bool Linked = false;

    private PlayerStats stats;
	private LineRenderer line;

	// Use this for initialization
	void Start () 
    {
        stats = GetComponent<PlayerStats>();
		line = GetComponent<LineRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (LinkedEntity == null)
			Unlink ();
		
		if (LinkedEntity != null) {
			line.SetPosition (0, this.transform.position);
			line.SetPosition (1, LinkedEntity.transform.position);
		} else
		{
			line.SetPosition (0, this.transform.position);
			line.SetPosition (1, this.transform.position);
		}

	}

	public void Link(GameObject entity) {
		LinkedEntity = entity;
		Linked = true;
	}

	public void Unlink () {
		LinkedEntity = null;
		Linked = false;
	}

	public void ToggleLink (GameObject entity) {
		if (entity == LinkedEntity)
			Unlink ();
		else
			Link (entity);
	}

	void ChangeLinkType (string type) {
		LinkType = type;

		if (Linked && (LinkedEntity != null))
			LinkedEntity.SendMessage ("ChangeLinkType", type);
	}

	public void TakeDamage (float damage)
	{
		if (Linked && (LinkedEntity != null))
            LinkedEntity.SendMessage("ApplyDamage", damage);
        else
            stats.ApplyDamage(damage);
	}
}
