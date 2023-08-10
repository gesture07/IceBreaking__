using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Cinemachine;
public class d_PlayerMovement : MonoBehaviour
{
    //컴포넌트 캐시처리를 위한 함수
    private Transform tr;
    private new Camera camera;

    public float MoveSpeed = 0.3f;
    public float turnSpeed = 80.0f;

     public bool ice = false;
    //PhotonView 컴포너트 캐시 처리를 위한 변수
    private PhotonView pv;
    //시네머신 가상 카메라를 저장할 변수
    private CinemachineVirtualCamera virtualCamera;

    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();
        camera = Camera.main;

        pv = GetComponent<PhotonView>();
        virtualCamera = GameObject.FindObjectOfType<CinemachineVirtualCamera>();

        //PhotonView가 자신의 것일 겻우 시네머신 가상카메라를 연결
        if(pv.IsMine)
        {
            virtualCamera.Follow = transform;
            virtualCamera.LookAt = transform;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        float r = Input.GetAxis("Mouse X");

        if(Input.GetKey(KeyCode.R)){
            ice = true;
        }

        tr.Rotate(Vector3.up * turnSpeed * Time.deltaTime* r);
        //자신이 생성한 네트워크 객체만 컨트롤
        if(pv.IsMine&&!ice)
        {
           PlayerInput();
        }
    
    }

    void PlayerInput()
    {
        if(Input.GetKey(KeyCode.D))
        {
            transform.position += new Vector3(MoveSpeed,0,0);
        }
        else if(Input.GetKey(KeyCode.A))
        {
            transform.position += new Vector3(-MoveSpeed,0,0);
        }
        else if(Input.GetKey(KeyCode.W))
        {
            transform.position += new Vector3(0,0,MoveSpeed);
        }
        else if(Input.GetKey(KeyCode.S))
        {
            transform.position += new Vector3(0,0,-MoveSpeed);
        }
    }

   
}
