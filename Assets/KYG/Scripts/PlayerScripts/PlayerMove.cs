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
        SYA_SymposiumManager.Instance.PlayerAuthority(PhotonNetwork.NickName,master);

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
        if (jumpCount == 0)
            jumpCount++;
        yVelocity = jumpPower;
    }
}
