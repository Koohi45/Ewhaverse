using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Voice.Unity;
using Photon.Voice.PUN;

public class OverThePlayer : MonoBehaviour
{
    private Canvas canvas;
    public Image recorderSprite; // ���Ҷ� �� ���� �ߴ� �̹���
    public Image speakerSprite; // ������ �鸱�� ���� ���� �ߴ� �̹���
    public PhotonVoiceView photonVoiceView;

    // Start is called before the first frame update
    void Awake()
    {
        this.canvas = this.GetComponent<Canvas>();        
    }
    void Start()
	{
        this.canvas.worldCamera = Camera.main;
    }
    private void Update()
    {
        //this.recorderSprite.enabled = this.photonVoiceView.IsRecording; // ���� ���ϴ� ���϶� ������ ����
        this.speakerSprite.enabled = this.photonVoiceView.IsSpeaking; // �������μ� ���ϴ� ���϶� ������ ����
    }
    private void LateUpdate()
    {
        //if (this.canvas.worldCamera == null) { this.canvas.worldCamera = Camera.main; return; } 
        // Ÿ���� �ü����� ������ �ش� ī�޶��� ������ �����ϰ� �� ���� �������� �������� ���̰Բ� transform��Ŵ
        this.transform.rotation = Quaternion.Euler(this.canvas.worldCamera.transform.eulerAngles.x, this.canvas.worldCamera.transform.eulerAngles.y, 0f); //canvas.worldCamera.transform.rotation;

    }
}
