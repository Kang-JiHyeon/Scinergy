using System.IO;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���������� ���� ��� ����� ���� ���� ������ �о���� ��
public class SYA_DirectoryController : MonoBehaviour
{
    //�⺻ ����
    private DirectoryInfo defaultDirectory;
    //���� ����
    private DirectoryInfo currentDirectory;
    //���� ��ο� �ִ� ����, ���� ���� ����/ ���� ����
    private SYA_DirectorySpawner directorySpawner;

    public GameObject fileLoaderSystem;

        //�������ڸ���
    private void Awake()
    {
    //���α׷��� �ֻ�ܿ� Ȱ��ȭ�� �ƴϾ �÷���(���ø����̼� ����)
        Application.runInBackground = true;

        directorySpawner = GetComponent<SYA_DirectorySpawner>();
        directorySpawner.SetUp(this);

        //���� ��� ����ȭ������ ����
        //Environment.GetFolderPath() �����쿡 �����ϴ� ���� ��θ� ������ �޼ҵ�
        //Environment.SpecialFolder �����쿡 �����ϴ� Ư�� ������ ������
        string deskyopFolder =Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        defaultDirectory = new DirectoryInfo(deskyopFolder);
        currentDirectory = new DirectoryInfo(deskyopFolder);

        //���� ������ �����ϴ� ���丮, ���� ����
        UpdateDirectory(currentDirectory);
    }

    private void Update()
    {
        //esc������ ����ȭ�� ����
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            UpdateDirectory(defaultDirectory);
        }
        //�齺���̽� ������ ��������
        if(Input.GetKeyDown(KeyCode.Backspace))
        {
            UpdateDirectory(currentDirectory);
        }
    }

    /// <sumary>
    /// ���� ���� ������Ʈ
    /// </sumary>
    private void UpdateDirectory(DirectoryInfo directory)
    {
        //���� ��� ����
        currentDirectory = directory;

        //���� ������ �����ϴ� ��� ����, ���� PanelData ����
        directorySpawner.UpdateDirectory(currentDirectory);

        /*//���� ���� �̸� ���
        Debug.Log($"���� ������ : {currentDirectory.Name}");

        //���� ������ �����ϴ� ��� ���� �̸� ���
        foreach(DirectoryInfo dir in currentDirectory.GetDirectories())
        {
            Debug.Log(dir.Name);
        }

        //���� ������ �����ϴ� ��� ���� �̸� ���
        foreach (FileInfo file in currentDirectory.GetFiles())
        {
            Debug.Log(file.Name);
        }*/

    }
    
    private void MoveToParentFolder(DirectoryInfo directory)
    {
        //���� ������ ���ٸ� ����
        if (directory.Parent == null) return;
        UpdateDirectory(directory.Parent);
    }

    public void UpdateInput(string data)
    {
        //������ ����� "..."��� ���� ������
        if(data.Equals("..."))
        {
            MoveToParentFolder(currentDirectory);
            return;
        }

        //������ ����� ������� ������ ���� ���η� �̵�
        foreach(DirectoryInfo directory in currentDirectory.GetDirectories())
        {
            if(data.Equals(directory.Name))
            {
                UpdateDirectory(directory);
                return;
            }
        }

        //������ ����� �����̐� Ȯ���ڿ� ���� ó��
        foreach (FileInfo file in currentDirectory.GetFiles())
        {
            if (data.Equals(file.Name))
            {
                fileLoaderSystem.GetComponent<SYA_ImageLoader>().OnLoad(file);
            }
        }
    }
}
