using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SYA_DirectorySpawner : MonoBehaviour
{
    //���� ��� �̸��� ��Ÿ���� �ؽ�Ʈ ������
    [SerializeField]
    private Text textDirectoryName;
    //���� ���ϵ��� ��ġ�Ǵ� ��ũ�Ѻ��� ��ũ�ѹ�
    [SerializeField]
    private Scrollbar verticalScrollbar;

    //���� ������ �����ϴ� ����, ������ ���ϸ��� ��Ÿ���� ������
    [SerializeField]
    private GameObject panelDataPrefab;
    //�����Ǵ� �ؽ�Ʈ �����̰� ����Ǵ� �θ� ������Ʈ
    [SerializeField]
    private Transform parentContent;

    //directorycontroller �ּ�����, ������ Ŭ���� ����
    private SYA_DirectoryController directoryController;

    //���� ������ �����ϴ� ���� ����Ʈ
    private List<SYA_Data> fileList;

    public void SetUp(SYA_DirectoryController controller)
    {
        directoryController = controller;
        //���� ������ �����ϴ� ���丮, ���� ������Ʈ ����Ʈ
        fileList = new List<SYA_Data>();
    }

    /// <summary>
    /// ���� ��ο� �����ϴ� ����, ������ �ؽ��� UI ����
    /// </summary>
    public void UpdateDirectory(DirectoryInfo currentDirectory)
    {
        //������ �����Ǿ� �ִ� ������ ���� ����
        for(int i=0; i<fileList.Count; ++i)
        {
            Destroy(fileList[i].gameObject);
        }
        fileList.Clear();

        //���� ���� �̸� ���
        textDirectoryName.text = currentDirectory.Name;

        //��ũ�ѹ� ��� 1�� �����ؼ� ���� ���� �̵�
        verticalScrollbar.value = 1;

        //���� ������ �̵��ϱ� ���� "..." ����
        SpawData("...", DataType.Diretory);

        //���������� �����ϴ� ��� ������ �ؽ��� UI�� ����
        foreach(DirectoryInfo directory in currentDirectory.GetDirectories())
        {
            SpawData(directory.Name, DataType.Diretory);
        }

        //���������� �����ϴ� ��� ������ �ؽ��� UI�� ����
        foreach (FileInfo file in currentDirectory.GetFiles())
        {
            if (file.FullName.Contains(".jpg") || file.FullName.Contains(".png"))
            {
                SpawData(file.Name, DataType.File);
            }
        }

        //������������ ����
        fileList.Sort((a, b) => a.FileName.CompareTo(b.FileName));

        //������ �Ϸ�� ����Ʈ �������� ȭ�� ������Ʈ�� ����
        //���������� ���� ��ܿ� ��ġ
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
