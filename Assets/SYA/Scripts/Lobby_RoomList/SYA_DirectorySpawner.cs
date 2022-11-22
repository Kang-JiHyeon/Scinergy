using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SYA_DirectorySpawner : MonoBehaviour
{
    //현재 경로 이름을 나타내는 텍스트 유아이
    [SerializeField]
    private Text textDirectoryName;
    //폴더 파일들이 배치되는 스크롤뷰의 스크롤바
    [SerializeField]
    private Scrollbar verticalScrollbar;

    //현재 폴더에 존재하는 폴더, 파일의 파일명을 나타내는 프리팹
    [SerializeField]
    private GameObject panelDataPrefab;
    //생성되는 텍스트 유아이가 저장되는 부모 오브젝트
    [SerializeField]
    private Transform parentContent;

    //directorycontroller 주소정보, 데이터 클래스 전달
    private SYA_DirectoryController directoryController;

    //현재 폴더에 존재하는 파일 리스트
    private List<SYA_Data> fileList;

    public void SetUp(SYA_DirectoryController controller)
    {
        directoryController = controller;
        //현재 폴더에 존재하는 디렉토리, 파일 오브젝트 리스트
        fileList = new List<SYA_Data>();
    }

    /// <summary>
    /// 현재 경로에 존재하는 폴더, 파일의 텍스쳐 UI 생성
    /// </summary>
    public void UpdateDirectory(DirectoryInfo currentDirectory)
    {
        //기존에 생성되어 있는 데이터 정보 삭제
        for(int i=0; i<fileList.Count; ++i)
        {
            Destroy(fileList[i].gameObject);
        }
        fileList.Clear();

        //현재 폴더 이름 출력
        textDirectoryName.text = currentDirectory.Name;

        //스크롤바 밸루 1로 설정해서 제일 위로 이동
        verticalScrollbar.value = 1;

        //상위 폴더로 이동하기 위해 "..." 생성
        SpawData("...", DataType.Diretory);

        //현재폴더에 존재하는 모든 폴더를 텍스쳐 UI로 생성
        foreach(DirectoryInfo directory in currentDirectory.GetDirectories())
        {
            SpawData(directory.Name, DataType.Diretory);
        }

        //현재폴더에 존재하는 모든 파일을 텍스쳐 UI로 생성
        foreach (FileInfo file in currentDirectory.GetFiles())
        {
            if (file.FullName.Contains(".jpg") || file.FullName.Contains(".png"))
            {
                SpawData(file.Name, DataType.File);
            }
        }

        //오름차순으로 정렬
        fileList.Sort((a, b) => a.FileName.CompareTo(b.FileName));

        //정렬이 완료된 리스트 기준으로 화면 오브젝트도 정렬
        //상위폴더로 가는 상단에 위치
        for(int i=0; i< fileList.Count; ++i)
        {
            fileList[i].transform.SetSiblingIndex(i);
            if(fileList[i].FileName.Equals("..."))
            {
                fileList[i].transform.SetAsFirstSibling();
            }
        }
    }

    private void SpawData(string fileName, DataType type)
    {
        GameObject clone = Instantiate(panelDataPrefab);

        clone.transform.SetParent(parentContent);
        clone.transform.localScale = Vector3.one;

        SYA_Data data = clone.GetComponent<SYA_Data>();
        data.SetUp(directoryController,fileName, type);

        fileList.Add(data);
    }
}
