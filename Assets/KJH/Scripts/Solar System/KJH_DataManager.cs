using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


// KJH_InfoData���� �о�� �����͸� ��ũ�� �ٿ� �ݿ��ϰ� �ʹ�.
public class KJH_DataManager : MonoBehaviour
{
    // ������ õü ���� ��ũ��Ʈ
    public KJH_SelectPlanet selectPlanet;
    // �о�� ���� ������
    KJH_Data data;

    [Header("Info")]
    // õü�̸�
    public List<Text> CB_Names;
    // õü����
    public Text CB_Type;
    // Info ScrollView�� Content
    public RectTransform trContent_info;
    // Info ScrollView�� RectTransform
    public RectTransform trScrollView_info;

    [Header("Detail Info")]
    public RectTransform trGrid;

    [Header("Structure")]
    public RectTransform trContent_structure;
    public RectTransform trScrollView_structure;

    [Header("Text Prefabs")]
    // ���� ����
    public GameObject titleFactory;
    // ���� ����
    public GameObject textFactory;
    // ������ ����
    public GameObject detailInfoFactory;
    // ���� Content�� ���� H
    float prevTrContentHeight;

    [Header("Button Celestial List")]
    public RectTransform trContent_CBList;


    // Start is called before the first frame update
    void Start()
    {
        data = GetComponent<KJH_Data>();
    }

    public void ChangeInfo()
    {
        ClearContent(trContent_info, 2);
        ClearContent(trGrid);
        ClearContent(trContent_structure);

        int index = data.cbNames.FindIndex(x => x == selectPlanet.focusTarget.name);

        if (data.infos.Count > index && index >= 0)
        {
            for(int i=0; i<CB_Names.Count; i++)
            {
                CB_Names[i].text = data.infos[index][0];
            }

            CB_Type.text = data.infos[index][1];

            // wjdGrid Layout Group�� �߰�
            for (int i = 0; i < data.detailInfos[index].Count; i++)
            {
                AddInfo(data.detailInfos[index][i].Split(",")[0], detailInfoFactory, trGrid);
                AddInfo(data.detailInfos[index][i].Split(",")[1], detailInfoFactory, trGrid);
            }

            // Info Scroll View�� Content �߰�
            for (int i = 2; i < data.infos[index].Count; i++)
            {
                if(i > 2)
                {
                    AddInfo(data.infos[index][i].Split(",")[0], titleFactory, trContent_info);
                } 
                AddInfo(data.infos[index][i].Split(",")[1], textFactory, trContent_info);
            }

            // Structure Scroll View�� Content �߰�
            for (int i = 0; i < data.strucInfos[index].Count; i++)
            {
                AddInfo(data.strucInfos[index][i].Split(",")[0], titleFactory, trContent_structure);
                AddInfo(data.strucInfos[index][i].Split(",")[1], textFactory, trContent_structure);
            }
        }
    }

    void ClearContent(RectTransform tr, int startIndex = 0)
    {
        for(int i = startIndex; i< tr.childCount; i++)
        {
            Destroy(tr.GetChild(i).gameObject);
        }
    }

    /// <summary>
    /// �ش� �༺�� ������ ��ũ�� �ٿ� �߰��ϴ� �Լ�
    /// </summary>
    /// <param name="infoText"> ���� ����</param>
    /// <param name="addObject"> �ؽ�Ʈ ������</param>
    /// <param name="tr"> ������Ʈ�� �߰��� �θ� content </param>
    void AddInfo(string infoText, GameObject addObject, RectTransform tr)
    {
        // 0. �ٲ�� ���� Content H���� ����.
        prevTrContentHeight = trContent_info.sizeDelta.y;
        // 1. ChatItem�� �����. (�θ�  Scrollview�� content)
        GameObject item = Instantiate(addObject, tr);
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
        if (trContent_info.sizeDelta.y > trScrollView_info.sizeDelta.y)
        {
            // 4. ����, Content�� �ٴڿ� ����־��ٸ�
            if (trContent_info.anchoredPosition.y >= prevTrContentHeight - trScrollView_info.sizeDelta.y)
            {
                // 5. Content�� y���� �ٽ� �����Ѵ�.
                trContent_info.anchoredPosition = new Vector2(0, trContent_info.sizeDelta.y - trScrollView_info.sizeDelta.y);
            }
        }
    }

}
