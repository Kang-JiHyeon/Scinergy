using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SYA_Thumbnail : MonoBehaviour
{
    // �̹��� ������ ��
    public Image thumbnail;
    //�̹��� ��������Ʈ �ҽ�, ���
    public List<Sprite> thumbnails = new List<Sprite>();
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
            go.GetComponent<Image>().sprite = thumbnails[i];
        }
    }

/*    // Update is called once per frame
    void Update()
    {
        
    }*/
}
