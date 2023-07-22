using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;


public class PhotonSever : MonoBehaviourPunCallbacks
{
    void Start()
    {
        Screen.SetResolution(960, 600, false); // PC
        PhotonNetwork.ConnectUsingSettings(); // ���� ���ἳ��
    }

    //public override void OnConnectedToMaster()
    //{
      //  RoomOptions options = new RoomOptions(); // ��ɼǼ���
       // options.MaxPlayers = 5; // �ִ��ο� ����
       // PhotonNetwork.JoinOrCreateRoom("Room1", options, null); // ���� ������ �����ϰ� 
       //                                                         // ���ٸ� ���� ����� �����մϴ�.
   // }
   public override void OnConnectedToMaster() => PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions { MaxPlayers = 6}, null);

   public override void OnJoinedRoom() {

    PhotonNetwork.Instantiate("Player",Vector3.zero, Quaternion.identity);//프리팹 이름, 백터값, 회전값

   }


}
