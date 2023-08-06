using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class d_GameMgr : MonoBehaviour
{
    //접속된 플레이어 수를 표시할 Text UI 항목 변수
    public Text txtConnect;

    //접속 로그를 표시할 Text UI 항목 변수
    public Text txtLogMsg;
    //RPC호출을 위한 PhotonView
    private PhotonView pv;

    public TextMesh userIdText;

    //술래의 잡기 버튼
    public Button PCatch;

    void Awake()
    {
        //PhotonView component 할당
        pv = GetComponent<PhotonView>();

        //포톤클라우드의 네트워크 메시지 수신을 다시 연결
        PhotonNetwork.IsMessageQueueRunning = true;
        //룸에 입장 후 기존 접속자 정보를 출력
        GetConnectPlayerCount();

    }

    // Start is called before the first frame update
    void Start()
    {
        //캐치버튼 비활성화
        PCatch.interactable = false;
        //로그 메시지에 출력할 문자열 생성
        string msg = "/n<color=#00ff00>[" + PhotonNetwork.LocalPlayer.NickName + "] Connectec</color>";
        //RPC함수 호출
        pv.RPC("LogMsg", RpcTarget.AllBuffered, msg);

        // if(PlayerPrefs.HasKey("USER_ID"))
        {
            userIdText = gameObject.GetComponent<TextMesh>();
            string userId = PlayerPrefs.GetString("USER_ID");

            userIdText.text = userId;
            Debug.Log(userIdText);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

#region //룸 접속자 관련
    //룸 접속자 정보를 조회하는 함수
    void GetConnectPlayerCount()
    {
        //현재 입장한 룸 정보를 받아옴
        Room currRoom = PhotonNetwork.CurrentRoom;
        // Debug.Log("Room Name: " + currentRoom.Name);
        // Debug.Log("Room Player Count: " + currentRoom.PlayerCount);
        // Debug.Log("Max Players: " + currentRoom.MaxPlayers);
        // Debug.Log("Is Open: " + currentRoom.IsOpen);
        // Debug.Log("Is Visible: " + currentRoom.IsVisible);

        //현재 룸의 접속자 수와 최대 접속 가능한 수를 문자열로 구성한 후 Text UI항목에 출력
        txtConnect.text = currRoom.PlayerCount.ToString() + "/" + currRoom.MaxPlayers.ToString();
    }

    //네트워크 플레이어가 접속했을 때 호출되는 함수
    void OnPhotonPlayerconnected(Player newPlayer)
    {
        Debug.Log(newPlayer.ToStringFull());
        GetConnectPlayerCount();
    }

    //네트워크 플레이어가 룸을 나가거나 접속이 끊어졌을 때 호출되는 함수
    void OnPhotonPlayerDisconnected(Player outPlayer)
    {
        GetConnectPlayerCount();
    }
#endregion

#region //Player 메서드
    // // 로컬 플레이어의 Player 객체에 접근합니다.
    //         Player localPlayer = PhotonNetwork.LocalPlayer;
    //         Debug.Log("로컬 플레이어 닉네임: " + localPlayer.NickName);
    //         Debug.Log("로컬 플레이어 액터 번호: " + localPlayer.ActorNumber);
    //         Debug.Log("로컬 플레이어 사용자 ID: " + localPlayer.UserId);
    //         // ... 필요한 경우 다른 속성에 접근합니다.

    //         // 다른 플레이어들의 Player 객체에 접근합니다.
    //         foreach (Player player in PhotonNetwork.PlayerList)
    //         {
    //             Debug.Log("플레이어 닉네임: " + player.NickName);
    //             Debug.Log("플레이어 액터 번호: " + player.ActorNumber);
    //             Debug.Log("플레이어 사용자 ID: " + player.UserId);
    //             // ... 필요한 경우 다른 속성에 접근합니다.
#endregion

#region //Exit button + Log message
    
    [PunRPC]
    void LogMsg(string msg)
    {
        //로그 메시지 Text UI에 텍스트를 누적시켜 표시
        txtLogMsg.text = txtLogMsg.text + msg;
    //룸 나가기 버튼 클릭 이벤트에 연결될 함수
    }

    public void OnClickExitRoom()
    {
        
        //로그 메시지에 출력할 문자열 생성
        string msg = "/n<color=#00ff00>[" + PhotonNetwork.LocalPlayer.NickName + "] Connectec</color>";
        //RPC함수 호출
        pv.RPC("LogMsg", RpcTarget.AllBuffered, msg);
    

        //현재 룸을 빠져나가며 생성한 모든 네트워크 객체를 삭제
        PhotonNetwork.LeaveRoom();
    }

    //룸에서 접속 종료했을 때 호출되는 콜백 함수
    void OnLeftRoom()
    {
        //로비씬을 호출
        Application.LoadLevel("Lobby");
    }
    #endregion

#region //

    void OnCollisionEnter(Collision sul) 
    {
        if(sul.collider.tag == "sullae")
        {
            PCatch.interactable = true;
        }
    }
    
#endregion
}
