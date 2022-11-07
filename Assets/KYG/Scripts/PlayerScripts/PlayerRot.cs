using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerRot : MonoBehaviour
{
    public float rotSpeed = 205;
    public Transform camPos;
    float mx;
    float my;
    float rotX;
    float rotY;
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
                CamNormal();
                return;
            case CamState.onUI:
                CamOnUI();
                return;

        }
    }

    private void CamOnUI()
    {
        
        if (SceneManager.GetActiveScene().name == "KYG_Scene" && !GetComponent<PlayerControl>().playerUI.activeSelf)
        {
            state = CamState.normal;
        }
        else if(Input.GetKeyDown(KeyCode.B) && SceneManager.GetActiveScene().name != "KYG_Scene")
        {
            state = CamState.normal;
        }
    }

    private void CamNormal()
    {
        float originX = transform.rotation.y;
        float originY = camPos.rotation.x;
        float mx = Input.GetAxis("Mouse X");
        float my = Input.GetAxis("Mouse Y");
        rotX += mx * rotSpeed * Time.deltaTime;
        rotY += -my * rotSpeed * Time.deltaTime;
        rotY = Mathf.Clamp(rotY, -60f, 60f);
        transform.localEulerAngles = new Vector3(0, rotX + originX, 0);
        camPos.localEulerAngles = new Vector3(rotY + originY, 0, 0);
        if (Input.GetKeyDown(KeyCode.B) && SceneManager.GetActiveScene().name != "KYG_Scene")
        {
            state = CamState.onUI;
        }else if(SceneManager.GetActiveScene().name == "KYG_Scene" && GetComponent<PlayerControl>().playerUI.activeSelf)
        {
            state = CamState.onUI;
        }
    }

    private void CamStarLook()
    {
        Vector3 starDirection = (starLookDirection - transform.position).normalized;
        Quaternion lookRotationY = Quaternion.LookRotation(new Vector3(starDirection.x, 0, starDirection.z));
        Quaternion LookRotationX = Quaternion.LookRotation(starDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotationY, Time.deltaTime);
        camPos.rotation = Quaternion.Slerp(camPos.rotation, LookRotationX, Time.deltaTime);
        //transform.rotation = lookRotationY;
        //camPos.rotation = LookRotationX;
        //print(Vector3.Angle(camPos.eulerAngles, starDirection));
        if (Vector3.Angle(camPos.forward, starDirection) < 1f)
        {
            rotX = transform.eulerAngles.y;
            rotY = -Vector3.Angle(new Vector3(starLookDirection.x, 0, starLookDirection.z) - transform.position, starLookDirection - transform.position);
            state = CamState.normal;
        }
    }
}
