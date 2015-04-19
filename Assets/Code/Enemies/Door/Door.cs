﻿    using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Door :Enemy 
{
    private GameObject Player;

    private Text text;

    public override void Start()
    {
        spriRen = GetComponent<SpriteRenderer>();
        text = GameObject.Find("Door Text").GetComponent<Text>();
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
          text.text = "Hello, my name is Door... I will not let you enter. Try something 'Unconventional'.";
        }
        else
          text.text = "";
      }

      base.Update();
    }

    public override void FixedUpdate()
    {

    }
}
