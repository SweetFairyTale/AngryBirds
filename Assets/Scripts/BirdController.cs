using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdController : MonoBehaviour
{

    private bool isClick = false;  //控制Updata方法中事件检测时机.
    private bool isFlying = true;  //控制弹弓渲染时机，以及防止射出后重复点击小鸟触发的bug.
    private bool audioPlayOnlyOnce = true;

    public float maxLength = 1.0f;

    [HideInInspector]
    public SpringJoint2D sp;

    protected Rigidbody2D rg;

    private LineRenderer right;
    private Transform rightPos;
    private LineRenderer left;
    private Transform leftPos;
    public GameObject boom;

    public BirdTrail trail;
    public AudioClip select;
    public AudioClip fly;

    protected SpriteRenderer renderHurt;
    public Sprite hurt;

    //继承属性，允许小鸟(在飞行途中)使用技能.
    private bool sibideUP = false;   

    private void Awake()
    {
        sp = GetComponent<SpringJoint2D>();
        renderHurt = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        right = GameObject.Find("Slingshot_right").GetComponent<LineRenderer>();
        rightPos = GameObject.Find("rightHold").GetComponent<Transform>();
        left = GameObject.Find("Slingshot_left").GetComponent<LineRenderer>();
        leftPos = GameObject.Find("leftPos").GetComponent<Transform>();
        // sp在Start方法中赋值会出错，需在Awake方法中赋值
        rg = GetComponent<Rigidbody2D>();
        //trail = GetComponent<BirdTrail>();  字段trail为private使此赋值方法有误
    }

    private void OnMouseDown()
    {
        if(isFlying)
        {
            AudioDisplay(select);
            rg.isKinematic = true;
            isClick = true;
        }
    }

    private void OnMouseUp()
    {
        if(isFlying)
        {
            isClick = false;
            rg.isKinematic = false;
            Invoke("Fly", 0.08f);  //弹簧作用时间，增加可能导致弹不出去.
            isFlying = false;
            //禁用画线组件
            right.enabled = false;
            left.enabled = false;
        }
    }

    void Update()
    {
        if (isClick)
        {
            transform.rotation = new Quaternion(0, 0, 0, 0);
            //transform.position = Input.mousePosition;  错误 屏幕坐标(主相机左下0)和世界坐标系不同
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position += new Vector3(0, 0, -Camera.main.transform.position.z);

            if (Vector3.Distance(transform.position, rightPos.position) > maxLength)
            {
                Vector3 pos = (transform.position - rightPos.position).normalized;  //获取方向且长度为1的向量
                pos *= maxLength;
                transform.position = pos + rightPos.position;
            }

        }
        if (isFlying)
            DrawLine();

        //相机跟随.
        float posX = transform.position.x;
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position,
               new Vector3(Mathf.Clamp(posX, 0f, 13f), Camera.main.transform.position.y,
               Camera.main.transform.position.z), 2.5f * Time.deltaTime);

        if(sibideUP)  //某些小鸟拥有特殊点击技能.
            if(Input.GetMouseButtonDown(0))
            {
                Skill();
            }
    }

    private void Fly()
    {
        AudioDisplay(fly);
        trail.StartTrail();
        sp.enabled = false;  //断开弹簧.
        sibideUP = true;
        Invoke("ToNextBird", 4f);  //三秒后销毁已发射小鸟并准备下一只.
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        sibideUP = false;
        trail.EndTrail();
    }

    /// <summary>
    /// 销毁当前小鸟，准备让下一只小鸟飞出.
    /// </summary>
    protected virtual void ToNextBird()
    {
        GameManager._instance.birds.Remove(this);
        Destroy(gameObject);
        Instantiate(boom, transform.position, Quaternion.identity);
        GameManager._instance.DetermineWinOrLose();
    }

    /// <summary>
    /// 渲染弹弓
    /// </summary>
    private void DrawLine()
    {
        right.enabled = true;
        left.enabled = true;
        right.SetPosition(0, rightPos.position);
        right.SetPosition(1, transform.position);

        left.SetPosition(0, leftPos.position);
        left.SetPosition(1, transform.position);
    }

    private void AudioDisplay(AudioClip clip)
    {
        AudioSource.PlayClipAtPoint(clip, transform.position);
    }

    /// <summary>
    /// 允许小鸟使用技能.
    /// </summary>
    protected virtual void Skill()
    {
        sibideUP = false;
    }

    //在被撞体脚本中调用.
    public void Hurt()
    {
        renderHurt.sprite = hurt;
    }
}
