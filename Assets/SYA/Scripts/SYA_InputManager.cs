using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SYA_InputManager : MonoBehaviour
{
    
    static Vector3 firstPosition;
    static bool bDown = false; 
    //�ȵ���̵���
#if Androids
    static bool bJoystick = true;
    //PC���
#elif PC
        bool bJoystick = false;
#endif

    public static float GetAxis(string axis)
    {
        if (bJoystick == false)
        {
            return Input.GetAxis(axis);
        }
        //�ȵ���̵���
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
        //�ȵ���̵���
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

    // ���ⱸ�ϱ�
    public static Vector3 GetDirection()
    {
        Vector3 dir = Vector3.zero;
        Touch touch = Input.GetTouch(0);
        // ����ڰ� ó�� Ŭ���ߴٸ�
        if (touch.phase == TouchPhase.Began)
        {
            // Ŭ�� ���̴�.
            bDown = true;
            // ù��° ���� Ŭ���� ������ ���콺 ��ġ
            firstPosition = touch.position;
        }
        if (touch.phase == TouchPhase.Ended)
        {
            bDown = false;
        }
        // ���콺�� Ŭ�����̶��
        if (bDown)
        {
            //������ ���� -> ���ⱸ�ϱ�
            dir = new Vector3(touch.position.x, touch.position.y, 0) - new Vector3(firstPosition.x, firstPosition.y, 0);
        }

        return dir;//.normalized;
    }
    public static bool GetJump()
    {
        return true;
    }
}
