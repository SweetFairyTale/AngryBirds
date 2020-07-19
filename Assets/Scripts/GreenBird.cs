using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenBird : BirdController {

    protected override void Skill()
    {
        base.Skill();
        Vector3 speed = rg.velocity;
        speed.x *= -1;
        rg.velocity = speed;
        gameObject.GetComponent<SpriteRenderer>().flipX = true;
        gameObject.GetComponent<Transform>().Rotate(new Vector3(0, 0, 55));
    }
}
