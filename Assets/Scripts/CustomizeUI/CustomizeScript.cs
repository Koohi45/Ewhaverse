using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using System.Text;
using System.Web;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
[System.Serializable]
public class custominfo
{
    public custominfo(string _type, string _name, bool _isusing)
    {
        type = _type; name = _name; isusing = _isusing;
    }
    public string type, name;
    public bool isusing;
}
public class CustomizeScript : MonoBehaviour
{
    [SerializeField] string url;
    public List<custominfo> customlist, confirmlist;
    public String[] typename = { "Skin", "Eye", "Mouse", "Hair", "Top", "Bottom", "Shoes", "Accessory" };
    public GameObject tabtype, viewentity;
    IEnumerator coroutine1, coroutine2;
    public bool done;
    void Start()
    {
        //���� �� DB�� Ski1,Eye1,Mou1,Hai1,Top1,Bot1,Sho1,Acc1�� ����ȴ�
        //�α��� ���� custominfo�� ���� list�� CustomJson.txt�� �����صд�
        //CustomJson.txt�� �ҷ��ͼ� customlist�� �߰��Ѵ�
        coroutine1 = CustomCoroutine("startcustom");
        coroutine2 = CustomCoroutine("confirmcustom");
        StartCoroutine(coroutine1);
        //list�� �����Ͽ� ĳ���͸� �׸���(�ٸ� �ʿ����� txt->list->draw)
    }
    IEnumerator CustomCoroutine(string command)
    {
        done = false;
        WWWForm form = new WWWForm();
        form.AddField("command", command);
        form.AddField("id", File.ReadAllText(Application.persistentDataPath + "/Sync.txt"));
        form.AddField("password", "");
        form.AddField("email", "");
        form.AddField("item", File.ReadAllText(Application.persistentDataPath + "/CustomJson.txt"));
        UnityWebRequest www = UnityWebRequest.Post(url, form);
        yield return www.SendWebRequest();
        string result = www.downloadHandler.text;
        done = true;
        if (command.Contains("startcustom"))
        {
            StartProcess(result);
            StopCoroutine(coroutine1);
        }
        if (command.Contains("confirmcustom"))
        {
            StopCoroutine(coroutine2);
        }
    }
    public void StartProcess(string result)
    {
        //SkiN,~,AccN->customlist
        if (!done)
        {
            for (int i = 0; i < 60; i++) { }
        }
        string tempstring;
        for(int i = 0; i < typename.Length; i++)
        {
            for(int j = 1; j < 6; j++)
            {
                tempstring = typename[i].Substring(0, 3) + j.ToString();
                customlist.Add(new custominfo(typename[i], tempstring, false));
            }
        }
        string[] info = result.Split(new string[] { "," }, StringSplitOptions.None);
        int tempindex;
        for (int i = 0; i < info.Length; i++)
        {
            tempindex = customlist.FindIndex(x => x.name == info[i]);
            customlist[tempindex].isusing = true;
        }
    }
    public void ConfirmProcess()
    {
        //customlist->SkiN,~,AccN
        if (!done)
        {
            for (int i = 0; i < 60; i++) { }
        }
        confirmlist = customlist.FindAll(x => x.isusing == true);
        StringBuilder wdata = new StringBuilder();
        for (int i = 0; i < confirmlist.Count; i++)
        {
            wdata.Append(confirmlist[i].name);
            if (i < confirmlist.Count - 1)
            {
                wdata.Append(",");
            }
        }
        File.WriteAllText(Application.persistentDataPath + "/CustomJson.txt", wdata.ToString());
    }
    public void SelectClicked()
    {
        //ó������ Model�ǰ� ViewSkin�� ��������
        //Model����: Inspector���� Model���� ON, Dress���� OFF, List���� ViewSkin�� ON
        //Dress����: Inspector���� Model���� OFF, Dress���� ON, List���� ViewTop�� ON
        tabtype = EventSystem.current.currentSelectedGameObject;
        if (tabtype.name == "SelectModel")
        {
            transform.Find("Dress").gameObject.SetActive(false);
            transform.Find("Model").gameObject.SetActive(true);
            TypeClicked(transform.Find("Model").transform.Find("Skin").gameObject);
        }
        if (tabtype.name == "SelectDress")
        {
            transform.Find("Model").gameObject.SetActive(false);
            transform.Find("Dress").gameObject.SetActive(true);
            TypeClicked(transform.Find("Dress").transform.Find("Top").gameObject);
        }
    }
    public void TypeClicked(GameObject obj)
    {
        //Skin����: List���� ViewSkin�� ON
        //Eye����: List���� ViewEye�� ON
        //Mouse����: List���� ViewMouse�� ON
        //Hair����: List���� ViewHair�� ON
        //Top����: List���� ViewTop�� ON
        //Bottom����: List���� ViewBottom�� ON
        //Shoes����: List���� ViewShoes�� ON
        //Accessory����: List���� ViewAccessory�� ON
        tabtype = EventSystem.current.currentSelectedGameObject;
        if (obj.name == "Skin" || obj.name == "Top")
        {
            tabtype = obj;
        }
        string tmpstring;
        for (int i = 0; i < typename.Length; i++)
        {
            tmpstring = "View" + typename[i];
            transform.Find("List").transform.Find(tmpstring).gameObject.SetActive(false);
        }
        tmpstring = "View" + tabtype.name;
        transform.Find("List").transform.Find(tmpstring).gameObject.SetActive(true);
    }
    public void EntityClicked()
    {
        //ListView~�� ��ƼƼ�� Ŭ���Ǹ� ����Ʈ���� isusing�� �ٲ۴�
        viewentity = EventSystem.current.currentSelectedGameObject;
        int tmpindex1;
        tmpindex1 = customlist.FindIndex(x => x.name == viewentity.name);
        int tmpindex2;
        tmpindex2 = tmpindex1 / 5;
        for (int i = 0; i < 5; i++)
        {
            customlist[(tmpindex2 * 5) + i].isusing = false;
        }
        customlist[tmpindex1].isusing = true;
        //ĳ���� �̸����⸦ �ٽ� �׸���
    }
    public void ConfirmClicked()
    {
        //Ȯ�εǸ� customlist�� ������ CustomJson.txt�� �����Ѵ�
        //customlist�� ������ ������ DB�� �����Ѵ�
        ConfirmProcess();
        StartCoroutine(coroutine2);
        //���� ȭ������ �̵��Ѵ�
    }
    public void CancelClicked()
    {
        //��ҵǸ� ���� ȭ������ �̵��Ѵ�
    }
}