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


    public void ChangeInfo()
    {
        ClearContent(trContent_info, 2);
        ClearContent(trGrid);
        ClearContent(trContent_structure);

        int index = KJH_Data.instance.cbNames.FindIndex(x => x == selectPlanet.focusTarget.name);

        if (KJH_Data.instance.infos.Count > index && index >= 0)
        {
            for(int i=0; i<CB_Names.Count; i++)
                CB_Names[i].text = KJH_Data.instance.infos[index][0];

            CB_Type.text = KJH_Data.instance.infos[index][1];

            // �����󼼺��� Grid Layout Group�� �߰�
            for (int i = 0; i < KJH_Data.instance.detailInfos[index].Count; i++)
            {
                string[] s = KJH_Data.instance.detailInfos[index][i].Split(",");
                AddInfo(s[0], detailInfoFactory, trGrid);
                AddInfo(s[1], detailInfoFactory, trGrid);
            }

            // Info Scroll View�� Content�� �߰�
            AddInfo(KJH_Data.instance.infos[index][2].Split(",")[1], textFactory, trContent_info);
            for (int i = 3; i < KJH_Data.instance.infos[index].Count; i++)
            {
                string[] s = KJH_Data.instance.infos[index][i].Split(",");
                AddInfo(s[0], titleFactory, trContent_info);
                AddInfo(s[1], textFactory, trContent_info);
            }

            // Structure Scroll View�� Content�� �߰�
            for (int i = 0; i < KJH_Data.instance.strucInfos[index].Count; i++)
            {
                string[] s = KJH_Data.instance.strucInfos[index][i].Split(",");
                AddInfo(s[0], titleFactory, trContent_structure);
                AddInfo(s[1], textFactory, trContent_structure);
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
        // 1.. �ٲ�� ���� Content H���� ����
        prevTrContentHeight = trContent_info.sizeDelta.y;
        // 2. text item ��ü ����
        GameObject item = Instantiate(addObject, tr);
        // 3. ���� item���� DataItem ������Ʈ�� �����´�
        KJH_DataItem data = item.GetComponent<KJH_DataItem>();
        // 4. ������ ������Ʈ�� data�� ����
        data.SetText(infoText);

        StartCoroutine(AutoScrollBotton());
    }

    IEnumerator AutoScrollBotton()
    {
        yield return null;

        // trScrollView�� H�� Content�� H���� Ŀ����(��ũ�� ������ ����)
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
