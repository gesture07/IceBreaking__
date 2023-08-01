using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class d_MergeScenes : MonoBehaviour
{
    //두 개의 씬을 한번에 보여줘야 할 때 사용
    public void OnClickStartBtn()
    {
        Debug.Log("Click Button");
        //기본이 되는 씬
        //Application.LoadLevel("sceneLevel1");
        //추가되는 씬
        //Application.LoadLevelAdditive("sceneLevel2");
    }
}
