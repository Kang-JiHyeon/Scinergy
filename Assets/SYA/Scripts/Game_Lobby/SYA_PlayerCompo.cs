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
        else if (!isKjh && (currentScene.Contains("Sympo") || currentScene.Contains("KYG")))
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
            if(PlayerMove.cc!=null)
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
            if(photonView.IsMine)
                photonView.RPC("RPCGoToUni", RpcTarget.All);
        }
        else if (sceneName.Contains("Sympo") || sceneName.Contains("KYG"))
        {
            /*//채팅을 치는 중이거나 전체화면일 때는 이동 및 카메라 회전이 불가능하다
            if (PlayerMove != null)
            {
                PlayerMove.enabled = true;
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
        print("RPC함수는 잘 호출됨");
    }
}
