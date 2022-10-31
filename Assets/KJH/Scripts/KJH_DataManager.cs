using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


// KJH_InfoData���� �о�� �����͸� ��ũ�� �ٿ� �ݿ��ϰ� �ʹ�.
public class KJH_DataManager : MonoBehaviour
{
    // ������ õü ���� ��ũ��Ʈ
    KJH_SelectPlanet selectPlanet;
    [Header("CB Info")]
    // õü�̸�
    public Text CB_Name;
    // õü����
    public Text CB_Type;

    /* ���� ���� */
    // �о�� ���� ������
    KJH_Data infoData;
    
    // ���� ����
    public GameObject infoTitleFactory;
    // ���� ����
    public GameObject infoTextFactory;
    // ScrollView�� Content
    public RectTransform trContent;
    // ScrollView�� RectTransform
    public RectTransform trScrollView;
    // ���� Content�� ���� H
    float prevTrContentHeight;

    /* ���� �� ���� */
    [Header("Detail Info")]
    // Grid Layout Group
    public RectTransform trGrid;
    public GameObject detailInfoFactory;



    // Start is called before the first frame update
    void Start()
    {
        infoData = GetComponent<KJH_Data>();
        selectPlanet = Camera.main.GetComponent<KJH_SelectPlanet>();
    }


    public void ChangeInfo()
    {
        ClearContent();

        int index = infoData.cbNames.FindIndex(x => x == selectPlanet.focusTarget.name);

        if (infoData.infos.Count > index && index >= 0)
        {
            CB_Name.text = infoData.infos[index][0];
            CB_Type.text = infoData.infos[index][1];

            // Grid Layout Group�� �߰�
            for (int i = 0; i < infoData.detailInfos[index].Count; i++)
            {
                AddInfo(infoData.detailInfos[index][i].Split(",")[0], detailInfoFactory);
                AddInfo(infoData.detailInfos[index][i].Split(",")[1], detailInfoFactory);
            }


            // Scroll View�� Content �߰�
            for (int i=2; i<infoData.infos[index].Count; i++)
            {
                if(i > 2)
                {
                    AddInfo(infoData.infos[index][i].Split(",")[0], infoTitleFactory);
                } 
                AddInfo(infoData.infos[index][i].Split(",")[1], infoTextFactory);
            }


        }

    }

    void ClearContent()
    {
        for(int i=2; i< trContent.childCount; i++)
        {
            Destroy(trContent.GetChild(i).gameObject);
        }
    }

    // �༺ Ŭ�� ���� �� ȣ��Ǵ� �Լ�...
    // �ش� �༺�� ������ ���� �޾� 
    void AddInfo(string infoText, GameObject addObject)
    {
        // 0. �ٲ�� ���� Content H���� ����.
        prevTrContentHeight = trContent.sizeDelta.y;
        // 1. ChatItem�� �����. (�θ�  Scrollview�� content)
        GameObject item = Instantiate(addObject, trContent);
        // 2. ���� ChatItem���� ChatItem ������Ʈ�� �����´�.
        KJH_DataItem data = item.GetComponent<KJH_DataItem>();
        // 3. ������ ������Ʈ�� s�� ����
        data.SetText(infoText);

        StartCoroutine(AutoScrollBotton());
    }

    IEnumerator AutoScrollBotton()
    {
        yield return null;

        // trScrollView�� H�� Contnet�� H���� Ŀ����(��ũ�� ������ ����)
        if (trContent.sizeDelta.y > trScrollView.sizeDelta.y)
        {
            // 4. ����, Content�� �ٴڿ� ����־��ٸ�
            if (trContent.anchoredPosition.y >= prevTrContentHeight - trScrollView.sizeDelta.y)
            {
                // 5. Content�� y���� �ٽ� �����Ѵ�.
                trContent.anchoredPosition = new Vector2(0, trContent.sizeDelta.y - trScrollView.sizeDelta.y);
            }
        }

    }
}
