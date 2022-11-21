using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SYA_UserInfoManagerSaveLoad;

public class LogInSystem : MonoBehaviour
{
    // �̸��� �� �н����� UI
    public InputField emailInputField;
    public InputField passwordInputField;

    // ��� ǥ���� Text
    public Text outputText;

    void Start()
    {
        FirebaseAuthManager.Instance.LoginState += OnChangedState;
        FirebaseAuthManager.Instance.Init();
    }

    private void OnChangedState(bool sign)
    {
        outputText.text = sign ? "�α��� : " : "�α׾ƿ� : ";
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
