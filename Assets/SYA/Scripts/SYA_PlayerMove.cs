using Photon.Pun;
using Photon.Voice.PUN;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SYA_PlayerMove : MonoBehaviourPun
{
    public Button jumpButton;

    public float speed = 10;
    public float jumpPower = 3;
    public float jumpCount = 0;
    public float yVelocity;
    public float gravity = -9.81f;
    public CharacterController cc;
    public Text nickName;

    Vector3 dir;

    private void Awake()
    {
        SYA_SymposiumManager.Instance.PlayerNameAuthority(
    photonView.Owner.NickName,
    photonView,
    GetComponentInChildren<AudioSource>(),
    gameObject);
        GetComponentInChildren<AudioSource>().enabled = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (!photonView.IsMine) return;
        cc = GetComponent<CharacterController>();
        //nickName.text = PhotonNetwork.NickName ;
    }


    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine) return;

        float h = SYA_InputManager.GetAxis("Horizontal");
            float v = SYA_InputManager.GetAxis("Vertical");

            dir = Vector3.forward * v + Vector3.right * h;

            if (cc.isGrounded)
            {
                yVelocity = 0;
                jumpCount = 0;
            }
            if (SYA_InputManager.GetButtonDown("Jump"))
            {
                GetJump();
            }
        
        dir = Camera.main.transform.TransformDirection(dir);
        dir.Normalize();

        //jumpButton.onClick.AddListener(GetJump);
        yVelocity += gravity * Time.deltaTime;
        dir.y = yVelocity;
        cc.Move(dir * speed * Time.deltaTime);
    }

    public void GetJump()
    {
        if(jumpCount == 0)
        jumpCount++;
        yVelocity = jumpPower;
    }
}

