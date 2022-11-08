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

    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
        nickName.text = photonView.Owner.NickName;
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            Vector3 dir = Vector3.forward * v + Vector3.right * h;
            dir = Camera.main.transform.TransformDirection(dir);
            dir.Normalize();
            if(cc.isGrounded)
            {
                yVelocity = 0;
                jumpCount = 0;
            }
            if (Input.GetButtonDown("Jump") && jumpCount == 0)
            {
                jumpCount++;
                yVelocity = jumpPower;
            }
            yVelocity += gravity * Time.deltaTime;
            dir.y = yVelocity;
            cc.Move(dir * speed * Time.deltaTime);
        }
        else
        {
            ////Lerp를 이용해서 목적지, 목적방향까지 이동 및 회전
            //transform.position = Vector3.Lerp(transform.position, receivePos, lerpSpeed * Time.deltaTime);
            //transform.rotation = Quaternion.Lerp(transform.rotation, receiveRot, lerpSpeed * Time.deltaTime);
            return;
            
        }
    }
}
