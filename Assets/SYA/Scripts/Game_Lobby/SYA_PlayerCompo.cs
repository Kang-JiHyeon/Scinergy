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
            //�ν��Ͻ��� ���� �ְ�
            Instance = this;
            //���� ���� ��ȯ�� �Ǿ �ı����� �ʰ� �ϰڴ�

            DontDestroyOnLoad(gameObject);
        }
        //�׷��� ������
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
            //ä���� ġ�� ���̰ų� ��üȭ���� ���� �̵� �� ī�޶� ȸ���� �Ұ����ϴ�
            PlayerMove.enabled = !(PlayerMove.fullScreenMode ||
                SYA_ChatManager.Instance.inputFocused);
            PlayerRot.enabled = !(PlayerMove.fullScreenMode ||
                SYA_ChatManager.Instance.inputFocused);
        }
    }
}
