using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackBird : BirdController {

    private List<PigController> blocks = new List<PigController>();

    /// <summary>
    /// 进入爆炸范围.
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            blocks.Add(collision.GetComponent<PigController>());
        }
    }

    /// <summary>
    /// 撤出爆炸范围.
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            blocks.Remove(collision.GetComponent<PigController>());
        }
    }

    protected override void Skill()
    {
        base.Skill();
        if(blocks.Count > 0 && blocks != null)
        {
            //for (int i = 0; i < blocks.Count; i++)             
            //    blocks[i].Dead();    
            //Dead方法销毁游戏物体，触发OnTriggerExit2D,导致Count值动态减小，场景中物体无法全部销毁.

            while (blocks.Count > 0)
                blocks[0].Dead();
        }
        Clear();
    }

    private void Clear()
    {
        rg.velocity = Vector3.zero;
        Instantiate(boom, transform.position, Quaternion.identity);
        renderHurt.enabled = false;
        GetComponent<CircleCollider2D>().enabled = false;
        trail.EndTrail();
    }

    protected override void ToNextBird()
    {
        GameManager._instance.birds.Remove(this);
        Destroy(gameObject);        
        GameManager._instance.DetermineWinOrLose();
    }
}
