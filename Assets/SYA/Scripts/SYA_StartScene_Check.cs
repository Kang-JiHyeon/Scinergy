using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SYA_StartScene_Check : MonoBehaviour
{
    public GameObject TermsOfService;
    public GameObject Login;
    public GameObject startButton;
    public Toggle toggle1;
    public Toggle toggle2;
    public Toggle toggle3;
    public Toggle toggle4;

    public InputField pwInputField;

    public void AllToggleCheckButton()
    {
        toggle1.isOn = true;
        toggle2.isOn = true;
        toggle3.isOn = true;
        toggle4.isOn = true;
    }

    public void NextButton()
    {
        
        if(toggle1.isOn&&toggle2.isOn)
        {
            TermsOfService.SetActive(false);
            Login.SetActive(true);
        }
        else
        {
            print("이용약관에 동의해주세요");
        }
    }

    public void LogInButton()
    {
        Login.SetActive(false);
        startButton.SetActive(true);
    }

    InputField.ContentType type; 
    public void PWShow()
    {
        type = pwInputField.contentType;
        if (type == InputField.ContentType.Password)
        {
            type = InputField.ContentType.Standard;
        }
        else
        {
            type = InputField.ContentType.Password;
        }
        pwInputField.contentType = type;
    }

    private void Update()
    {

    }
}
