using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class SYA_PlayerSit : MonoBehaviourPun
{
    public enum State
    {
        Up,
        Down
    }
    public State sitState = State.Up;
    public float currentTime = 0;
    public bool anim_false;

    public Text updownText;
    public string downStr = "X를 눌러 앉기";
    public string upStr = "X를 눌러 일어나기";

    //앉아있는지에 대한 상태
    public bool isSit = false;

    PlayerMove playerMove;
    PlayerRot playerRot;
    //원래의 속도
    float speed;
    //원래의 점프
    float jump;

    private void Start()
    {
        playerMove = GetComponent<PlayerMove>();
        playerRot = GetComponent<PlayerRot>();
        speed = playerMove.speed;
        jump = playerMove.jumpPower;
    }

    private void Update()
    {
        if (!photonView.IsMine) return;
        //트리거 영역 안에 들어가면
        if (ontriger)
        {
            switch (sitState)
            {
                //일어난 상태 일 때
                case State.Up:
                    //키를 눌렀을 때 앉을 수 있다는 안내문구를 출력한다
                    updownText.text = downStr;
                    //x를 누르면
                    if (Input.GetKeyDown(KeyCode.X))
                    {
                        //플레이어의 캐릭터 컨트롤이 꺼진다
                        //playerMove.cc.enabled = false;
                        //playerRot.enabled = false;
                        //앉아 있는 상태를 트루로 바꿔준다
                        isSit = true;

                        //앉는 애니메이션이 재생된다
                        //GetComponent<PlayerMove>().Sit(true);
                        photonView.RPC("RPCSit", RpcTarget.All, true);

                        //플레이어를 앉는 의자의 위치로 조정해준다
                        transform.position = targetGameobject.transform.position;
                        transform.eulerAngles = targetGameobject.transform.eulerAngles;

                        SitUpDown(0, 0);
                    }
                    break;
                case State.Down:
                    updownText.text = upStr;
                    transform.LookAt(targetGameobject.GetComponent<SYA_SympoSit>().TV);
                    if (Input.GetKeyDown(KeyCode.X))
                    {
                        print("out");
                        //GetComponent<PlayerMove>().Sit(false);
                        photonView.RPC("RPCSit", RpcTarget.All, false);
                        //GetComponentInChildren<Animator>().
                        //Vector3 target = targetGameobject.transform.position;
                        //iTween.MoveTo(targetGameobject, target + new Vector3(0, 0.2f, -0.5f), 2);

                        //애니메이션이 진행되고 있음을 알린다
                        anim_false = true;
                    }
                    break;
            }

            /*if (!sit)
            {
                print("X를 눌러 앉으시오");
                //트리거가 발생하면 ‘X 눌러 상호작용’ 하라는 문구 뜸
                //X를 누르면
                SitUpDown(other.gameObject, 0, true);
            }
            else
            {
                print("X를 눌러 일어나시오");
                SitUpDown(other.gameObject, 6, false);
            }*/
        }
        //애니메이션이 진행 중이다
        if (anim_false)
        {
            //2초가 되면
            currentTime += Time.deltaTime;
            if (currentTime >= 2)
            {
                currentTime = 0;
                //애니메이션이 끝난 상태고,
                //원래 상태로 조정해준다
                //playerMove.cc.enabled = true;
                //playerRot.enabled = true;
                isSit = false;
                SitUpDown(speed, jump);
            }
        }
    }

    public GameObject targetGameobject;
    public bool ontriger;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent != null)
            if (other.transform.parent.name.Contains("Sit"))
            {
                if (!photonView.IsMine) return;
                updownText.transform.parent.gameObject.SetActive(true);
                targetGameobject = other.gameObject;
                ontriger = true;
            }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.parent != null)
            if (other.transform.parent.name.Contains("Sit"))
            {
                if (!photonView.IsMine) return;
                updownText.transform.parent.gameObject.SetActive(false);
                anim_false = false;
                targetGameobject = null;
                ontriger = false;
            }
    }

    void SitUpDown(float speed, float jump)
    {
        //플레이어의 속도와 점프강도를 조정해준다
        playerMove.speed = speed;
        playerMove.jumpPower = jump;

        //상태에 따라 반대로 상태를 바꿔준다
        if (sitState == State.Up)
            sitState = State.Down;
        else
            sitState = State.Up;

        //애니메이션의 진행이 끝났음을 알려준다
        anim_false = false;
    }

    //사용자가 앉는 애니메이션을 진행한다고 다른 사용자에게 알려준다
    [PunRPC]
    void RPCSit(bool sit_)
    {
        playerMove.anim.SetBool("Sit", sit_);
    }
}
