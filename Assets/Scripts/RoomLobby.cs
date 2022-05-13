using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;

public class RoomLobby : MonoBehaviourPunCallbacks
{
    PlayerControl player;

    void Start()
    {

    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    //�뱤�� �ǵ��ư��� ��ư �Լ�
    public void BackToSquare()
    {
        PhotonNetwork.LoadLevel("Square");
        PhotonNetwork.JoinLobby();
    }
}
