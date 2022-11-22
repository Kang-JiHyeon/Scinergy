using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SYA_ImageLoader : MonoBehaviour
{
    //�̹��� ������ ����ϴ� Panel
    [SerializeField]
    private GameObject panelLmageViewer;

    //������ ��Ÿ���� �̹��� ���
    [SerializeField]
    private Image imageDrawTexture;
    //���� �̸�, �ػ�, �뷮
    [SerializeField]
    private Text textFileData;

    //image ui �ִ� ũ��
    private float maxwidth = 670;
    private float maxHeight = 385;

    //�ش� ���ϳ��� �̹��� ������ �ҷ����� ��
    public void OnLoad(FileInfo file)
    {
        //�̹��� ������ ����ϴ� Panel Ȱ��ȭ
        panelLmageViewer.SetActive(true);
        //���Ϸκ��� bytes �����͸� �ҷ��´�
        byte[] byteTexture = File.ReadAllBytes(file.FullName);
        //���� �ؽ��Ŀ��� ����Ʈ �迭 ������ �������� Texture2D �̹��� ���� ������ ����
        Texture2D texture2D = new Texture2D(0, 0);
        if(byteTexture.Length>0)//������ ������->�޾ƿ� ���� ������
        {
            texture2D.LoadImage(byteTexture);
        }

        //����ϴ� �̹����� �̹���UI�� ũ�� ����
        //������ �ִ� ���� ũ��
        //������ ���߾� �ٿ��ֱ�
        if(texture2D.width > maxwidth)
        {
            imageDrawTexture.rectTransform.sizeDelta = new Vector2(maxwidth, maxwidth / texture2D.width * texture2D.height);
        }
        else if(texture2D.height > maxHeight)
        { 
            imageDrawTexture.rectTransform.sizeDelta = new Vector2(maxHeight / texture2D.height * texture2D.width, maxHeight);
        }
        else
        {
            imageDrawTexture.rectTransform.sizeDelta = new Vector2(maxwidth, maxHeight);
        }

        //�ؽ��� 2�� ��������Ʈ ���·� �ٲپ� �ֱ�
        Sprite sprite = Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), new Vector2(0.5f, 0.5f));

        //imageDrawTexture �̹��� UI�� �������� �̹����� ���� ��������Ʈ��
        imageDrawTexture.sprite = sprite;

        //�̹��� ���� ���� ���
        textFileData.text = $"{file.Name} ({texture2D.width} * {texture2D.height})";
    }

    public void OffLoad()
    {
        panelLmageViewer.SetActive(false);
    }
}
