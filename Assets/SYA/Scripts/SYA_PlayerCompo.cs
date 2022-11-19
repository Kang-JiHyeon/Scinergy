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
