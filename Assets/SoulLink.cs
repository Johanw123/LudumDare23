using UnityEngine;
using System.Collections;

public class SoulLink : MonoBehaviour {

	public GameObject LinkedEntity;
  public GameObject Player;
	public string LinkType;
	public bool Linked = false;

    private PlayerStats stats;
	private LineRenderer line;

	// Use this for initialization
	void Start () 
    {
      stats = Player.GetComponent<PlayerStats>();
		line = GetComponent<LineRenderer> ();
	}

  private float stuff = -1;
	
	// Update is called once per frame
	void Update () {

		if (LinkedEntity == null)
			Unlink ();
		
		if (LinkedEntity != null) {
      
      line.sortingLayerName = "Foreground";
      line.SetPosition(0, new Vector3(Player.transform.position.x, Player.transform.position.y, -1));
      line.SetPosition(1, new Vector3(LinkedEntity.transform.position.x, LinkedEntity.transform.position.y, -1));

      //transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);

      stuff += 0.013f;
      if (stuff > 1.0f)
        stuff = -1.0f;
      //line.material.mainTextureOffset = new Vector2(line.material.mainTextureOffset.x, stuff);
      line.material.mainTextureOffset = new Vector2(-stuff, -line.material.mainTextureOffset.y);

      //line.SetColors(Color.red, Color.red);

      
      
      if (LinkType == "Fire")
        line.material.color = Color.red * 1.5f;
      else if (LinkType == "Ice")
        line.material.color = Color.blue* 1.5f;
      else
        line.material.color = Color.yellow * 1.5f;

		} else
		{
      line.SetPosition(0, Player.transform.position);
      line.SetPosition(1, Player.transform.position);
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

	public void ChangeLinkType (string type) {
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
