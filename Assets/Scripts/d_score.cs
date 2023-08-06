using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class d_score : MonoBehaviour
{
    //Text UI항목 연결을 위한 변수
    public Text txtScore;
    //누적 점수를 기록하기 위한 변수
    private int TotScore = 0;

    // Start is called before the first frame update
    void Start()
    {
        //처음 실행 후 저장된 스코어 정보 로드
        TotScore = PlayerPrefs.GetInt("TOT_SCORE", 0);
        DispScore(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DispScore(int score)
    {
        TotScore += score;
        //txtScore.txt = "score<color=#ff0000" + TotScore.ToString() + "</color>";

        //스코어 저장
        PlayerPrefs.SetInt("TOT_SCORE", TotScore);
    }
}
