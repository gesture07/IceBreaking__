using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class d_LobbyManager : MonoBehaviourPunCallbacks
{
    //게임 버전
    private string gameVersion = "1";

    //네트워크 정보를 표시할 텍스트
    public Text connectionOnfoText;
    //룸 접속 버튼
    public Button joinButton;

    //게임 실행과 동시에 마스터 서버 접속 시도
    void Start()
    {
        //접속에 필요한 정보(게임 버전) 설정
        PhotonNetwork.GameVersion = gameVersion;
        //설정한 정보로 마스터 서버 접속 시도
        PhotonNetwork.ConnectUsingSettings();

        //룸 접속 버튼 잠시 비활성화
        joinButton.interactable = false;
        //접속 시도 중임을 텍스트로 표시
        connectionOnfoText.text = "마스터 서버에 접속 중...";
    }

    //마스터 서버 접속 성공 시 자동 실행
    public override void OnConnectedToMaster()
    {
        //룸 접속 버튼 활성화
        joinButton.interactable = true;
        //접속 정보 표시
        connectionOnfoText.text = "온라인: 서버와 연결됨";
    }

    //마스터 서버 접속 실패 시 자동 실행
    public override void OnDisconnected(DisconnectCause cause)
    {
        //룸 접속 버튼 비활성화
        joinButton.interactable = false;
        //접속 정보 표시
        connectionOnfoText.text = "오프라인: 서버와 연결되지 않음. \n 접속 재시도 중...";

        //마스터 서버로의 재접속 시도
        PhotonNetwork.ConnectUsingSettings();
    }

    //룸 접속 시도
    public void Connect()    
    {
        //중복 접속 시도를 막기 위해 접속 버튼 잠시 비활성화
        joinButton.interactable = false;
        

        //마스터 서버에 접속 중이라면
        if(PhotonNetwork.IsConnected)
        {
            //룸 접속 실행
            connectionOnfoText.text = "룸에 접속...";
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            //마스터 서버에 접속 중이 아니라면 마스터 서버에 접속 시도
            connectionOnfoText.text = "오프라인: 서버와 연결되지 않음. \n 재접속 시도 중...";
            //마스터 서버로의 재접속 시도
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    //빈 방이 없어 랜덤 룸 참가에 실패한 경우 자동 실행
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        //접속 상태 표시
        connectionOnfoText.text = "빈 방이 없음, 새로운 방 생성...";
        //최대 4명을 수용 가능한 빈 방 생성
        PhotonNetwork.CreateRoom(null, new RoomOptions{MaxPlayers = 4});
    }

    //룸에 참가 완료된 경우 자동 실행
    public override void OnJoinedRoom()
    {
        //접속 상태 표시
        connectionOnfoText.text = "방 참가 성공";
        //모든 룸 참가자가 Main씬을 로드하게 함
        PhotonNetwork.LoadLevel("Game");
        // SceneManager.LoadScene("Game");
    }
}
