using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.UI;

public class SYA_Thumbnail : MonoBehaviour
{
    // �̹��� ������ ��
    public RawImage thumbnail;
    //�̹��� �迭 �����ϴ� ��
    byte[] byteTexture;
    //�̹��� ��������Ʈ �ҽ�, ���
    public List<Texture2D> thumbnails = new List<Texture2D>();
    //�����Ǵ� �� �θ�
    public Transform content;
    //������ ������
    public GameObject item;
    //�Ϸ� ��ư ������ â�ݱ�

    public void OnClickTh()
    {
        gameObject.SetActive(false);
    }

// Start is called before the first frame update
    void Start()
    {
        SetTh();
    }

    //�ҽ�����Ʈ��� ����
    void SetTh()
    {
        for(int i=0; i<thumbnails.Count; ++i)
        {
            GameObject go = Instantiate(item, content);
            go.GetComponent<RawImage>().texture = thumbnails[i];
        }
    }

    public GameObject fileGo;
    //��ư�� ������ ���� â�� ������
    public void OnClickFile()
    {
        fileGo.SetActive(!fileGo.activeSelf);
    }

    /*    // Update is called once per frame
        void Update()
        {

        }*/
}
