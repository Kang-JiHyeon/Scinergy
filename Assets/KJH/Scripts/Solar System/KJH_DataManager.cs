using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


// KJH_InfoData에서 읽어온 데이터를 스크롤 바에 반영하고 싶다.
public class KJH_DataManager : MonoBehaviour
{
    // 선택한 천체 관련 스크립트
    public KJH_SelectPlanet selectPlanet;
    // 읽어온 정보 데이터
    KJH_Data data;

    [Header("Info")]
    // 천체이름
    public List<Text> CB_Names;
    // 천체종류
    public Text CB_Type;
    // Info ScrollView의 Content
    public RectTransform trContent_info;
    // Info ScrollView의 RectTransform
    public RectTransform trScrollView_info;

    [Header("Detail Info")]
    public RectTransform trGrid;

    [Header("Structure")]
    public RectTransform trContent_structure;
    public RectTransform trScrollView_structure;

    [Header("Text Prefabs")]
    // 정보 제목
    public GameObject titleFactory;
    // 정보 내용
    public GameObject textFactory;
    // 상세정보 내용
    public GameObject detailInfoFactory;
    // 이전 Content의 높이 H
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

            // wjdGrid Layout Group에 추가
            for (int i = 0; i < data.detailInfos[index].Count; i++)
            {
                AddInfo(data.detailInfos[index][i].Split(",")[0], detailInfoFactory, trGrid);
                AddInfo(data.detailInfos[index][i].Split(",")[1], detailInfoFactory, trGrid);
            }

            // Info Scroll View의 Content 추가
            for (int i = 2; i < data.infos[index].Count; i++)
            {
                if(i > 2)
                {
                    AddInfo(data.infos[index][i].Split(",")[0], titleFactory, trContent_info);
                } 
                AddInfo(data.infos[index][i].Split(",")[1], textFactory, trContent_info);
            }

            // Structure Scroll View의 Content 추가
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
    /// 해당 행성의 정보를 스크롤 바에 추가하는 함수
    /// </summary>
    /// <param name="infoText"> 정보 내용</param>
    /// <param name="addObject"> 텍스트 프리팹</param>
    /// <param name="tr"> 오브젝트가 추가될 부모 content </param>
    void AddInfo(string infoText, GameObject addObject, RectTransform tr)
    {
        // 0. 바뀌기 전의 Content H값을 넣자.
        prevTrContentHeight = trContent_info.sizeDelta.y;
        // 1. ChatItem을 만든다. (부모를  Scrollview의 content)
        GameObject item = Instantiate(addObject, tr);
        // 2. 만든 ChatItem에서 ChatItem 컴포넌트를 가져온다.
        KJH_DataItem data = item.GetComponent<KJH_DataItem>();
        // 3. 가져온 컴포넌트를 s에 설정
        data.SetText(infoText);

        StartCoroutine(AutoScrollBotton());
    }

    IEnumerator AutoScrollBotton()
    {
        yield return null;

        // trScrollView의 H가 Contnet의 H보다 커지면(스크롤 가능한 상태)
        if (trContent_info.sizeDelta.y > trScrollView_info.sizeDelta.y)
        {
            // 4. 만약, Content가 바닥에 닿아있었다면
            if (trContent_info.anchoredPosition.y >= prevTrContentHeight - trScrollView_info.sizeDelta.y)
            {
                // 5. Content의 y값을 다시 설정한다.
                trContent_info.anchoredPosition = new Vector2(0, trContent_info.sizeDelta.y - trScrollView_info.sizeDelta.y);
            }
        }
    }

}
