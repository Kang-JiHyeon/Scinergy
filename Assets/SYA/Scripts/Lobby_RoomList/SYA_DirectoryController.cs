using System.IO;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//현재폴더의 파일 목록 출력을 위해 현재 폴더를 읽어오는 곳
public class SYA_DirectoryController : MonoBehaviour
{
    //기본 폴더
    private DirectoryInfo defaultDirectory;
    //현재 폴더
    private DirectoryInfo currentDirectory;
    //현재 경로에 있는 폴더, 파일 정보 생성/ 삭제 제어
    private SYA_DirectorySpawner directorySpawner;

    public GameObject fileLoaderSystem;

        //시작하자마자
    private void Awake()
    {
    //프로그램이 최상단에 활성화가 아니어도 플레이(어플리케이션 설정)
        Application.runInBackground = true;

        directorySpawner = GetComponent<SYA_DirectorySpawner>();
        directorySpawner.SetUp(this);

        //최초 경로 바탕화면으로 설정
        //Environment.GetFolderPath() 윈도우에 존재하는 폴더 경로를 얻어오는 메소드
        //Environment.SpecialFolder 윈도우에 존재하는 특수 폴더를 열거형
        string deskyopFolder =Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        defaultDirectory = new DirectoryInfo(deskyopFolder);
        currentDirectory = new DirectoryInfo(deskyopFolder);

        //현재 폴더에 존재하는 디렉토리, 파일 생성
        UpdateDirectory(currentDirectory);
    }

    private void Update()
    {
        //esc누르면 바탕화면 폴더
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            UpdateDirectory(defaultDirectory);
        }
        //백스페이스 누르면 상위폴더
        if(Input.GetKeyDown(KeyCode.Backspace))
        {
            UpdateDirectory(currentDirectory);
        }
    }

    /// <sumary>
    /// 현재 폴더 업데이트
    /// </sumary>
    private void UpdateDirectory(DirectoryInfo directory)
    {
        //현재 경로 설정
        currentDirectory = directory;

        //현재 폴더에 존재하는 모든 폴더, 파일 PanelData 생성
        directorySpawner.UpdateDirectory(currentDirectory);

        /*//현재 폴더 이름 출력
        Debug.Log($"현재 폴더명 : {currentDirectory.Name}");

        //현재 폴더에 존재하는 모든 폴더 이름 출력
        foreach(DirectoryInfo dir in currentDirectory.GetDirectories())
        {
            Debug.Log(dir.Name);
        }

        //현재 폴더에 존재하는 모든 파일 이름 출력
        foreach (FileInfo file in currentDirectory.GetFiles())
        {
            Debug.Log(file.Name);
        }*/

    }
    
    private void MoveToParentFolder(DirectoryInfo directory)
    {
        //상위 폴더가 없다면 종료
        if (directory.Parent == null) return;
        UpdateDirectory(directory.Parent);
    }

    public void UpdateInput(string data)
    {
        //선택한 목록이 "..."라면 상위 폴더로
        if(data.Equals("..."))
        {
            MoveToParentFolder(currentDirectory);
            return;
        }

        //선택한 목록이 폴더라면 선택한 폴더 내부로 이동
        foreach(DirectoryInfo directory in currentDirectory.GetDirectories())
        {
            if(data.Equals(directory.Name))
            {
                UpdateDirectory(directory);
                return;
            }
        }

        //선택한 목록이 파일이먄 확장자에 따라 처리
        foreach (FileInfo file in currentDirectory.GetFiles())
        {
            if (data.Equals(file.Name))
            {
                fileLoaderSystem.GetComponent<SYA_ImageLoader>().OnLoad(file);
            }
        }
    }
}
