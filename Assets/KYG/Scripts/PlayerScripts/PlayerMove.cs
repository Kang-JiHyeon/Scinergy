using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;
public class PlayerMove : MonoBehaviourPun
{
    public float speed = 10;
    public float jumpPower = 3;
    public float jumpCount = 0;
    public float yVelocity;
    public float gravity = -9.81f;
    public CharacterController cc;
    public TextMeshProUGUI nickName;
    public Animator anim;

    public Action<bool> FullScreen;

    Vector3 dir;
    ////도착위치
    //Vector3 receivePos;
    ////회전되어야 하는 값
    //Quaternion receiveRot;
    ////보간속력
    //public float lerpSpeed = 100;
    //public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    //{
    //    //데이터 보내기
    //    if(stream.IsWriting)//isMine == true;
    //    {
    //        stream.SendNext(transform.position);
    //        stream.SendNext(transform.rotation);
    //    }
    //    else if(stream.IsReading)// isMine = false;
    //    {
    //        receivePos = (Vector3)stream.ReceiveNext();
    //        receiveRot = (Quaternion)stream.ReceiveNext();
    //    }
    //    //데이터 받기

    //}

    private void Awake()
    {
        SYA_SymposiumManager.Instance.PlayerNameAuthority(
    photonView.Owner.NickName,
    photonView,
    GetComponentInChildren<AudioSource>(),
    gameObject);

        bool master;
        if (!SYA_SymposiumManager.Instance.playerAuthority.ContainsKey(PhotonNetwork.NickName))
        {
            master = PhotonNetwork.MasterClient.UserId == SYA_SymposiumManager.Instance.player[PhotonNetwork.NickName].Owner.UserId;
        }
        else
        {
            master = SYA_SymposiumManager.Instance.playerAuthority[PhotonNetwork.NickName] == "Owner";
        }
        SYA_SymposiumManager.Instance.PlayerAuthority(PhotonNetwork.NickName, master);
        //anim = GetComponentInChildren<Animator>();
        GetComponentInChildren<AudioSource>().enabled = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
        nickName.text = photonView.Owner.NickName;
    }

    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine) return;
        //전체화면모드가 되면 이동막기
        if (!fullScreenMode)
        {
            float h = SYA_InputManager.GetAxis("Horizontal");
            float v = SYA_InputManager.GetAxis("Vertical");
            photonView.RPC("RPCanimMove", RpcTarget.All, v, h);
            /*anim.SetFloat("Speed", v);
            anim.SetFloat("Direction", h);*/
            dir = Vector3.forward * v + Vector3.right * h;

            if (cc.isGrounded)
            {
                yVelocity = 0;
                jumpCount = 0;
                photonView.RPC("RPCanimJump", RpcTarget.All, false);
                //anim.SetBool("Jump", false);
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

        //TV 더블 클릭시 모드 실행
        if (Input.GetKeyDown(KeyCode.M))
        {
            //GetComponentInChildren<Camera>().enabled = false;

            //펄스는 트루 / 트루는 펄스
            fullScreenMode = !fullScreenMode;
            //TV의 카메라를 끄고 키는 액션 함수 실행
            FullScreen(fullScreenMode);

            //카메라 회전 막기
            GetComponent<PlayerRot>().enabled = !fullScreenMode;
        }
    }
    public bool fullScreenMode;

    public void GetJump()
    {
        if (jumpCount == 0)
        {
            jumpCount++;
            yVelocity = jumpPower;
            photonView.RPC("RPCanimJump", RpcTarget.All, true);
            //anim.SetBool("Jump", true);
        }
    }

    [PunRPC]
    void RPCanimMove(float speed, float direction)
    {
        anim.SetFloat("Speed", speed);
        anim.SetFloat("Direction", direction);
    }

    [PunRPC]
    void RPCanimJump(bool jump)
    {
        anim.SetBool("Jump", jump);
    }



    public void Sit(bool sit_)
    {
        anim.SetBool("Sit", sit_);
    }
}
