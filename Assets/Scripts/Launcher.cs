
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;//콜백으로 바꾸면서 생김

namespace Com.MyCompany.HRGame{
    public class Launcher : MonoBehaviourPunCallbacks//pun콜백 구현해보기
    //public class Launcher : MonoBehaviour
    {
        #region Private Serializable Fields

        [Tooltip("The maximum number of players per room. When a room is full, it can't be joined by new players, and so new room will be created")]
        [SerializeField]
        //룸에 참여할 수 있는 최대 플레이어 수. 방이 꽉 차면 새로운 방
        private byte maxPlayersPerRoom = 4;
       

        #endregion

        #region Private Fields
        //게임 버전을 나타냄 //이미 출시되어 프로젝트에서 큰 변경사항있기 전까지는 "1"  
        string gameVersion = "1"; 
        #endregion
        //MonoBehaviour은 초기 초가화 단계에서 유니티에 의해 게임 오브젝트를 호출한다.
        #region MonoBehaviour CallBacks

        void Awake(){
            //중요//마스터 클라이언트의 LoadLevel()과 동일한 룸에 있는 모든 클라이언트의 LoadLevel이 자동으로 동기화.
            PhotonNetwork.AutomaticallySyncScene = true;
            //플레이어 수에 따라 크기가 변경되는 경기장.
            //로드된 씬은 연결하고 있는 모든 플레이어들이 동일.
            //위의 값이 true 일 때, masterclient는 PhotonNetwork.LoadLevel()을 호출 할 수 있고 
            //모든 연결된 플레이어들은 동일한 레벨을 자동적으로 로드

        }
        
        void Start()
        {
           // Connect();//커넥트 함수 호출//2.로비 uI 에서 제거->플레이버튼을 누르기 전까지는 커넥트 돠지 않는다.
            //이미 연결된 경우 랜덤 룸에 가입하려고 시도
            //아직 연결되지 않은 경우 Phonton Cloud Network에 이 응용 프로그램 인스턴스 연결

        }
        
        public void Connect(){

            //연결되어 있는지 확인하고, 연결되어 있으면 참여하고. 그렇지 않으면 서버에 연결을 시작.
            if(PhotonNetwork.IsConnected){
                
                ////#중요 이 시점에서 랜덤 룸에 참여. 실패하면 OnJoinRandomFailed()에 알림이 표시되고 생성.
                PhotonNetwork.JoinRandomRoom();

            }
            else{
                //중요. Photon Online Server에 연결.
                PhotonNetwork.GameVersion = gameVersion;
                //포튼 클라우드에 연결 되는 시작 시점
                PhotonNetwork.ConnectUsingSettings();
                
            
            }

        }
    #endregion

    //씬을 실행 시키면 PUN 접속에 성공하여 기존의 룸에 참여를 시도하거나 새로 룸을 생성하고 생성된 그 룸에 참여.
    #region MonoBehaviourPunCallbacks Callbacks
    
    public override void OnConnectedToMaster(){
        Debug.Log("PUN Basics Tutorial/Launcher: OnConnectedToMaster() was called by PUN");
        // 잠재적으로 존재하는 방이 있으면 가입, 그렇지 않으면 OnJoin Random Failed()로 다시 호출됩니다
        PhotonNetwork.JoinRandomRoom();
    
    }
    
   public override void OnDisconnected(DisconnectCause cause)
    {
    Debug.LogWarningFormat("PUN Basics Tutorial/Launcher: OnDisconnected() was called by PUN with reason {0}", cause);
    }
    //룸의 무작위 입장이 실패->통지를 받게 되며 룸을 실제로 생성해야 함.
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
    Debug.Log("PUN Basics Tutorial/Launcher:OnJoinRandomFailed() was called by PUN. No random room available, so we create one.\nCalling: PhotonNetwork.CreateRoom");

    // #중요: 랜덤 방에 입장하는데 실패->새로운 방을 만들자// 위에 설정된 맥스 플레이어 수만큼
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = maxPlayersPerRoom });
    }
    //성공적으로 룸에 참가
    public override void OnJoinedRoom()
    {
        Debug.Log("PUN Basics Tutorial/Launcher: OnJoinedRoom() called by PUN. Now this client is in a room.");
    }

    #endregion

    }
}