using RockVR.Video;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Windows.WebCam;

public class SYA_SympoUI : MonoBehaviour
{
    public GameObject window;
    public GameObject windowList;
    //move��ư On / Off
    public Text moveOnOffstr;
    public GameObject moveOnOff;

    public GameObject spaceButton;
    public GameObject userData;

    private void Awake()
    {
        transform.parent = GameObject.Find("Canvas_DontDestroy").transform.GetChild(0).transform;
    }

    public void UwcOnOff()
    {
        window.SetActive(!window.activeSelf);
        windowList.SetActive(!windowList.activeSelf);
        moveOnOff.SetActive(!moveOnOff.activeSelf);
    }

    public void UwcMoveOnOff()
    {
        window.GetComponent<SYA_SympoWindowsMoving>().enabled = !window.GetComponent<SYA_SympoWindowsMoving>().enabled;
        moveOnOffstr.text = $"MOVE : {window.GetComponent<SYA_SympoWindowsMoving>().enabled}";
        Camera.main.transform.parent.GetComponentInParent<SYA_PlayerRot>().enabled = true;
    }

    public void OnUserList()
    {
        userData.SetActive(!userData.activeSelf);
    }

    public void OnSpaceChange()
    {
        spaceButton.SetActive(!spaceButton.activeSelf);
    }

    public void SolarSystemChange()
    {
        SceneManager.LoadScene("KJH_RevolutionScene");
        print("�ֶ� �ý������� �̵�");
    }

    public void ConstellationChange()
    {
        //SceneManager.LoadScene("");
        print("���ڸ��� �̵�");
    }

    bool isRecording;
    public Text recordeOnOff;
    //��ư�� ������ ��,
    public void Recording()
    {
        //��ȭ���̶�� 
        if(isRecording)
        {
            //VideoCapture.
            //��ȭ�� ������
            VideoCaptureCtrl.instance.StopCapture();
            recordeOnOff.text = "Off";
            //�����Ѵ�
            isRecording = false;
        }
        //��ȭ���� �ƴ϶��
        else
        {
            //��ȭ�� �����Ѵ�
            VideoCaptureCtrl.instance.StartCapture();
            recordeOnOff.text = "On";
            isRecording = true;
        }

/*        //��ȭ ����
        VideoCaptureCtrl.instance.StartCapture();

        //��ȭ ����
        VideoCaptureCtrl.instance.StopCapture();

        //�Ͻ� ���� �� �̾� ����
        VideoCaptureCtrl.instance.ToggleCapture();

        //��� �غ�
        VideoPlayer.instance.SetRootFolder();
        //���
        VideoPlayer.instance.PlayVideo();

        VideoPlayer.instance.NextVideo();*/
    }

}
