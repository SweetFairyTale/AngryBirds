using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigController : MonoBehaviour
{

    public float maxSpeed = 10;
    public float minSpeed = 5;
    public Sprite hurt;
    private SpriteRenderer render;

    public GameObject boom;
    public GameObject pigScore;

    public AudioClip hurtCollision;
    public AudioClip dead;

    public bool isPig = false;  //为使木块与猪共用此脚本而进行的判定

    void Start()
    {
        //transform.Find()
        render = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Bird")
        {
            collision.transform.GetComponent<BirdController>().Hurt();
        }

        //Debug.Log(collision.relativeVelocity.magnitude);
        if (collision.relativeVelocity.magnitude > maxSpeed)  //kill pig directly.
        {
            Dead();
        }
        else
            if (collision.relativeVelocity.magnitude > minSpeed && collision.relativeVelocity.magnitude < maxSpeed)
        {
            AudioDisplay(hurtCollision);
            render.sprite = hurt;
        }
    }

    public void Dead()
    {
        if (isPig)
        {
            GameManager._instance.pig.Remove(this);
        }
        AudioDisplay(dead);  //死亡音效.
        Destroy(gameObject);
        Instantiate(boom, transform.position, Quaternion.identity);
          //显示得分.
        GameObject go = Instantiate(pigScore, transform.position + new Vector3(0, 0.7f, 0), Quaternion.identity);
        Destroy(go, 1f);
    }

    private void AudioDisplay(AudioClip clip)
    {
        AudioSource.PlayClipAtPoint(clip, transform.position);
    }
}
