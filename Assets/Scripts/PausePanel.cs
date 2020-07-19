using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausePanel : MonoBehaviour {

    private Animator anim;
    public GameObject pauseButton;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    /// <summary>
    /// 点击暂停按钮时触发该事件.
    /// </summary>
    public void Pause()
    {  
        anim.SetBool("isPause", true);  //1.播放面板动画
        pauseButton.SetActive(false);   //2.隐藏暂停按钮

        //*pause按钮必须在pausePanel物体层级下面.(?)
    }

    /// <summary>
    /// 点击继续按钮时触发该事件.
    /// </summary>
    public void Continue()
    {
        //使场景恢复可动状态，然后播放面板退出动画.
        Time.timeScale = 1;
        anim.SetBool("isPause", false);
    }

    public void Retry()
    {
        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
    }

    public void Home()
    {
        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    /// <summary>
    /// 暂停面板动画播放完后触发该事件，暂停游戏.
    /// </summary>
    public void PauseAnmiEnd()
    {
        Time.timeScale = 0;
    }

    public void ResumeAnimEnd()
    {
        pauseButton.SetActive(true);
    }
}
