using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdTrail : MonoBehaviour {

    public WeaponTrail myTrail;

    private float t = 0.033f;
    private float tempT = 0;
    private float animationIncrement = 0.003f;

    void Start()
    {
        // 默认没有拖尾效果
        myTrail.SetTime(0.0f, 0.0f, 1.0f);
    }

    void LateUpdate()
    {
        t = Mathf.Clamp(Time.deltaTime, 0, 0.066f);

        if (t > 0)
        {
            while (tempT < t)
            {
                tempT += animationIncrement;

                if (myTrail.time > 0)
                {
                    myTrail.Itterate(Time.time - t + tempT);
                }
                else
                {
                    myTrail.ClearTrail();
                }
            }

            tempT -= t;

            if (myTrail.time > 0)
            {
                myTrail.UpdateTrail(Time.time, t);
            }
        }
    }

    /// <summary>
    /// 启用拖尾效果
    /// </summary>
    public void StartTrail()
    {
        //设置拖尾时长
        myTrail.SetTime(2.0f, 0.0f, 1.0f);
        //开始进行拖尾
        myTrail.StartTrail(0.5f, 0.4f);
    }

    /// <summary>
    /// 结束拖尾效果
    /// </summary>
    public void EndTrail()
    {
        //清除拖尾
        myTrail.ClearTrail();
    }
}
