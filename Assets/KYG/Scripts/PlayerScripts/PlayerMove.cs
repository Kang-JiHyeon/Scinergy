using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;
using UnityEngine.SceneManagement;

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

    SYA_PlayerSit playerSit;
    PlayerRot playerRot;

    public string currentScene;

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
        nickName.text = photonView.Owner.NickName;
        GetComponentInChildren<AudioSource>().enabled = false;
        playerSit = GetComponent<SYA_PlayerSit>();
        playerRot = GetComponent<PlayerRot>();

        if (photonView.IsMine)
        {
            SYA_SymposiumManager.Instance.PlayerNameAuthority(PhotonNetwork.NickName,
            photonView,
            GetComponentInChildren<AudioSource>());
            photonView.RPC("RPCPlayerNameAuthority", RpcTarget.All, PhotonNetwork.NickName);
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
        }
        //anim = GetComponentInChildren<Animator>();
    }

    [PunRPC]
    public void RPCPlayerNameAuthority(string name)
    {
        print("�Ҹ���>?");
        SYA_SymposiumManager.Instance.playerName.Add(name);
    }

    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine) return;
        /*if(SceneManager.GetActiveScene().name.Contains("Sympo"))
        {

        }*/
        //��üȭ���尡 �Ǹ� �̵�����
        if (!(fullScreenMode || playerSit.isSit))
        {
            if (SYA_ChatManager.Instance != null)
                if (SYA_ChatManager.Instance.inputFocused) return;

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

            //���� ������, ��ǲ�ʵ� ��Ŀ���� �ƴҶ�
            //���۹� ���̵� ����
            if (Input.GetKeyDown(KeyCode.Tab) && !SYA_ChatManager.Instance.inputFocused)
                SYA_UI.SYA_UIManager.Instance.OnGuid();
        }


        //TV ���� Ŭ���� ��� ����
        if (Input.GetMouseButtonDown(0))
        {
            if (!currentScene.Contains("Sympo")) return;
            //Ŭ���� ������ ray�� �� ��,
            if (fullScreenMode)
            {
                _cam = Tv.GetComponentInChildren<SYA_FullScreen>().camera_;
            }
            else
            {
                _cam = GetComponentInChildren<Camera>();
            }
            Ray ray = _cam.ScreenPointToRay(Input.mousePosition);
            //�ش� ������Ʈ�� �θ� �̸��� TV���
            RaycastHit raycastHit = new RaycastHit();
            Physics.Raycast(ray, out raycastHit);
            if (raycastHit.collider.gameObject.name == "TV")
            {
                Tv = raycastHit.collider.gameObject;
                //Ƚ���� ++���ش�
                buttonOn++;
            }
        }
        if (buttonOn >= 1)
        {
            //Ŭ�� �� �ð��� �帥��
            currentTime += Time.deltaTime;
            //���� �ð��� �Ǹ� ��ư�� ���� Ƚ���� �ð��� 0�� �ȴ�
            if (currentTime >= clickTime)
            {
                currentTime = 0;
                buttonOn = 0;
            }
            //GetComponentInChildren<Camera>().enabled = false;
            if (buttonOn >= 2)
            {
                currentTime = 0;
                buttonOn = 0;
                //�޽��� Ʈ�� / Ʈ��� �޽�
                fullScreenMode = !fullScreenMode;
                //TV�� ī�޶� ���� Ű�� �׼� �Լ� ����
                SYA_FullScreen.instance.FullScreen(fullScreenMode);

                //ī�޶� ȸ�� ����
                if (!playerSit.isSit)
                    playerRot.enabled = !fullScreenMode;
            }
        }
    }
    public bool fullScreenMode;
    //���콺 Ŭ���� �帣�� �ð�
    float currentTime = 0;
    //���� Ŭ�� ���� �ð�
    float clickTime = 0.5f;
    //Ŭ�� Ƚ��
    int buttonOn = 0;
    GameObject Tv;
    Camera _cam;

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
