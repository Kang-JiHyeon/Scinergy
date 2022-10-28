using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SYA_UIManager : MonoBehaviour
{
    public static SYA_UIManager Instance;

    private void Awake()
    {
        //if (!photonView.IsMine) return;
        if (Instance == null)
        {
            //�ν��Ͻ��� ���� �ְ�
            Instance = this;
            //���� ���� ��ȯ�� �Ǿ �ı����� �ʰ� �ϰڴ�

            DontDestroyOnLoad(gameObject);
        }
        //�׷��� ������
        else
        {
            Destroy(gameObject);
        }
    }

    //ä��â �¿���
    public GameObject chat;
    public void ChatOnOff()
    {
        chat.SetActive(!chat.activeSelf);
    }

    //����ũ �¿���
    public GameObject micOn;
    public GameObject micOff;
    public void MicOnOff()
    {
        micOn.SetActive(!micOn.activeSelf);
        micOff.SetActive(!micOff.activeSelf);
        if (micOn.activeSelf == true)//Ʈ���϶�->����ũ On�� ��
        {
            // ����ũ Ű��
            print("����ũ ����");
        }
        else
        {
            //����ũ ����
            print("����ũ ����");
        }
    }

    //����â �¿���
    public GameObject option;
    public void OptionOnOff()
    {
        option.SetActive(!option.activeSelf);
    }

    //������ â �¿���
    public GameObject quit;
    public void QuitOnOff()
    {
        quit.SetActive(!quit.activeSelf);
    }

}
