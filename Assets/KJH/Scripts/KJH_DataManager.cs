using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


// KJH_InfoData에서 읽어온 데이터를 스크롤 바에 반영하고 싶다.
public class KJH_DataManager : MonoBehaviour
{
    // 선택한 천체 관련 스크립트
    KJH_SelectPlanet selectPlanet;
    [Header("CB Info")]
    // 천체이름
    public Text CB_Name;
    // 천체종류
    public Text CB_Type;

    /* 정보 내용 */
    // 읽어온 정보 데이터
    KJH_Data infoData;
    
    // 정보 제목
    public GameObject infoTitleFactory;
    // 정보 내용
    public GameObject infoTextFactory;
    // ScrollView의 Content
    public RectTransform trContent;
    // ScrollView의 RectTransform
    public RectTransform trScrollView;
    // 이전 Content의 높이 H
    float prevTrContentHeight;

    /* 정보 상세 내용 */
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

            // Grid Layout Group에 추가
            for (int i = 0; i < infoData.detailInfos[index].Count; i++)
            {
                AddInfo(infoData.detailInfos[index][i].Split(",")[0], detailInfoFactory);
                AddInfo(infoData.detailInfos[index][i].Split(",")[1], detailInfoFactory);
            }


            // Scroll View의 Content 추가
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

    // 행성 클릭 됬을 때 호출되는 함수...
    // 해당 행성의 정보를 전달 받아 
    void AddInfo(string infoText, GameObject addObject)
    {
        // 0. 바뀌기 전의 Content H값을 넣자.
        prevTrContentHeight = trContent.sizeDelta.y;
        // 1. ChatItem을 만든다. (부모를  Scrollview의 content)
        GameObject item = Instantiate(addObject, trContent);
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
        if (trContent.sizeDelta.y > trScrollView.sizeDelta.y)
        {
            // 4. 만약, Content가 바닥에 닿아있었다면
            if (trContent.anchoredPosition.y >= prevTrContentHeight - trScrollView.sizeDelta.y)
            {
                // 5. Content의 y값을 다시 설정한다.
                trContent.anchoredPosition = new Vector2(0, trContent.sizeDelta.y - trScrollView.sizeDelta.y);
            }
        }

    }
}
