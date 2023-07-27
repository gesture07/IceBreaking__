using System;
using System.Collections;

using UnityEngine;
using UnityEngine.SceneManagement;

using Photon.Pun;
using Photon.Realtime;

public class h_GameManager : MonoBehaviourPunCallbacks
{
    #region Private Methods

    void LoadArena()
    {
        if(!PhotonNetwork.IsMasterClient)//마스터 클라이언트인지 체크크
        {
            //너님 방장 아니세요
            Debug.LogError("PhotonNetwork : Trying to Load a level but we are not the master Client");
            return;
        }
        //로딩 레벨과 플레이어 수를 알려줌
        Debug.LogFormat("PhotonNetwork : Loading Level : {0}", PhotonNetwork.CurrentRoom.PlayerCount);//"PhotonNetwork.CurrentRoom.PlayerCount 속성을 사용하여 플레이어 수를 가져
        PhotonNetwork.LoadLevel("Room for "+ PhotonNetwork.CurrentRoom.PlayerCount); //PhotonNetwork.LoadLevel()을 사용하여 새로운 레벨 또는 씬을 로드합니다
    }

    #endregion

    #region Photon Callbacks

    public override void OnLeftRoom()
    {
        SceneManager.LoadScene(0);
    }

    #endregion

    #region Public Methods

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    #endregion

    //GameManager가 플레이어 연결과 연결 해제를 리슨 할 필요가 있음
    #region Photon Callbacks

    public override void OnPlayerEnteredRoom(Player other)
    {
        Debug.LogFormat("OnPlayerEnteredRoom() {0}", other.NickName); // not seen if you're the player connecting

        if (PhotonNetwork.IsMasterClient)
        {
            Debug.LogFormat("OnPlayerEnteredRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient); // called before OnPlayerLeftRoom

            LoadArena();
        }
    } 
        
    public override void OnPlayerLeftRoom(Player other)
    {
        Debug.LogFormat("OnPlayerLeftRoom() {0}", other.NickName); // 다른 플레이어가 나가는 걸 확인가능

        if (PhotonNetwork.IsMasterClient)
        {
            Debug.LogFormat("OnPlayerLeftRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient); // called before OnPlayerLeftRoom

            LoadArena();//플레이어 수와 로딩 레벨을 알려줌
        
        }
    } 
    #endregion

}
     
  
