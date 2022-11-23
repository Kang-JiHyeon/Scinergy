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
    GameObject playerBody;
    GameObject playerName;

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
        PlayerMove.enabled = false;
        CharacterController = GetComponent<CharacterController>();
        CharacterController.enabled = false;
        PlayerRot = GetComponent<PlayerRot>();
        PlayerRot.enabled=false;
        //ĳ���� �𵨸�
        playerBody = transform.GetChild(0).gameObject;
        //ĳ���� �г���
        playerName = transform.GetChild(1).gameObject;
    }

/*    private void Update()
    {
        
        
    }*/

    void OnEnable()
    {
        //���� �ε�Ǹ� ���ε� �Լ��� ȣ���� �� �ֵ��� �����Ѵ�
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ChangeComponent(scene.name);
    }

    //������Ʈ ����
    void ChangeComponent(string sceneName)
    {
        if (sceneName.Contains("KJH"))
        {
            PlayerRot.camPos.gameObject.SetActive(false);
            playerBody.SetActive(false);
            playerName.SetActive(false);
            PlayerMove.enabled = false;
            CharacterController.enabled = false;
            PlayerRot.enabled = false;
        }
        else if(sceneName.Contains("Sympo"))
        {
            //ä���� ġ�� ���̰ų� ��üȭ���� ���� �̵� �� ī�޶� ȸ���� �Ұ����ϴ�
            PlayerMove.enabled = !(PlayerMove.fullScreenMode ||
                SYA_ChatManager.Instance.inputFocused);
            PlayerRot.enabled = !(PlayerMove.fullScreenMode ||
                SYA_ChatManager.Instance.inputFocused );
            //����� ������̰�
            //���� �κ�ų�(��������) ���ڸ��� ��
            PlayerRot.camPos.gameObject.SetActive(photonView.IsMine);
            playerBody.SetActive(true);
            playerName.SetActive(true);
            //���ڿ� ���� ���°� Ʈ���� CC�� ���ش�
            CharacterController.enabled = true;
        }
        else if(sceneName.Contains("KYG"))
        {
            //ä���� ġ�� ���̰ų� ��üȭ���� ���� �̵� �� ī�޶� ȸ���� �Ұ����ϴ�
            PlayerMove.enabled = !SYA_ChatManager.Instance.inputFocused;
            PlayerRot.enabled = !SYA_ChatManager.Instance.inputFocused;
            //����� ������̰�
            //���� �κ�ų�(��������) ���ڸ��� ��
            PlayerRot.camPos.gameObject.SetActive(true);
            playerBody.SetActive(true);
            playerName.SetActive(true);
            //���ڿ� ���� ���°� Ʈ���� CC�� ���ش�
            CharacterController.enabled = true;
            print(sceneName);
        }
    }
}
