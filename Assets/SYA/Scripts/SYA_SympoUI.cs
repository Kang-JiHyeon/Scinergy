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

    //�� �̵� ��ư
    public GameObject solButton;
    public GameObject conButton;
    public GameObject solSympoButton;
    public GameObject conSympoButton;

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
    }

    public void OnUserList()
    {
        userData.SetActive(!userData.activeSelf);
    }

    public void OnSpaceChange()
    {
        spaceButton.SetActive(!spaceButton.activeSelf);
        if(SceneManager.GetActiveScene().name== "SymposiumScene")
        {
            solButton.SetActive(true);
            conButton.SetActive(true);
            solSympoButton.SetActive(false);
            conSympoButton.SetActive(false);
        }
        else if(SceneManager.GetActiveScene().name == "KJH_RevolutionScene")
        {
            solButton.SetActive(false);
            conButton.SetActive(true);
            solSympoButton.SetActive(true);
            conSympoButton.SetActive(false);
        }
        else if (SceneManager.GetActiveScene().name == "KYG_Scene")
        {
            solButton.SetActive(true);
            conButton.SetActive(false);
            solSympoButton.SetActive(false);
            conSympoButton.SetActive(true);
        }
    }

    public void SolarSystemChange()
    {
        SceneManager.LoadScene("KJH_RevolutionScene");
        OnOffChsnge();
        print("�ֶ� �ý������� �̵�");
    }

    public void ConstellationChange()
    {
        SceneManager.LoadScene("KYG_Scene");
        OnOffChsnge();
        print("���ڸ��� �̵�");
    }

    public void SymposiumChsnge()
    {
        SceneManager.LoadScene("SymposiumScene");
        OnOffChsnge();
    }

    void OnOffChsnge()
    {
        spaceButton.SetActive(!spaceButton.activeSelf);
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
