using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// 행성의 이름에 맞게 
public class KJH_CSVTest : MonoBehaviour
{
    // 정보목록 -> 정보보기:구성:거리:우리은하에서의 태양
    Dictionary<string, string> dict_infoList;
    // 정보상세목록 -> 적도지름:질량:은하 중심까지의 거리:자전주기:은하 공전주기:표면 중력: 표면 온도:내용
    Dictionary<string, string> dict_infoDetailList;


    void Awake()
    {
        List<Dictionary<string, object>> data = KJH_CSVReader.Read("Data1");

        for (var i = 0; i < data.Count; i++)
        {
            //print("name " + data[i]["name"] + " " +
            //       "age " + data[i]["age"] + " " +
            //       "speed " + data[i]["speed"] + " " +
            //       "desc " + data[i]["description"]);

            //print(i + " : " + data[i]["천체이름"]);
        }
    }

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
}