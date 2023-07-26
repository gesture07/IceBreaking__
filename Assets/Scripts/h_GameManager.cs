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
            Debug.LogError("PhotonNetwork : Trying to Load a level but we are not the master Client");
            return;
        }
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
}
     
  
