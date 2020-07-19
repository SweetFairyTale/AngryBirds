using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour {
       
    public bool isSelect = false;  //当前关卡是否可玩.

    public Sprite levelBG;
    private Image image;
    public GameObject[] stars;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    private void Start()
    {
        if(transform.parent.GetChild(0).name == gameObject.name) //只有第一关符合 默认解锁.
        {
            isSelect = true;
        }
        else  //其他关卡是否解锁取决于其前一关，前一关星数>0时解锁当前关.
        {
            int previous = int.Parse(gameObject.name) - 1;
            if(PlayerPrefs.GetInt("level" + previous.ToString()) > 0)
            {
                isSelect = true;
            }
        }

        if(isSelect)
        {
            image.overrideSprite = levelBG;
            transform.Find("Level").gameObject.SetActive(true);

            //
            int count = PlayerPrefs.GetInt("level" + gameObject.name);
            if(count > 0)
            {
                for (int i = 0; i < count; i++)
                    stars[i].SetActive(true);
            }
        }
    }

    /// <summary>
    /// 处理当前关卡被激活时 星星总数的问题.
    /// </summary>
    public void Selected()
    {
        if(isSelect)
        {
            PlayerPrefs.SetString("nowLevel", "level" + gameObject.name);
            SceneManager.LoadScene(2);
        }
    }
}
