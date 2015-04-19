    using UnityEngine;
using System.Collections;

public class Door :Enemy 
{
    public override void Start()
    {
        spriRen = GetComponent<SpriteRenderer>();
    }

    public override void Update()
    {
        base.Update();
    }

    public override void FixedUpdate()
    {

    }
}
