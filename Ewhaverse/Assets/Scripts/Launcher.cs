using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class Launcher : MonoBehaviourPunCallbacks
{
    string gameVersion = "1";   //����
    public Text text;   //���� ���� �˸� 

    public InputField userIDText;   //userId

    void Awake()
    {
        //������ ���� �ε��ϸ�, ������ ����鵵 �ڵ� ��ũ
        PhotonNetwork.AutomaticallySyncScene = true;
        
        //���� ����
        PhotonNetwork.GameVersion = gameVersion;
        
        //���� ����
        //if(!PhotonNetwork.IsConnected)
            //PhotonNetwork.ConnectUsingSettings();

        

    }

    //������ ���� ���� ������ �ڵ� ����
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
        text.text = "���� ���� ����!";
    }

    //������ ���� ���� ���н� �ڵ� ����
    public override void OnDisconnected(DisconnectCause cause)
    {
        text.text = "���� ��...";
        PhotonNetwork.ConnectUsingSettings();
    }

    //���̵� �Է�
    public void IDConfirm()
    {
        AuthenticationValues authValues = new AuthenticationValues(userIDText.text);
        PhotonNetwork.AuthValues = authValues;
        Debug.Log(PhotonNetwork.AuthValues.UserId);
        //���� ����
        if (!PhotonNetwork.IsConnected)
            PhotonNetwork.ConnectUsingSettings();

    }

    public override void OnJoinedLobby()
    {
        text.text = "�κ� ����";
        //PhotonNetwork.FindFriends(new string[] { "1" });
    }


}
