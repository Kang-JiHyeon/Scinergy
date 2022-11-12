using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SYA_PlayerCompo : MonoBehaviour
{
   SYA_PlayerMove SYA_PlayerMove;
   CharacterController CharacterController;
   PlayerRot PlayerRot;

    private void Start()
    {
        SYA_PlayerMove = GetComponent<SYA_PlayerMove>();
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
