using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    public float speed = 10;
    public float jumpPower = 3;
    public float jumpCount = 0;
    public float yVelocity;
    public float gravity = -9.81f;
    public CharacterController cc;
    public Text nickName;
    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
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
}
