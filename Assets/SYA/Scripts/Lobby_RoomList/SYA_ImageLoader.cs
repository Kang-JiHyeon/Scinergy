using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SYA_ImageLoader : MonoBehaviour
{
    //�̹��� ������ ����ϴ� Panel
    //[SerializeField]
    public GameObject panelLmageViewer;

    //������ ��Ÿ���� �̹��� ���
    [SerializeField]
    private Image imageDrawTexture;
    //���� �̸�, �ػ�, �뷮
    [SerializeField]
    private Text textFileData;

    //image ui �ִ� ũ��
    private float maxwidth = 670;
    private float maxHeight = 385;

    Texture2D texture2D;
    string path_;
    long size;

    //�ش� ���ϳ��� �̹��� ������ �ҷ����� ��
    public void OnLoad(FileInfo file)
    {
        //�̹��� ������ ����ϴ� Panel Ȱ��ȭ
        panelLmageViewer.SetActive(true);
        path_ = file.FullName;
        size = file.Length;
        //���Ϸκ��� bytes �����͸� �ҷ��´�
        byte[] byteTexture = File.ReadAllBytes(file.FullName);
        //���� �ؽ��Ŀ��� ����Ʈ �迭 ������ �������� Texture2D �̹��� ���� ������ ����
        texture2D = new Texture2D(0, 0);
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
        textFileData.text = $"{file.Name} ({texture2D.width} * {texture2D.height}) / {size}bytes";
    }

    public void OffLoad()
    {
        panelLmageViewer.transform.parent.gameObject.SetActive(false);
            sizeOverText.SetActive(false);
    }

    public SYA_SympoLobby SympoLobby;
    public SYA_Thumbnail Thumbnail;
    //�뷮�� �ʹ� ũ�ٴ� ����
    public GameObject sizeOverText;
    //Ȯ���� ����,��
    //��������Ʈ �ҽ������� �� �ҽ��� ������
    public void OnClickAddSpriteList()
    {
        //�뷮 Ȯ��
        if (texture2D.width <= 1000 && texture2D.height <= 1000)
        {
            Thumbnail.thumbnail.texture = texture2D;
            SympoLobby.custom = true;
            SympoLobby.path = path_;
            OffLoad();
        }
        //������ ������ ���� ���
        else
        {
            sizeOverText.SetActive(true);
        }
    }
}
