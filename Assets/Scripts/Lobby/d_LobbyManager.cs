using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class d_LobbyManager : MonoBehaviourPunCallbacks
{
#region // 변수 선언
    //게임 버전
    private string gameVersion = "v0.1";
    //플레이어 이름을 입력하는 UI항목 연결
    public InputField userId;
    //룸 이름을 입력받을 UI항목 연결 변수
    public InputField roomName;
    

    //네트워크 정보를 표시할 텍스트
    public Text connectionOnfoText;
    //룸 접속 버튼
    public Button joinButton;
    public Button roomCreateButton;

    
    //RoomItem이 child로 생성될 Parent객체
    public GameObject scrollcontents;
    //룸 목록만큼 생성될 RoomItem프리팹
    public GameObject roomItem;
#endregion

    //게임 실행과 동시에 마스터 서버 접속 시도
    void Start()
    {
        //접속에 필요한 정보(게임 버전) 설정
        PhotonNetwork.GameVersion = gameVersion;
        if(!PhotonNetwork.IsConnected)
        {
            //설정한 정보로 마스터 서버 접속 시도
            PhotonNetwork.ConnectUsingSettings();
        }
        //사용자 이름 설정
        userId.text = GetUserId();

        
        //룸 이름을 무작위로 설정
        roomName.text = "ROOM_" + Random.Range(0, 999).ToString("000");
        
        //룸 접속 버튼 잠시 비활성화
        joinButton.interactable = false;
        roomCreateButton.interactable = false;
        //접속 시도 중임을 텍스트로 표시
        connectionOnfoText.text = "마스터 서버에 접속 중...";

        //방 목록을 얻습니다.
        PhotonNetwork.JoinLobby();
    }

#region //서버 접속 + 방 접속
    //마스터 서버 접속 성공 시 자동 실행
    public override void OnConnectedToMaster()
    {
        //룸 접속 버튼 활성화
        joinButton.interactable = true;
        roomCreateButton.interactable = true;
        //접속 정보 표시
        connectionOnfoText.text = "온라인: 서버와 연결됨";
        userId.text = GetUserId();
    }

    //마스터 서버 접속 실패 시 자동 실행
    public override void OnDisconnected(DisconnectCause cause)
    {
        //룸 접속 버튼 비활성화
        joinButton.interactable = false;
        roomCreateButton.interactable = false;
        //접속 정보 표시
        connectionOnfoText.text = "오프라인: 서버와 연결되지 않음. \n 접속 재시도 중...";

        //마스터 서버로의 재접속 시도
        PhotonNetwork.ConnectUsingSettings();
    }

    //랜덤룸 접속 시도
    public void Connect()    
    {
        //중복 접속 시도를 막기 위해 접속 버튼 잠시 비활성화
        joinButton.interactable = false;
        roomCreateButton.interactable = false;
        

        //마스터 서버에 접속 중이라면
        if(PhotonNetwork.IsConnected)
        {
            //룸 접속 실행
            connectionOnfoText.text = "룸에 접속...";

            //로컬 플레이어의 이름을 설정
            PhotonNetwork.LocalPlayer.NickName = userId.text;
            //플레이어의 이름을 저장
            PlayerPrefs.SetString("USER_ID", userId.text);

            //접속 과정에 대한 로그를 출력
            Debug.Log("랜덤 룸접속 과정 :" + PhotonNetwork.NetworkClientState);
            /*Connected, connectingToNameServer, ConnectedToNameServer, 
            ConnectingToMasterserver, ConnectedToMaster, 
            Joining, Joined, 
            Leaving, Disconnecting, Disconnected
            */

            //씬을 이동하는 동안 포톤클라우드 서버로부터 네트워크 메시지 수신 중단
            PhotonNetwork.IsMessageQueueRunning = false;
            
            //랜덤룸으로 입장
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
    
    //방제작 버튼 클릭시
    public void OnClickCreateRoom()
    {
        //중복 접속 시도를 막기 위해 접속 버튼 잠시 비활성화
        roomCreateButton.interactable = false;
        joinButton.interactable = false;
        
        //마스터 서버에 접속 중이라면
        if(PhotonNetwork.IsConnected)
        {
            string _roomName = roomName.text;
            //룸 이름이 없거나 null일 경우 룸 이름 지정
            if(string.IsNullOrEmpty(roomName.text))
            {
                _roomName = "ROOM_" + Random.Range(0, 999).ToString("000");
            }

            //로컬 플레이어의 이름을 설정
            PhotonNetwork.LocalPlayer.NickName = userId.text;
            //플레이어의 이름을 저장
            PlayerPrefs.SetString("USER_ID", userId.text);

            //생성할 룸의 조건 설정
            RoomOptions roomOptions = new RoomOptions();
            roomOptions.IsOpen = true;
            roomOptions.IsVisible = true;
            roomOptions.MaxPlayers = 10;

            //지정한 조건에 맞는 룸 생성 함수
            PhotonNetwork.CreateRoom(_roomName, roomOptions, null);
        }
        else
        {
            //마스터 서버에 접속 중이 아니라면 마스터 서버에 접속 시도
            connectionOnfoText.text = "오프라인: 서버와 연결되지 않음. \n 재접속 시도 중...";
            //마스터 서버로의 재접속 시도
            PhotonNetwork.ConnectUsingSettings();
        }
        
    }

    //룸 생성 실패할 때 호출되는 콜백함수
    void OnPhotonCreateRoomFailed(object[] codeAndMsg)
    {
        Debug.Log("Create Room Failed = " + codeAndMsg[1]);
        Debug.Log("제작 방 접속 과정 :" + PhotonNetwork.NetworkClientState);
        /*Connected, connectingToNameServer, ConnectedToNameServer, 
        ConnectingToMasterserver, ConnectedToMaster, 
        Joining, Joined, 
        Leaving, Disconnecting, Disconnected
        */
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

    string GetUserId()
    {
        string userId = PlayerPrefs.GetString("USER_ID");

        if(string.IsNullOrEmpty(userId))
        {
            userId = "USER_" + Random.Range(0, 999).ToString("000");
        }

        return userId;
    }
#endregion

    //생성된 룸 목록 업데이트
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        Debug.Log("방 목록 업데이트");
        
        //룸 목록을 다시 받았을 때 갱신하기 위해 기존에 생성된 RoomItem을 삭제
        foreach(GameObject obj in GameObject.FindGameObjectsWithTag("RoomItem"))
        {
            Destroy(obj);
        }

        //Grid Layout Group Component의 constraint Count 값을 증가시킬 변수
        int rowCount = 0;
        //스크롤 영역 초기화
        scrollcontents.GetComponent<RectTransform>().sizeDelta = Vector2.zero;
        
        foreach (RoomInfo roomInfo in roomList)
        {
            //RoomItem 프리랩을 동적으로 생성
            GameObject room = Instantiate(roomItem);
            //생성한 RoomItem 프리랩의 Parent를 지정
            room.transform.SetParent(scrollcontents.transform, false);

            //생성한 RoomItem에 표시하기 위한 텍스트 정보 전달
            d_RoomData roomData = room.GetComponent<d_RoomData>();
            roomData.roomName = roomInfo.Name;
            roomData.connectPlayer = roomInfo.PlayerCount;
            roomData.MaxPlayers = roomInfo.MaxPlayers;
            //텍스트 정보를 표시
            roomData.DispRoomData();


            //RoomItem의 Button component에 클릭 이벤트를 동적으로 연결
            roomData.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(delegate{onClickRoomItem(roomData.roomName);});
            //Grid Layout Group component의 constraint count값을 증가
            scrollcontents.GetComponent<GridLayoutGroup>().constraintCount = ++rowCount;
            //스크롤 영역의 높이를 증가시킴
            scrollcontents.GetComponent<RectTransform>().sizeDelta += new Vector2(0,20);
        }
    }


    //RoomItem이 클릭되면 호출될 이벤트 연결 함수
    void onClickRoomItem(string roomName)
    {
        //로컬 플레이어의 이름을 설정
        PhotonNetwork.LocalPlayer.NickName = userId.text;
        //플레이어의 이름을 저장
        PlayerPrefs.SetString("USER_ID", userId.text);

        //인자로 전달된 이름에 해당하는 룸으로 입장
        PhotonNetwork.JoinRoom(roomName);
    }
}
