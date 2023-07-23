
using UnityEngine;
using Photon.Pun;

namespace Com.MyCompany.MyGame{
    public class Launcher : MonoBehaviour
    {
        #region Private Serializable Fields

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
            Connect();//커넥트 함수 호출
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
    
    }
}