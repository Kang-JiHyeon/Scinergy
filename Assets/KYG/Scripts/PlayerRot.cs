using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRot : MonoBehaviour
{
    public float rotSpeed = 205;
    public Transform camPos;
    float mx;
    float my;
    public float rotX;
    public float rotY;
    public bool starLook = false;
    private Vector3 starLookDirection;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (starLook)
        {
            Vector3 starDirection = (starLookDirection - transform.position).normalized;
            Quaternion lookRotationY = Quaternion.LookRotation(new Vector3(starDirection.x, 0, starDirection.z));
            Quaternion LookRotationX = Quaternion.LookRotation(starDirection);
            transform.rotation =Quaternion.Slerp(transform.rotation, lookRotationY, Time.deltaTime);
            camPos.rotation = Quaternion.Slerp(camPos.rotation, LookRotationX, Time.deltaTime);
            print(Vector3.Angle(camPos.eulerAngles, starDirection));
            if (Vector3.Angle(camPos.forward, starDirection) < 1f)
            {
                rotX = transform.eulerAngles.y;
                rotY = -Vector3.Angle(new Vector3(starLookDirection.x,0,starLookDirection.z) - transform.position, starLookDirection - transform.position);
                starLook = false;
            }
        }
        else
        {
            float mx = Input.GetAxis("Mouse X");
            float my = Input.GetAxis("Mouse Y");
            rotX += mx * rotSpeed * Time.deltaTime;
            rotY += -my * rotSpeed * Time.deltaTime;
            //rotY = Mathf.Clamp(rotY, -60f, 60f);
            transform.localEulerAngles = new Vector3(0, rotX, 0);
            camPos.localEulerAngles = new Vector3(rotY, 0, 0);
        }
    }
    public void StarSet(Vector3 starDirection)
    {
        starLook = true;
        starLookDirection = starDirection;
    }

}
