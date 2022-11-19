using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class SYA_PlayerCompo : MonoBehaviourPun
{
    public static SYA_PlayerCompo Instance;
   PlayerMove SYA_PlayerMove;
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
        SYA_PlayerMove = GetComponent<PlayerMove>();
        CharacterController = GetComponent<CharacterController>();
        PlayerRot=GetComponent<PlayerRot>();
    }
    private void Update()
    {
        bool kjh = SceneManager.GetActiveScene().name.Contains("KJH");
        transform.GetChild(0).gameObject.SetActive(!kjh);
        transform.GetChild(1).gameObject.SetActive(!kjh);
        SYA_PlayerMove.enabled = !kjh;
        CharacterController.enabled= !kjh;
        PlayerRot.enabled= !kjh;
    }
}
