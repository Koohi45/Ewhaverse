using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

/*�� ������ �����ϴ� Ŭ���� 
 */
public class PlayerInfo : MonoBehaviourPunCallbacks
{
    public static PlayerInfo info;

    GameObject my_player;   //�� �÷��̾�
    GameObject child;

    bool inlobby;   //�κ�

    private float pos_x;    //transform.poition
    private float pos_y;
    private float pos_z;

    private float rot;

    //�ƹ�Ÿ ����
    int front;
    int back;

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
    }

    // Start is called before the first frame update
    void Start()
    {
        inlobby = true;
        pos_x = 0f;
        pos_y = 2f;
        pos_z = 0f;
        rot = 0f;

        front = Random.Range(0, 5);
        back = Random.Range(0, 5);
    }

    /*
    // Update is called once per frame
    void Update()
    {

        if (PhotonNetwork.InLobby)
            inlobby = true;
        else if(PhotonNetwork.InRoom)
        { 
            if(PhotonNetwork.CurrentRoom.Name == "Square")
            {
                if(my_player != null)
                {
                    pos_x = my_player.transform.position.x;
                    pos_y = my_player.transform.position.y;
                    pos_z = my_player.transform.position.z;
                    rot = child.transform.rotation.eulerAngles.y;
                }                
            }
            else
            {
                inlobby = false;
                pos_x = 0f;
                pos_y = 2f;
                pos_z = 0f;
            }
        }
    }
    */

    public void FindPlayerObject()
    {
        my_player = GameObject.Find(PhotonNetwork.AuthValues.UserId).gameObject;
        child = my_player.transform.Find("avatar").gameObject;
        
        //pv = my_player.GetPhotonView();
    }

    public void UpdateSquarePos()
    {
        //�뱤�� >> �κ� >> �뱤��
        if(inlobby && PhotonNetwork.CurrentRoom.Name == "Square")
        {        
            Debug.Log("�뱤�� >> �κ� >> �뱤��");
            my_player.transform.position = new Vector3(pos_x, pos_y, pos_z);
            child.transform.rotation = Quaternion.Euler(0, rot, 0);
            Debug.LogFormat("{0}, {1}, {2}", pos_x, pos_y, pos_z);
            inlobby = false;
        }
    }

    public (int, int) Get()
    {
        return (front, back);
    }
}
