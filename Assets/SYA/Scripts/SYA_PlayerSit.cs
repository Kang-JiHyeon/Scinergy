using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class SYA_PlayerSit : MonoBehaviourPun
{
    bool sit;
    public Transform TV;

    public enum State
    {
        up,
        Down
    }
    public State sitState = State.Down;
    public float currentTime = 0;
    public bool anim_false;

    public Text updownText;
    public string downStr = "X를 눌러 앉기";
    public string upStr = "X를 눌러 일어나기";

    private void Update()
    {
        if (!photonView.IsMine) return;
        //트리거 영역 안에 들어가면
        if (ontriger)
        {
            switch (sitState)
            {
                //앉기 상태일 때
                case State.Down:
                    updownText.text = downStr;
                    //x를 누르면
                    if (Input.GetKeyDown(KeyCode.X))
                    {
                        //플레이어의 캐릭터 컨트롤이 꺼진다
                        GetComponent<CharacterController>().enabled = false;
                        //앉는 애니메이션이 재생된다
                        photonView.RPC("RPCSit", RpcTarget.All, true);
                        //위치를 조정해준다
                        transform.position = targetGameobject.transform.position;
                        SitUpDown(targetGameobject, 0, true, 0);
                        print(transform.position.ToString());
                        //targetGameobject.GetComponent<PlayerMove>().Sit(true);
                    }
                    break;
                case State.up:
                    updownText.text = upStr;
                    transform.eulerAngles = targetGameobject.transform.eulerAngles;
                    if (Input.GetKeyDown(KeyCode.X))
                    {
                        print("out");
                        //targetGameobject.GetComponent<PlayerMove>().Sit(false);
                        photonView.RPC("RPCSit", RpcTarget.All, false);
                        //GetComponentInChildren<Animator>().
                        //Vector3 target = targetGameobject.transform.position;
                        //iTween.MoveTo(targetGameobject, target + new Vector3(0, 0.2f, -0.5f), 2);

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
        if (anim_false)
        {
            currentTime += Time.deltaTime;
            if (currentTime >= 3)
            {
                currentTime = 0;
                //후 고정
                SitUpDown(targetGameobject, 6, false, 2.5f);
                GetComponent<PlayerMove>().cc.enabled = true;
            }
        }
    }

    public GameObject targetGameobject;
    public bool ontriger;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.transform.parent.name.Contains("Sit"))
        {
            if (!photonView.IsMine) return;
            updownText.enabled = true;
            targetGameobject = other.gameObject;
            ontriger = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.transform.parent.name.Contains("Sit"))
        {
            if (!photonView.IsMine) return;
            updownText.enabled = false;
            anim_false = false;
            targetGameobject = null;
            ontriger = false;
        }
    }

    void SitUpDown(GameObject targameObject, int speed, bool sit_, float jump)
    {
        PlayerMove playerMove = GetComponent<PlayerMove>();
        //playerMove.cc.enabled = !sit_;

        //후 고정
        playerMove.speed = speed;
        playerMove.jumpPower = jump;
        //해당 위치에 플레이어가 이동
        /*if (sit_)*/
            
        //targameObject.transform.rotation = transform.rotation;
        print("앉았습니다");

        if (sit_)
            sitState = State.up;
        else
        {
            sitState = State.Down;
        }
        anim_false = false;


    }

    [PunRPC]
    void RPCSit(bool sit_)
    {
        GetComponentInChildren<Animator>().SetBool("Sit", sit_);
    }
}
