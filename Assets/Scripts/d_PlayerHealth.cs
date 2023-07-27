using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class d_PlayerHealth : MonoBehaviour
{
    //시작 체력 = 100
    public int stratingHealth = 100;
    //현재 체력
    public int currentHealth;
    //체력 게이지 UI와 연결된 변수
    public Slider healthSlider;
    //데미지를 입을 때 화면을 빨갛게 만들기 위한 투명한 이미지
    public Image damageImage;
    //데미지를 입었을 때 재생할 오디오
    public AudioClip deathClip;

    //애니메이터 컨트롤러에 매개변수를 전달하기 위해 연결한 animation컴포넌트
    Animator anim;
    //플레이어 게임 오브젝트에 붙어있는 오디오 소스 컴포넌트
    //효과음을 재생할 때 필요합니다
    AudioSource playerAudio;
    //플레이어의 움직임을 관리하는 playerMovement스크립트 컴포넌트
    PlayerMovement playerMovement;
    //플레이어가 죽었는지 저장하는 플래그
    bool isDead;

    void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
