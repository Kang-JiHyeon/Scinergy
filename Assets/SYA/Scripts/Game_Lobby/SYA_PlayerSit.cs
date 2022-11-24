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
    public string downStr = "X�� ���� �ɱ�";
    public string upStr = "X�� ���� �Ͼ��";

    //�ɾ��ִ����� ���� ����
    public bool isSit = false;

    PlayerMove playerMove;
    PlayerRot playerRot;
    //������ �ӵ�
    float speed;
    //������ ����
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
        //Ʈ���� ���� �ȿ� ����
        if (ontriger)
        {
            switch (sitState)
            {
                //�Ͼ ���� �� ��
                case State.Up:
                    //Ű�� ������ �� ���� �� �ִٴ� �ȳ������� ����Ѵ�
                    updownText.text = downStr;
                    //x�� ������
                    if (Input.GetKeyDown(KeyCode.X))
                    {
                        //�÷��̾��� ĳ���� ��Ʈ���� ������
                        //playerMove.cc.enabled = false;
                        //playerRot.enabled = false;
                        //�ɾ� �ִ� ���¸� Ʈ��� �ٲ��ش�
                        isSit = true;

                        //�ɴ� �ִϸ��̼��� ����ȴ�
                        //GetComponent<PlayerMove>().Sit(true);
                        photonView.RPC("RPCSit", RpcTarget.All, true);

                        //�÷��̾ �ɴ� ������ ��ġ�� �������ش�
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

                        //�ִϸ��̼��� ����ǰ� ������ �˸���
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
        //�ִϸ��̼��� ���� ���̴�
        if (anim_false)
        {
            //2�ʰ� �Ǹ�
            currentTime += Time.deltaTime;
            if (currentTime >= 2)
            {
                currentTime = 0;
                //�ִϸ��̼��� ���� ���°�,
                //���� ���·� �������ش�
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
        //�÷��̾��� �ӵ��� ���������� �������ش�
        playerMove.speed = speed;
        playerMove.jumpPower = jump;

        //���¿� ���� �ݴ�� ���¸� �ٲ��ش�
        if (sitState == State.Up)
            sitState = State.Down;
        else
            sitState = State.Up;

        //�ִϸ��̼��� ������ �������� �˷��ش�
        anim_false = false;
    }

    //����ڰ� �ɴ� �ִϸ��̼��� �����Ѵٰ� �ٸ� ����ڿ��� �˷��ش�
    [PunRPC]
    void RPCSit(bool sit_)
    {
        playerMove.anim.SetBool("Sit", sit_);
    }
}
