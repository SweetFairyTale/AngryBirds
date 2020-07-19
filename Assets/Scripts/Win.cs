using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win : MonoBehaviour {

    
    //win动画最后一帧事件调用此方法    
    public void Show()
    {
        GameManager._instance.ShowStars();
    }
     
}
