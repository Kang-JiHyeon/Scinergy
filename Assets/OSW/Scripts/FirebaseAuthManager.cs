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

    // 파이어베이스 로그인 또는 회원가입 등에 사용
    private FirebaseAuth auth;
    // 인증이 완료된 유저 정보
    private FirebaseUser user;

    public string UserId => user.UserId;

    public Action<bool> LoginState;

    public void Init()
    {
        // 파이어베이스 인증 객체를 초기화(파이어베이스를 사용할 기본준비 셋팅)
        auth = FirebaseAuth.DefaultInstance;

        // 임시처리
        if (auth.CurrentUser != null)
        {
            LogOut();
        }

        auth.StateChanged += OnChanged;
    }

    private void OnChanged(object sender, EventArgs e)
    {
        // Currentuser가 user가 아니라면
        if (auth.CurrentUser != user)
        {
            bool signed = (auth.CurrentUser != user && auth.CurrentUser != null);
            if (!signed && user != null)
            {
                Debug.Log("로그아웃");
                LoginState?.Invoke(false);
            }
            user = auth.CurrentUser;
            if (signed)
            {
                Debug.Log("로그인");
                LoginState?.Invoke(true);
            }
        }
    }

    // 회원가입
    public void SignUp(string email, string passsword)
    {
        auth.CreateUserWithEmailAndPasswordAsync(email, passsword).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
                //Debug.LogError("회원가입 취소");
                return;
            }
            // 회원가입 실패 이유 => 이메일이 형식에 맞지 않거나 / 비밀번호가 너무 간단 / 이미 가입된 이메일 등등...
            if (task.IsFaulted)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                //Debug.LogError("회원가입 실패");
                return;
            }

            FirebaseUser newUser = task.Result;
            //Debug.LogError("회원가입 완료");
            Debug.LogFormat("Firebase user created successfully: {0} ({1})",
                newUser.DisplayName, newUser.UserId);
        });
    }

    // 로그인
    public void LogIn(string email, string passsword)
    {
        auth.SignInWithEmailAndPasswordAsync(email, passsword).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                //Debug.LogError("로그인 취소");
                Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
                return;
            }
            // 로그인 실패 이유 => 이메일이 형식에 맞지 않거나 / 비밀번호가 너무 간단 / 이미 가입된 이메일 등등...
            if (task.IsFaulted)
            {
                //Debug.LogError("로그인 실패");
                Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                return;
            }

            FirebaseUser newUser = task.Result;
            Debug.LogError("로그인 완료");
            Debug.LogFormat("User signed in successfully: {0} ({1})",
                 newUser.DisplayName, newUser.UserId);
        });
    }

    // 로그아웃
    public void LogOut()
    {
        auth.SignOut();
        Debug.Log("로그아웃");
    }

    // 비밀번호를 잊어버렸을 때 재설정
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
