using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// �༺�� �̸��� �°� 
public class KJH_CSVTest : MonoBehaviour
{
    // ������� -> ��������:����:�Ÿ�:�츮���Ͽ����� �¾�
    Dictionary<string, string> dict_infoList;
    // �����󼼸�� -> ��������:����:���� �߽ɱ����� �Ÿ�:�����ֱ�:���� �����ֱ�:ǥ�� �߷�: ǥ�� �µ�:����
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

            //print(i + " : " + data[i]["õü�̸�"]);
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