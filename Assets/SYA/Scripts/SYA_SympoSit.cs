using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SYA_SympoSit : MonoBehaviourPun
{
    bool sit;
    public Transform TV;

    enum State
    {
        up,
        Down
    }
    State sitState = State.Down;
    public float currentTime = 0;
    bool anim_false;

    private void Update()
    {
        if (!SYA_SymposiumManager.Instance.player[PhotonNetwork.NickName].IsMine) return;
        if (ontriger)
        {
            switch (sitState)
            {
                case State.Down:
                    print("X를 눌러 앉으시오");
                    if (Input.GetKeyDown(KeyCode.X))
                    {
                        targetGameobject.GetComponent<PlayerMove>().cc.enabled = false;
                        SitUpDown(targetGameobject, 0, true, 0);
                        targetGameobject.GetPhotonView().RPC("RPCSit", RpcTarget.All, true);
                        //targetGameobject.GetComponent<PlayerMove>().Sit(true);
                    }
                    break;
                case State.up:
                    print("X를 눌러 일어나시오");
                    targetGameobject.transform.eulerAngles = transform.eulerAngles;
                    if (Input.GetKeyDown(KeyCode.X))
                    {
                        //targetGameobject.GetComponent<PlayerMove>().Sit(false);
                        targetGameobject.GetPhotonView().RPC("RPCSit", RpcTarget.All, false);
                        Vector3 target = targetGameobject.transform.position;
                        //iTween.MoveTo(targetGameobject, target + new Vector3(0, 0.2f, -0.5f), 2);

                        anim_false = true;
                        targetGameobject.GetComponent<PlayerMove>().cc.enabled = true;

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
            }
        }
    }

    GameObject targetGameobject;
    bool ontriger;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name.Contains("Player"))
        {
            if (!other.GetComponent<PhotonView>().IsMine) return;
            targetGameobject = other.gameObject;
            ontriger = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name.Contains("Player"))
        {
            if (!other.GetComponent<PhotonView>().IsMine) return;
            ontriger = false;
        }
    }

    void SitUpDown(GameObject targameObject, int speed, bool sit_, float jump)
    {
        PlayerMove playerMove = targetGameobject.GetComponent<PlayerMove>();
        //playerMove.cc.enabled = !sit_;

        //후 고정
        playerMove.speed = speed;
        playerMove.jumpPower = jump;
        //해당 위치에 플레이어가 이동
        if (sit_)
            targameObject.transform.position = transform.position;
        targameObject.transform.rotation = transform.rotation;
        print("앉았습니다");

        if (sit_)
            sitState = State.up;
        else
            sitState = State.Down;
        anim_false = false;



    }
}
