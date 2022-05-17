using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

/*내 정보를 저장하는 클래스 
 */
public class PlayerInfo : MonoBehaviourPunCallbacks
{
    public static PlayerInfo info;

    static GameObject my_player;   //내 플레이어
    static GameObject child;

    public static bool inlobby;   //로비

    private static float pos_x;    //transform.poition
    private static float pos_y;
    private static float pos_z;

    private static float rot;

    //아바타 정보
    static int front;
    static int back;
    private static MeshFilter mesh_hair_front;
    private static SkinnedMeshRenderer mesh_hair_back;

    [Header("Avatar")]
    [SerializeField] private Mesh[] hair_front;
    [SerializeField] private Mesh[] hair_back;
    //[SerializeField] private Mesh[] hairs;
    //[SerializeField] private Mesh[] hairs;
    //[SerializeField] private Mesh[] hairs;

    /*싱글턴 사용*/
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

        front = Random.Range(0, hair_front.Length);
        back = Random.Range(0, hair_back.Length);
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

    public static void FindPlayerObject()
    {
        my_player = GameObject.Find(PhotonNetwork.AuthValues.UserId).gameObject;
        child = my_player.transform.Find("avatar").gameObject;
        mesh_hair_front = child.transform.Find("hair-front01").gameObject.GetComponent<MeshFilter>();
        mesh_hair_back = child.transform.Find("hair-back01").gameObject.GetComponent<SkinnedMeshRenderer>();
    }

    public static void UpdateSquarePos()
    {
        //대광장 >> 로비 >> 대광장
        if(inlobby && PhotonNetwork.CurrentRoom.Name == "Square")
        {
            Debug.Log("대광장 >> 로비 >> 대광장");
            my_player.transform.position = new Vector3(pos_x, pos_y, pos_z);
            child.transform.rotation = Quaternion.Euler(0, rot, 0);
            Debug.LogFormat("{0}, {1}, {2}", pos_x, pos_y, pos_z);
            inlobby = false;
        }
    }

    public void Avatar()
    {
        mesh_hair_front.mesh = hair_front[front];
        mesh_hair_back.sharedMesh = hair_back[back];
    }
}
