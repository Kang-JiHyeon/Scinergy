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
    SYA_PlayerSit playerSit;
    private void Start()
    {
        PlayerMove = GetComponent<PlayerMove>();
        //PlayerMove.enabled = false;
        CharacterController = GetComponent<CharacterController>();
        //CharacterController.enabled = false;
        PlayerRot = GetComponent<PlayerRot>();
        //PlayerRot.enabled=false;
        //캐릭터 모델링
        playerBody = transform.GetChild(0).gameObject;
        //캐릭터 닉네임
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
            //채팅을 치는 중이거나 전체화면일 때는 이동 및 카메라 회전이 불가능하다
            PlayerMove.enabled = !isTrigger;
            PlayerMove.currentScene = currentScene;
            PlayerRot.enabled = !isTrigger && !playerSit.isSit;
            //포톤뷰 이즈마인이고
            //씬이 로비거나(심포지엄) 별자리일 때
            PlayerRot.camPos.gameObject.SetActive(photonView.IsMine);
            playerBody.SetActive(true);
            playerName.SetActive(true);
            PlayerMove.cc.enabled = !playerSit.isSit;
        }

    }

    public bool isTrigger;

    void OnEnable()
    {
        //씬이 로드되면 씬로드 함수를 호출할 수 있도록 설정한다
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ChangeComponent(scene.name);
    }
    string currentScene;
    //컴포넌트 변경
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
            //채팅을 치는 중이거나 전체화면일 때는 이동 및 카메라 회전이 불가능하다
            if (PlayerMove != null)
            {
                PlayerMove.enabled = true;
                PlayerMove.currentScene = sceneName;
                //의자에 앉은 상태가 트루라면 CC를 꺼준다
                CharacterController.enabled = true;
            }
            if (PlayerRot != null)
            {
                PlayerRot.enabled = true;
                //포톤뷰 이즈마인이고
                //씬이 로비거나(심포지엄) 별자리일 때
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
