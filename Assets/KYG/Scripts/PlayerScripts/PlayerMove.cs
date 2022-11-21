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
    ////������ġ
    //Vector3 receivePos;
    ////ȸ���Ǿ�� �ϴ� ��
    //Quaternion receiveRot;
    ////�����ӷ�
    //public float lerpSpeed = 100;
    //public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    //{
    //    //������ ������
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
    //    //������ �ޱ�

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
        //��üȭ���尡 �Ǹ� �̵�����
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

        //TV ���� Ŭ���� ��� ����
        if (Input.GetKeyDown(KeyCode.M))
        {
            //GetComponentInChildren<Camera>().enabled = false;

            //�޽��� Ʈ�� / Ʈ��� �޽�
            fullScreenMode = !fullScreenMode;
            //TV�� ī�޶� ���� Ű�� �׼� �Լ� ����
            FullScreen(fullScreenMode);

            //ī�޶� ȸ�� ����
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
