using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SYA_InputManager : MonoBehaviour
{
    
    static Vector3 firstPosition;
    static bool bDown = false; 
    //안드로이드라면
#if Androids
    static bool bJoystick = true;
    //PC라면
#elif PC
        bool bJoystick = false;
#endif

    public static float GetAxis(string axis)
    {
        if (bJoystick == false)
        {
            return Input.GetAxis(axis);
        }
        //안드로이드라면
        if (axis == "Horizontal")
        {
            return GetDirection().x;
        }
        else if (axis == "Vertical")
        {
            return GetDirection().y;
        }

        return 0;
    }

    public static bool GetButtonDown(string Button)
    {
        if (bJoystick == false)
        {
            return Input.GetButtonDown(Button);
        }
        //안드로이드라면
        /*if (Button == "Jump")
        {
            if(jump==1)
            {
                return true;
            }
            return false;
        }*/

        return false;
    }

    // 방향구하기
    public static Vector3 GetDirection()
    {
        Vector3 dir = Vector3.zero;
        Touch touch = Input.GetTouch(0);
        // 사용자가 처음 클릭했다면
        if (touch.phase == TouchPhase.Began)
        {
            // 클릭 중이다.
            bDown = true;
            // 첫번째 점은 클릭한 지점의 마우스 위치
            firstPosition = touch.position;
        }
        if (touch.phase == TouchPhase.Ended)
        {
            bDown = false;
        }
        // 마우스가 클릭중이라면
        if (bDown)
        {
            //벡터의 뺄셈 -> 방향구하기
            dir = new Vector3(touch.position.x, touch.position.y, 0) - new Vector3(firstPosition.x, firstPosition.y, 0);
        }

        return dir;//.normalized;
    }
    public static bool GetJump()
    {
        return true;
    }
}
