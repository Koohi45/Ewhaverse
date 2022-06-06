using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

/*�� ������ �����ϴ� Ŭ����*/
public class PlayerInfo : MonoBehaviourPunCallbacks
{
    public static PlayerInfo info;

    static GameObject my_player;   //�� �÷��̾�
    GameObject child;

    public static bool is_customizing;    //Ŀ���͸���¡ �� Ȯ��

    private static float pos_x;    //transform.poition
    private static float pos_y;
    private static float pos_z;

    //�ƹ�Ÿ ����
    public AvatarInfo avatarinfo = new AvatarInfo();


    /*�̱��� ���*/
    private void Awake()
    {
        if (info != null)
        {
            Destroy(gameObject);
            return;
        }
        info = this;
        DontDestroyOnLoad(gameObject);

        PhotonPeer.RegisterType(typeof(AvatarInfo), 0, AvatarInfo.Serialize, AvatarInfo.Deserialize);
    }

    // Start is called before the first frame update
    void Start()
    {
        is_customizing = false;
        pos_x = 0f;
        pos_y = 2f;
        pos_z = 0f;
    }
  
    //Ŀ���͸���¡�� ���� �� ��ġ ���
    public static void PresentLoca()
    {
        if (PhotonNetwork.CurrentRoom.Name == "Square")
        {
            if (my_player != null)
            {
                pos_x = my_player.transform.position.x;
                pos_y = my_player.transform.position.y;
                pos_z = my_player.transform.position.z;
            }
        }
        Debug.LogFormat("{0}, {1}, {2}", pos_x, pos_y, pos_z);
    }

    public static void FindPlayerObject()
    {
        my_player = GameObject.Find(PhotonNetwork.AuthValues.UserId).gameObject;
        //child = my_player.transform.Find("avatar").gameObject;
    }

    public static void UpdateSquarePos()
    {       
        my_player.transform.position = new Vector3(pos_x, pos_y, pos_z);
            //child.transform.rotation = Quaternion.Euler(0, rot, 0);
    }


    public void ReadAvatarInfo()
    {
        string result = File.ReadAllText(Application.persistentDataPath + "/CustomJson.txt");
        string[] info = result.Split(new string[] { "," }, StringSplitOptions.None);
        
        //�Ǻλ�
        String[] skincolor = info[0].Split(new string[] { "/" }, StringSplitOptions.None);
        avatarinfo.skin.r = float.Parse(skincolor[0]);
        avatarinfo.skin.g = float.Parse(skincolor[1]);
        avatarinfo.skin.b = float.Parse(skincolor[2]);

        //��
        avatarinfo.eye.type = int.Parse(info[1].Replace("Eye", ""));
        String[] eyecolor = info[2].Split(new string[] { "/" }, StringSplitOptions.None);
        avatarinfo.eye.r = float.Parse(eyecolor[0]);
        avatarinfo.eye.g = float.Parse(eyecolor[1]);
        avatarinfo.eye.b = float.Parse(eyecolor[2]);
        
        //��
        avatarinfo.mouse.type = int.Parse(info[3].Replace("Mou", ""));

        //�Ӹ�
        avatarinfo.hair.front_type = int.Parse(info[4].Replace("HaF", ""));
        avatarinfo.hair.back_type = int.Parse(info[5].Replace("HaB", ""));
        String[] haircolor = info[6].Split(new string[] { "/" }, StringSplitOptions.None);
        avatarinfo.hair.r = float.Parse(haircolor[0]);
        avatarinfo.hair.g = float.Parse(haircolor[1]);
        avatarinfo.hair.b = float.Parse(haircolor[2]);
        
        //��
        avatarinfo.cloth.top = int.Parse(info[7].Replace("Top", ""));
        avatarinfo.cloth.bottom = int.Parse(info[8].Replace("Bot", ""));
        avatarinfo.cloth.shoes = int.Parse(info[9].Replace("Sho", ""));
        avatarinfo.cloth.acc = int.Parse(info[10].Replace("Acc", ""));

    }


}
