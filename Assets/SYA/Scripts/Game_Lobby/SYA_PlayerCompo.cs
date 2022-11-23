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
        PlayerMove.enabled = false;
        CharacterController = GetComponent<CharacterController>();
        CharacterController.enabled = false;
        PlayerRot = GetComponent<PlayerRot>();
        PlayerRot.enabled=false;
        //캐릭터 모델링
        playerBody = transform.GetChild(0).gameObject;
        //캐릭터 닉네임
        playerName = transform.GetChild(1).gameObject;
    }

/*    private void Update()
    {
        
        
    }*/

    void OnEnable()
    {
        //씬이 로드되면 씬로드 함수를 호출할 수 있도록 설정한다
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ChangeComponent(scene.name);
    }

    //컴포넌트 변경
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
            //채팅을 치는 중이거나 전체화면일 때는 이동 및 카메라 회전이 불가능하다
            PlayerMove.enabled = !(PlayerMove.fullScreenMode ||
                SYA_ChatManager.Instance.inputFocused);
            PlayerRot.enabled = !(PlayerMove.fullScreenMode ||
                SYA_ChatManager.Instance.inputFocused );
            //포톤뷰 이즈마인이고
            //씬이 로비거나(심포지엄) 별자리일 때
            PlayerRot.camPos.gameObject.SetActive(photonView.IsMine);
            playerBody.SetActive(true);
            playerName.SetActive(true);
            //의자에 앉은 상태가 트루라면 CC를 꺼준다
            CharacterController.enabled = true;
        }
        else if(sceneName.Contains("KYG"))
        {
            //채팅을 치는 중이거나 전체화면일 때는 이동 및 카메라 회전이 불가능하다
            PlayerMove.enabled = !SYA_ChatManager.Instance.inputFocused;
            PlayerRot.enabled = !SYA_ChatManager.Instance.inputFocused;
            //포톤뷰 이즈마인이고
            //씬이 로비거나(심포지엄) 별자리일 때
            PlayerRot.camPos.gameObject.SetActive(true);
            playerBody.SetActive(true);
            playerName.SetActive(true);
            //의자에 앉은 상태가 트루라면 CC를 꺼준다
            CharacterController.enabled = true;
            print(sceneName);
        }
    }
}
