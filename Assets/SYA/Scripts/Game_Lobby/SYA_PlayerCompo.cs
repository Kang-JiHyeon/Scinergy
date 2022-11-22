using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class SYA_PlayerCompo : MonoBehaviourPun
{
    public static SYA_PlayerCompo Instance;
    PlayerMove PlayerMove;
    CharacterController CharacterController;
    PlayerRot PlayerRot;
    SYA_DontDestroy SYA_DontDestroy;

    /*private void Awake()
    {
        *//*if (Instance == null)
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
        }*//*
    }*/

    private void Start()
    {
        PlayerMove = GetComponent<PlayerMove>();
        CharacterController = GetComponent<CharacterController>();
        PlayerRot = GetComponent<PlayerRot>();
    }
    private void Update()
    {
        if (SceneManager.GetActiveScene().name.Contains("KJH"))
        {
            PlayerRot.camPos.gameObject.SetActive(false);
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(false);
            PlayerMove.enabled = false;
            CharacterController.enabled = false;
            PlayerRot.enabled = false;
        }
        else
        {
            PlayerRot.camPos.gameObject.SetActive(true);
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(1).gameObject.SetActive(true);
            CharacterController.enabled = true;
            //채팅을 치는 중이거나 전체화면일 때는 이동 및 카메라 회전이 불가능하다
            PlayerMove.enabled = !(PlayerMove.fullScreenMode ||
                SYA_ChatManager.Instance.inputFocused);
            PlayerRot.enabled = !(PlayerMove.fullScreenMode ||
                SYA_ChatManager.Instance.inputFocused);
        }
    }
}
