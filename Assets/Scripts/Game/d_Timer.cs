using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class d_Timer : MonoBehaviour
{

    public float LimitTime;
    public Text text_timer;

    // Update is called once per frame
    void Update()
    {
       LimitTime -= Time.deltaTime;
       text_timer.text = "남은 시간 : "+ Mathf.Round(LimitTime); 
    }
}
