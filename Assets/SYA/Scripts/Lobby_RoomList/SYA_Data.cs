using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//������ �Ӽ� (����, ����) - ������ ��¿�
public enum DataType {Diretory=0, File }

public class SYA_Data : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    //�����ܿ� ������ �� �ִ� ��������Ʈ ������
    [SerializeField]
    private Sprite[] spriteIcon;


    //������ �Ӽ��� ���� ������ ���
    private Image imageIcon;
    //������ �̸� ���
    private Text textDataName;

    //������ �Ӽ�
    private DataType dataType;

    //���� �̸�
    private string fileName;
    //�ܺο��� Ȯ���ϱ� ���� GET������Ƽ
    public string FileName => fileName;

    //���� �̸��� �ִ� ����
    private int maxFileNameLength = 20;

    private SYA_DirectoryController directoryController;

    //�޾ƿ� ������ ������ ������ ����
    public void SetUp(SYA_DirectoryController controller ,string fileName, DataType dataType)
    {
        directoryController = controller;
        //���� �ǳڵ������� ������Ʈ�� �̹��� ������Ʈ�� �������� �ʾҴٸ� �ǳڵ����� ��
        imageIcon = GetComponentInChildren<Image>();
        textDataName = GetComponentInChildren<Text>();

        this.fileName = fileName;
        this.dataType = dataType;

        //������ �̹��� ����
        imageIcon.sprite = spriteIcon[(int)this.dataType];

        //���� �̸� ���
        textDataName.text = this.fileName;
        //���� �̸��� �ִ� ���� maxFileNameLength�� �Ѿ�� �̸��� �޺κ� �ڸ��� "..."�߰�
        if(fileName.Length>=maxFileNameLength)
        {
            textDataName.text = fileName.Substring(0, maxFileNameLength);
            textDataName.text += "...";
        }

        //�����̸� ������
        SetTextColor();
    }

    private void SetTextColor()
    {
        if(dataType == DataType.Diretory)
        {
            textDataName.color = Color.blue;
        }
        else
        {
            textDataName.color = Color.black;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        textDataName.color = Color.red;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        directoryController.UpdateInput(fileName);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        SetTextColor();
    }
}
