using System.IO;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���������� ���� ��� ����� ���� ���� ������ �о���� ��
public class SYA_DirectoryController : MonoBehaviour
{
    //���� ����
    private DirectoryInfo currentDirectory;

        //�������ڸ���
    private void Awake()
    {
    //���α׷��� �ֻ�ܿ� Ȱ��ȭ�� �ƴϾ �÷���(���ø����̼� ����)
        Application.runInBackground = true;

        //���� ��� ����ȭ������ ����
        //Environment.GetFolderPath() �����쿡 �����ϴ� ���� ��θ� ������ �޼ҵ�
        //Environment.SpecialFolder �����쿡 �����ϴ� Ư�� ������ ������
        string deskyopFolder =Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        currentDirectory = new DirectoryInfo(deskyopFolder);

        //���� ������ �����ϴ� ���丮, ���� ����
        UdateDirectory(currentDirectory);
    }

    private void UdateDirectory(DirectoryInfo currentDirectory)
    {
        throw new NotImplementedException();
    }

    /// <sumary>
    /// ���� ���� ������Ʈ
    /// </sumary>
    //
}
