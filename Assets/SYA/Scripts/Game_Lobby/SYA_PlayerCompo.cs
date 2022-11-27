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

    PlayerMove PlayerMove;
    CharacterController CharacterController;
    PlayerRot PlayerRot;
    GameObject playerBody;
    GameObject playerName;

    private void Awake()
    {
        Instance = this;
        PlayerDestroy += SYA_Voice.Instance.DestroyOnGo;
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
    SYA_PlayerSit playerSit;
    private void Start()
    {
        PlayerMove = GetComponent<PlayerMove>();
        //PlayerMove.enabled = false;
        CharacterController = GetComponent<CharacterController>();
        //CharacterController.enabled = false;
        PlayerRot = GetComponent<PlayerRot>();
        //PlayerRot.enabled=false;
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
        else if (currentScene.Contains("Sympo") || currentScene.Contains("KYG"))
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
            print(sceneName);
            if (PlayerRot != null)
            {
                PlayerRot.camPos.gameObject.SetActive(false);
                PlayerRot.enabled = false;
            }
            playerBody.SetActive(false);
            playerName.SetActive(false);
            PlayerMove.enabled = false;
            CharacterController.enabled = false;
        }
        else if (sceneName.Contains("Sympo") || sceneName.Contains("KYG"))
        {
            //ä���� ġ�� ���̰ų� ��üȭ���� ���� �̵� �� ī�޶� ȸ���� �Ұ����ϴ�
            if (PlayerMove != null)
            {
                PlayerMove.enabled = true;
                PlayerMove.currentScene = sceneName;
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
                playerName.SetActive(true);
        }
    }
    public Action PlayerDestroy;
    private void OnDestroy()
    {
        PlayerDestroy();
    }
}
