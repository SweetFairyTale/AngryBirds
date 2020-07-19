using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  // 1 

public class GameManager : MonoBehaviour {

    public static GameManager _instance;  //单例
    public List<BirdController> birds;
    public List<PigController> pig;
    private Vector3 firstPlace;

    public GameObject win;
    public GameObject lose;

    public GameObject[] stars;

    private int storeAllStars = 0;  //星星总数.

    private int totalNum = 10;

    void Start()
    {
        _instance = this;
        Init();
        if (birds.Count > 0)
        {
            firstPlace = new Vector3(-5.3f, -1.6f, 0);
        }

        //win = GameObject.Find("win");
        //lose = GameObject.Find("lose");
    }

    private void Init()
    {
        for(int i = 0; i < birds.Count; i++)
        {
            if(i == 0)
            {
                //birds[i].transform.position = firstPlace;   
                //error:第一只小鸟位置有误，原因不明，调至Determine函数中执行
                birds[i].enabled = true;
                birds[i].sp.enabled = true;
            }
            else
            {
                birds[i].enabled = false;
                birds[i].sp.enabled = false;
            }
        }
    }

    //根据界面中鸟和猪的数目(List<泛型>.Count)，判断游戏输赢，调用结算界面.
    public void DetermineWinOrLose()
    {
        if(pig.Count > 0)
        {
            if(birds.Count > 0)
            {
                //next bird preparing
                birds[0].transform.position = firstPlace;
                Init();
            }
            else
            {
                //lose
                lose.SetActive(true);
            }
        }
        else
        {
            //win          
            win.SetActive(true);
        }
    }
	
    /// <summary>
    /// 显示游戏结算时的星星数
    /// </summary>
    public void ShowStars()
    {
        StartCoroutine("ShowStarOnebyOne");
    }

    //协程方法 使星星一颗一颗显示
    IEnumerator ShowStarOnebyOne()
    {
        for ( ; storeAllStars < birds.Count + 1; storeAllStars++)
        {
            if(storeAllStars >= stars.Length)  //防止小鸟数量大于3时星星数组越界.
            {
                break;
            }
            yield return new WaitForSeconds(0.35f);  //每0.35秒显示一颗.
            stars[storeAllStars].SetActive(true);    //场景中三个star的默认状态也要预先调为false.
        }
    }

    //按钮响应事件，跳转(加载)其他场景时需引入命名空间1.
    public void Replay()
    {
        SaveData();
        SceneManager.LoadScene(2);  //(int)2 场景03-Game编号.(见File->Build Settings)
    }

    public void Home()
    {
        SaveData();
        SceneManager.LoadScene(1);
    }

    //向键值NowLevel存储星星个数的方法.
    public void SaveData()
    {
        if(storeAllStars > PlayerPrefs.GetInt(PlayerPrefs.GetString("nowLevel")))
        {
            PlayerPrefs.SetInt(PlayerPrefs.GetString("nowLevel"), storeAllStars);
        }

        //存储每关星星的总数.
        int sum = 0;
        for(int i = 1; i <= totalNum; i++)
        {
            sum += PlayerPrefs.GetInt(PlayerPrefs.GetString("level" + i.ToString()));
        }
        PlayerPrefs.SetInt("totalNum", sum);
    }
}
