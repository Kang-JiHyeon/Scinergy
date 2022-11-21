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
    public string downStr = "X�� ���� �ɱ�";
    public string upStr = "X�� ���� �Ͼ��";

    private void Update()
    {
        if (!photonView.IsMine) return;
        //Ʈ���� ���� �ȿ� ����
        if (ontriger)
        {
            switch (sitState)
            {
                //�ɱ� ������ ��
                case State.Down:
                    updownText.text = downStr;
                    //x�� ������
                    if (Input.GetKeyDown(KeyCode.X))
                    {
                        //�÷��̾��� ĳ���� ��Ʈ���� ������
                        GetComponent<CharacterController>().enabled = false;
                        //�ɴ� �ִϸ��̼��� ����ȴ�
                        photonView.RPC("RPCSit", RpcTarget.All, true);
                        //��ġ�� �������ش�
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
                print("X�� ���� �����ÿ�");
                //Ʈ���Ű� �߻��ϸ� ��X ���� ��ȣ�ۿ롯 �϶�� ���� ��
                //X�� ������
                SitUpDown(other.gameObject, 0, true);
            }
            else
            {
                print("X�� ���� �Ͼ�ÿ�");
                SitUpDown(other.gameObject, 6, false);
            }*/
        }
        if (anim_false)
        {
            currentTime += Time.deltaTime;
            if (currentTime >= 3)
            {
                currentTime = 0;
                //�� ����
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

        //�� ����
        playerMove.speed = speed;
        playerMove.jumpPower = jump;
        //�ش� ��ġ�� �÷��̾ �̵�
        /*if (sit_)*/
            
        //targameObject.transform.rotation = transform.rotation;
        print("�ɾҽ��ϴ�");

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
