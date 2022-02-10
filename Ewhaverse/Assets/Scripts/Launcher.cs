using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class Launcher : MonoBehaviourPunCallbacks
{
    string gameVersion = "1";   //����

    public InputField userIDText;   //userId

    void Awake()
    {
        //������ ���� �ε��ϸ�, ������ ����鵵 �ڵ� ��ũ
        PhotonNetwork.AutomaticallySyncScene = true;
        
        //���� ����
        PhotonNetwork.GameVersion = gameVersion;
        
        /* ���߿� userId �����ϰ� �Ǹ� ���� �ʿ� */
        //���� ����
        PhotonNetwork.ConnectUsingSettings();

    }

    //������ ���� ���� ������ �ڵ� ����
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
        Debug.Log("���� ���� ����!");
    }

    //������ ���� ���� ���н� �ڵ� ����
    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("���� ��...");
        PhotonNetwork.ConnectUsingSettings();
    }

    //���̵� ���� 
    public void IDConfirm()
    {
        AuthenticationValues authValues = new AuthenticationValues(userIDText.text);
        PhotonNetwork.AuthValues = authValues;
     
        //���� ����
        if (!PhotonNetwork.IsConnected)
            PhotonNetwork.ConnectUsingSettings();

    }

    public override void OnJoinedLobby()
    {
        Debug.Log("�κ� ����");
        //PhotonNetwork.FindFriends(new string[] { "1" });
    }


}
