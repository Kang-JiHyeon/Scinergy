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
            //인스턴스에 나를 넣고
            Instance = this;
            //나를 씬이 전환이 되어도 파괴되지 않게 하겠다

            DontDestroyOnLoad(gameObject);
        }
        //그렇지 않으면
        else
        {
            Destroy(gameObject);
        }
    }

    //채팅창 온오프
    public GameObject chat;
    public void ChatOnOff()
    {
        chat.SetActive(!chat.activeSelf);
    }

    //마이크 온오프
    public GameObject micOn;
    public GameObject micOff;
    public void MicOnOff()
    {
        micOn.SetActive(!micOn.activeSelf);
        micOff.SetActive(!micOff.activeSelf);
        if (micOn.activeSelf == true)//트루일때->마이크 On일 때
        {
            // 마이크 키기
            print("마이크 켜짐");
        }
        else
        {
            //마이크 끄기
            print("마이크 꺼짐");
        }
    }

    //설정창 온오프
    public GameObject option;
    public void OptionOnOff()
    {
        option.SetActive(!option.activeSelf);
    }

    //나가기 창 온오프
    public GameObject quit;
    public void QuitOnOff()
    {
        quit.SetActive(!quit.activeSelf);
    }

}
