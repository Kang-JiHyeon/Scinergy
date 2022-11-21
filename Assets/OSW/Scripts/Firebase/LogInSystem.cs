using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SYA_UserInfoManagerSaveLoad;

public class LogInSystem : MonoBehaviour
{
    // 이메일 및 패스워드 UI
    public InputField emailInputField;
    public InputField passwordInputField;

    // 결과 표시할 Text
    public Text outputText;

    void Start()
    {
        FirebaseAuthManager.Instance.LoginState += OnChangedState;
        FirebaseAuthManager.Instance.Init();
    }

    private void OnChangedState(bool sign)
    {
        outputText.text = sign ? "로그인 : " : "로그아웃 : ";
        outputText.text += FirebaseAuthManager.Instance.UserId;
    }

    public void SignUp()
    {
        string e = emailInputField.text;
        string p = passwordInputField.text;

        //FirebaseAuthManager.Instance.SignUp(e, p);
        SYA_UserInfoSave.Instance.IdPasswardSave(e, p);
    }

    public void LogIn()
    {
        FirebaseAuthManager.Instance.LogIn(emailInputField.text, passwordInputField.text);
    }

    public void LogOut()
    {
        FirebaseAuthManager.Instance.LogOut();
    }

    public void FindPassword()
    {
        FirebaseAuthManager.Instance.FindPassword(emailInputField.text);
    }
}
