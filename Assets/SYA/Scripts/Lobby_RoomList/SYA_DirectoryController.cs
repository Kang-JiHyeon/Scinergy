using System.IO;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//현재폴더의 파일 목록 출력을 위해 현재 폴더를 읽어오는 곳
public class SYA_DirectoryController : MonoBehaviour
{
    //현재 폴더
    private DirectoryInfo currentDirectory;

        //시작하자마자
    private void Awake()
    {
    //프로그램이 최상단에 활성화가 아니어도 플레이(어플리케이션 설정)
        Application.runInBackground = true;

        //최초 경로 바탕화면으로 설정
        //Environment.GetFolderPath() 윈도우에 존재하는 폴더 경로를 얻어오는 메소드
        //Environment.SpecialFolder 윈도우에 존재하는 특수 폴더를 열거형
        string deskyopFolder =Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        currentDirectory = new DirectoryInfo(deskyopFolder);

        //현재 폴더에 존재하는 디렉토리, 파일 생성
        UdateDirectory(currentDirectory);
    }

    private void UdateDirectory(DirectoryInfo currentDirectory)
    {
        throw new NotImplementedException();
    }

    /// <sumary>
    /// 현재 폴더 업데이트
    /// </sumary>
    //
}
