using System;
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
    public enum CamState 
    {
        normal,
        starLook,
        onUI,
    }

    public CamState state = CamState.normal;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        StateMachine();
    }
    public void StarSet(Vector3 starDirection)
    {
        state = CamState.starLook;
        starLookDirection = starDirection;
    }
    void StateMachine()
    {
        switch(state)
        {
            case CamState.starLook:
                CamStarLook();
                return;
            case CamState.normal:
                CanNormal();
                return;
            case CamState.onUI:
                CamOnUI();
                return;

        }
    }

    private void CamOnUI()
    {
        rotX = rotY = 0;
        if (!GetComponent<PlayerControl>().playerUI.activeSelf)
        {
            state = CamState.normal;
        }
    }

    private void CanNormal()
    {
        if (GetComponent<PlayerControl>().playerUI.activeSelf)
        {
            state = CamState.onUI;
        }
        float mx = Input.GetAxis("Mouse X");
        float my = Input.GetAxis("Mouse Y");
        rotX += mx * rotSpeed * Time.deltaTime;
        rotY += -my * rotSpeed * Time.deltaTime;
        //rotY = Mathf.Clamp(rotY, -60f, 60f);
        transform.localEulerAngles = new Vector3(0, rotX, 0);
        camPos.localEulerAngles = new Vector3(rotY, 0, 0);
    }

    private void CamStarLook()
    {
        Vector3 starDirection = (starLookDirection - transform.position).normalized;
        Quaternion lookRotationY = Quaternion.LookRotation(new Vector3(starDirection.x, 0, starDirection.z));
        Quaternion LookRotationX = Quaternion.LookRotation(starDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotationY, Time.deltaTime);
        camPos.rotation = Quaternion.Slerp(camPos.rotation, LookRotationX, Time.deltaTime);
        print(Vector3.Angle(camPos.eulerAngles, starDirection));
        if (Vector3.Angle(camPos.forward, starDirection) < 1f)
        {
            rotX = transform.eulerAngles.y;
            rotY = -Vector3.Angle(new Vector3(starLookDirection.x, 0, starLookDirection.z) - transform.position, starLookDirection - transform.position);
            state = CamState.normal;
        }
    }
}
