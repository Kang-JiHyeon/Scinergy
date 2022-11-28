using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class SYA_PlayerCompo : MonoBehaviourPun
{
    public static SYA_PlayerCompo Instance;
    public Action ChangeScene;

    SYA_PlayerSit playerSit;
    PlayerMove PlayerMove;
    CharacterController CharacterController;
    PlayerRot PlayerRot;
    GameObject playerBody;
    GameObject playerName;

    private void Awake()
    {
        Instance = this;
        
    }
    /*if (Instance == null)
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
        //ĳ���� �𵨸�
        playerBody = transform.GetChild(0).gameObject;
        //ĳ���� �г���
        playerName = transform.GetChild(1).gameObject;
        playerSit = GetComponent<SYA_PlayerSit>();
    }

    private void Update()
    {
        currentScene = SceneManager.GetActiveScene().name;
        if (currentScene.Contains("KJH"))
        {
            PlayerRot.camPos.gameObject.SetActive(false);
            playerBody.SetActive(false);
            playerName.SetActive(false);
            PlayerMove.enabled = false;
            CharacterController.enabled = false;
            PlayerRot.enabled = false;
        }
        else if (!isKjh && (currentScene.Contains("Sympo") || currentScene.Contains("KYG")))
        {
            //ä���� ġ�� ���̰ų� ��üȭ���� ���� �̵� �� ī�޶� ȸ���� �Ұ����ϴ�
            PlayerMove.enabled = !isTrigger;
            PlayerMove.currentScene = currentScene;
            PlayerRot.enabled = !isTrigger && !playerSit.isSit;
            //����� ������̰�
            //���� �κ�ų�(��������) ���ڸ��� ��
            PlayerRot.camPos.gameObject.SetActive(photonView.IsMine);
            playerBody.SetActive(true);
            playerName.SetActive(true);
            if(PlayerMove.cc!=null)
            PlayerMove.cc.enabled = !playerSit.isSit;
        }

    }

    public bool isTrigger;

    void OnEnable()
    {
        //���� �ε�Ǹ� ���ε� �Լ��� ȣ���� �� �ֵ��� �����Ѵ�
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ChangeComponent(scene.name);
    }
    string currentScene;
    //������Ʈ ����
    void ChangeComponent(string sceneName)
    {
        if (sceneName == "KJH_OrbitScene" || sceneName == "KJH_EclipseScene") return;
        if (sceneName.Contains("KJH"))
        {
            if(photonView.IsMine)
                photonView.RPC("RPCGoToUni", RpcTarget.All);
        }
        else if (sceneName.Contains("Sympo") || sceneName.Contains("KYG"))
        {
            /*//ä���� ġ�� ���̰ų� ��üȭ���� ���� �̵� �� ī�޶� ȸ���� �Ұ����ϴ�
            if (PlayerMove != null)
            {
                PlayerMove.enabled = true;
                //���ڿ� ���� ���°� Ʈ���� CC�� ���ش�
                CharacterController.enabled = true;
            }
            if (PlayerRot != null)
            {
                PlayerRot.enabled = true;
                //����� ������̰�
                //���� �κ�ų�(��������) ���ڸ��� ��
                PlayerRot.camPos.gameObject.SetActive(true);
            }
            if (playerBody != null)
                playerBody.SetActive(true);
            if (playerName != null)
                playerName.SetActive(true);*/
            if (photonView.IsMine)
                photonView.RPC("RPCBackToSympo", RpcTarget.All, sceneName);
        }
    }
    public Action PlayerDestroy;
    private void OnDestroy()
    {
        PlayerDestroy();
    }

    [PunRPC]
    public void RPCGoToUni()
    {
        GoToUni();
        //playerName.SetActive(false);
    }

    bool isKjh;
    public void GoToUni()
    {
        GetComponent<PlayerRot>().enabled = false;
        GetComponent<PlayerMove>().cc.enabled = false;
        GetComponent<PlayerMove>().enabled = false;
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(false);
        isKjh = true;
    }

    [PunRPC]
    public void RPCBackToSympo(string sceneName)
    {
        BackToSympo(sceneName);
    }

    public void BackToSympo(string sceneName)
    {
        GetComponent<PlayerRot>().enabled = true;
        GetComponent<PlayerMove>().enabled = true;
        GetComponent<PlayerMove>().cc.enabled = true;
        GetComponent<PlayerMove>().currentScene = sceneName;
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(1).gameObject.SetActive(true);
        isKjh = false;
        print("RPC�Լ��� �� ȣ���");
    }
}
