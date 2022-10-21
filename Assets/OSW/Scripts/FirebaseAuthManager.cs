using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;
using System;

public class FirebaseAuthManager : MonoBehaviour
{
    private static FirebaseAuthManager instance = null;

    public static FirebaseAuthManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new FirebaseAuthManager();
            }
            return instance;
        }
    }

    // ���̾�̽� �α��� �Ǵ� ȸ������ � ���
    private FirebaseAuth auth;
    // ������ �Ϸ�� ���� ����
    private FirebaseUser user;

    public string UserId => user.UserId;

    public Action<bool> LoginState;

    public void Init()
    {
        // ���̾�̽� ���� ��ü�� �ʱ�ȭ(���̾�̽��� ����� �⺻�غ� ����)
        auth = FirebaseAuth.DefaultInstance;

        // �ӽ�ó��
        if (auth.CurrentUser != null)
        {
            LogOut();
        }

        auth.StateChanged += OnChanged;
    }

    private void OnChanged(object sender, EventArgs e)
    {
        // Currentuser�� user�� �ƴ϶��
        if (auth.CurrentUser != user)
        {
            bool signed = (auth.CurrentUser != user && auth.CurrentUser != null);
            if (!signed && user != null)
            {
                Debug.Log("�α׾ƿ�");
                LoginState?.Invoke(false);
            }
            user = auth.CurrentUser;
            if (signed)
            {
                Debug.Log("�α���");
                LoginState?.Invoke(true);
            }
        }
    }

    // ȸ������
    public void SignUp(string email, string passsword)
    {
        auth.CreateUserWithEmailAndPasswordAsync(email, passsword).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
                //Debug.LogError("ȸ������ ���");
                return;
            }
            // ȸ������ ���� ���� => �̸����� ���Ŀ� ���� �ʰų� / ��й�ȣ�� �ʹ� ���� / �̹� ���Ե� �̸��� ���...
            if (task.IsFaulted)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                //Debug.LogError("ȸ������ ����");
                return;
            }

            FirebaseUser newUser = task.Result;
            //Debug.LogError("ȸ������ �Ϸ�");
            Debug.LogFormat("Firebase user created successfully: {0} ({1})",
                newUser.DisplayName, newUser.UserId);
        });
    }

    // �α���
    public void LogIn(string email, string passsword)
    {
        auth.SignInWithEmailAndPasswordAsync(email, passsword).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                //Debug.LogError("�α��� ���");
                Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
                return;
            }
            // �α��� ���� ���� => �̸����� ���Ŀ� ���� �ʰų� / ��й�ȣ�� �ʹ� ���� / �̹� ���Ե� �̸��� ���...
            if (task.IsFaulted)
            {
                //Debug.LogError("�α��� ����");
                Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                return;
            }

            FirebaseUser newUser = task.Result;
            Debug.LogError("�α��� �Ϸ�");
            Debug.LogFormat("User signed in successfully: {0} ({1})",
                 newUser.DisplayName, newUser.UserId);
        });
    }

    // �α׾ƿ�
    public void LogOut()
    {
        auth.SignOut();
        Debug.Log("�α׾ƿ�");
    }

    // ��й�ȣ�� �ؾ������ �� �缳��
    public void FindPassword(string email)
    {
        if (user != null)
        {
            auth.SendPasswordResetEmailAsync(email).ContinueWith(task => {
                if (task.IsCanceled)
                {
                    Debug.LogError("SendPasswordResetEmailAsync was canceled.");
                    return;
                }
                if (task.IsFaulted)
                {
                    Debug.LogError("SendPasswordResetEmailAsync encountered an error: " + task.Exception);
                    return;
                }

                Debug.Log("Password reset email sent successfully.");
            });
        }
    }
}
