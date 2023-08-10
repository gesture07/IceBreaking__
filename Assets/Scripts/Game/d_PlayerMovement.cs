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
    //수신된 위치와 회전값을 저장할 변수
    private Vector3 receivePos;
    private Quaternion receiveRot;
    //수신된 좌표로의 이동 및 회전 속도의 민감도
    public float damping = 10.0f;

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
        else
        {
            //수신된 좌표로 보간한 이동 처리
            transform.position = Vector3.Lerp(transform.position,
                                            receivePos,
                                            Time.deltaTime * damping);
            transform.rotation = Quaternion.Slerp(transform.rotation,
                                                receiveRot,
                                                Time.deltaTime * damping);
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
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        Debug.Log("OnPhotonSerializeView Called"); 
        //자신의 로컬 캐릭터인 경우 자신의 데이터를 다른 네트워크 유저에게 송신
        if(stream.IsWriting){
            stream.SendNext(tr.position);
            stream.SendNext(tr.rotation);
            stream.SendNext(ice); // 로컬 플레이어의 ice 값을 전송
            Debug.Log("OnPhotonSerializeView sendNext");
        }
        else
        {
            receivePos = (Vector3)stream.ReceiveNext();
            receiveRot = (Quaternion)stream.ReceiveNext();
            ice = (bool)stream.ReceiveNext(); // 네트워크를 통해 수신된 ice 값을 저장
            Debug.Log("OnPhotonSerializeView ReceiveNext");
        }
    }
   
}
