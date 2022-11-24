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
        print("불리니>?");
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
        //전체화면모드가 되면 이동막기
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

            //탭을 눌렀고, 인풋필드 포커싱이 아닐때
            //조작법 가이드 띄우기
            if (Input.GetKeyDown(KeyCode.Tab) && !SYA_ChatManager.Instance.inputFocused)
                SYA_UI.SYA_UIManager.Instance.OnGuid();
        }


        //TV 더블 클릭시 모드 실행
        if (Input.GetMouseButtonDown(0))
        {
            if (!currentScene.Contains("Sympo")) return;
            //클릭한 곳에서 ray를 쏠 때,
            if (fullScreenMode)
            {
                _cam = Tv.GetComponentInChildren<SYA_FullScreen>().camera_;
            }
            else
            {
                _cam = GetComponentInChildren<Camera>();
            }
            Ray ray = _cam.ScreenPointToRay(Input.mousePosition);
            //해당 오브젝트의 부모 이름이 TV라면
            RaycastHit raycastHit = new RaycastHit();
            Physics.Raycast(ray, out raycastHit);
            if (raycastHit.collider.gameObject.name == "TV")
            {
                Tv = raycastHit.collider.gameObject;
                //횟수를 ++해준다
                buttonOn++;
            }
        }
        if (buttonOn >= 1)
        {
            //클릭 후 시간이 흐른다
            currentTime += Time.deltaTime;
            //제한 시간이 되면 버튼을 누른 횟수와 시간이 0이 된다
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
                //펄스는 트루 / 트루는 펄스
                fullScreenMode = !fullScreenMode;
                //TV의 카메라를 끄고 키는 액션 함수 실행
                SYA_FullScreen.instance.FullScreen(fullScreenMode);

                //카메라 회전 막기
                if (!playerSit.isSit)
                    playerRot.enabled = !fullScreenMode;
            }
        }
    }
    public bool fullScreenMode;
    //마우스 클릭시 흐르는 시간
    float currentTime = 0;
    //더블 클릭 제한 시간
    float clickTime = 0.5f;
    //클릭 횟수
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
