    using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Door :Enemy 
{
    private GameObject Player;

    private Text text;
    private string doorText;

    public override void Start()
    {
        spriRen = GetComponent<SpriteRenderer>();
        var go = GameObject.Find("Door Text");

        if (go != null)
          text = go.GetComponent<Text>();

        doorText = text == null ? "" : text.text;
        Player = GameObject.Find("Player");
        anim = GetComponent<Animator>();
    }

    public override void Update()
    {
      if (text != null)
      {
        float distance = Vector3.Distance(transform.position, Player.transform.position);

        if (distance < 1.5f)
        {
          text.transform.position = Camera.main.WorldToScreenPoint(transform.position);
          text.text = doorText;//
        }
        else
          text.text = "";
      }

      base.Update();
    }
     
    public override void Die()
    {
      Destroy(text);
      base.Die();
    }

    public override void FixedUpdate()
    {

    }
}
