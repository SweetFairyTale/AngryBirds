using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSelect : MonoBehaviour {

    public int starsRequirement = 0;
    public bool unlocked = false;  //是否解锁(允许点击事件).

    private GameObject locking;
    private GameObject stars;

    private GameObject maps;
    public GameObject levelPanel;
    
    private void Start()
    {
        locking = transform.Find("Lock").gameObject;  //transform.Find(String),按名称取得子物体
        stars = transform.Find("StarRequire").gameObject;

        maps = transform.parent.gameObject;
        
        if(PlayerPrefs.GetInt("totalNum", 0) >= starsRequirement)  //键值??
        {
            unlocked = true;
        }

        if(unlocked)
        {
            locking.SetActive(false);
            stars.SetActive(true);

            //Text显示:
            
        }
    }

    /// <summary>
    /// 按钮点击事件，切换面板:地图->关卡.
    /// </summary>
    public void OnButtonSelect()
    {
        if(unlocked)
        {
            maps.SetActive(false);
            levelPanel.SetActive(true);
        }
    }

}
