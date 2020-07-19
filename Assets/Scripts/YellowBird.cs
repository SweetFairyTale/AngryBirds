using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowBird : BirdController {

    private SpriteRenderer yellowBirdRender;
    public Sprite rushB_NoStop;

    protected override void Skill()
    {
        base.Skill();
        yellowBirdRender = GetComponent<SpriteRenderer>();
        yellowBirdRender.sprite = rushB_NoStop;
        rg.velocity *= 1.5f;
    }
}
