using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PhotonSever : MonoBehaviourPunCallbacks
{
    void Start()
    {
        Screen.SetResolution(960, 600, false); // PC 실행 시 해상도 설정
        PhotonNetwork.ConnectUsingSettings(); // 포톤 연결설정
    }

    public override void OnConnectedToMaster()
    {
        RoomOptions options = new RoomOptions(); // 방옵션설정
        options.MaxPlayers = 5; // 최대인원 설정
        PhotonNetwork.JoinOrCreateRoom("Room1", options, null); // 방이 있으면 입장하고 
                                                                // 없다면 방을 만들고 입장합니다.
    }
}
