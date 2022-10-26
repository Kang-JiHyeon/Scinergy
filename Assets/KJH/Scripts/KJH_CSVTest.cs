using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// 행성의 이름에 맞게 
public class KJH_CSVTest : MonoBehaviour
{
    // 천체이름
    public Text CBName;
    // 천체종류
    public Text CBType;
    
    // 천체 정보보기 를 제외한 정보 내용 리스트
    // index 0 = 태양, 1=수성, 2=금성 ...
    List<List<string>> infos;

    // 행성 정보보기 내용 리스트
    // {{"적도지름,값", "질량,값", ...}, {}}
    List<List<string>> detailInfos;


    void Awake()
    {
        infos = new List<List<string>>();
        detailInfos = new List<List<string>>();

        List<Dictionary<string, object>> data = KJH_CSVReader.Read("Data1");

        for (int i = 0; i < data.Count; i++)
        {
            string[] infoTitles = ((string)data[i]["정보목록"]).Split(":");
            string[] detailInfoTitles = ((string)data[i]["정보상세목록"]).Split(":");
            
            // 읽어온 data에서 value값만 가져오기
            List<object> _values = new List<object>(data[i].Values);
            List<string> _infos = new List<string>();
            List<string> _detailInfos = new List<string>();

            _infos.Add("천체이름," + (string)data[i]["천체이름"]);
            _infos.Add("천체종류," + (string)data[i]["천체종류"]);

            // 천체 정보보기를 제외한 큰 정보값
            for (int j = 1; j < infoTitles.Length; j++)
            {
                string s = infoTitles[j] + "," + _values[j + 13];
                _infos.Add(s);
            }

            // 천체 정보보기 정보값(상세보기)
            for (int k = 0; k < detailInfoTitles.Length; k++)
            {
                string s = detailInfoTitles[k] + "," + _values[k + 4];
                _detailInfos.Add(s);
            }

            infos.Add(_infos);
            detailInfos.Add(_detailInfos);
        }
    }

    // Use this for initialization
    void Start()
    {
        string[] str1 = infos[0][0].Split(",");
        string[] str2 = infos[0][1].Split(",");
        CBName.text = str1[1];
        CBType.text = str2[1];

    }

    // Update is called once per frame
    void Update()
    {

    }
}