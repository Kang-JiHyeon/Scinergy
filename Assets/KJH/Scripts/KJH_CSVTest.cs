using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// �༺�� �̸��� �°� 
public class KJH_CSVTest : MonoBehaviour
{
    // õü�̸�
    public Text CBName;
    // õü����
    public Text CBType;
    
    // õü �������� �� ������ ���� ���� ����Ʈ
    // index 0 = �¾�, 1=����, 2=�ݼ� ...
    List<List<string>> infos;

    // �༺ �������� ���� ����Ʈ
    // {{"��������,��", "����,��", ...}, {}}
    List<List<string>> detailInfos;


    void Awake()
    {
        infos = new List<List<string>>();
        detailInfos = new List<List<string>>();

        List<Dictionary<string, object>> data = KJH_CSVReader.Read("Data1");

        for (int i = 0; i < data.Count; i++)
        {
            string[] infoTitles = ((string)data[i]["�������"]).Split(":");
            string[] detailInfoTitles = ((string)data[i]["�����󼼸��"]).Split(":");
            
            // �о�� data���� value���� ��������
            List<object> _values = new List<object>(data[i].Values);
            List<string> _infos = new List<string>();
            List<string> _detailInfos = new List<string>();

            _infos.Add("õü�̸�," + (string)data[i]["õü�̸�"]);
            _infos.Add("õü����," + (string)data[i]["õü����"]);

            // õü �������⸦ ������ ū ������
            for (int j = 1; j < infoTitles.Length; j++)
            {
                string s = infoTitles[j] + "," + _values[j + 13];
                _infos.Add(s);
            }

            // õü �������� ������(�󼼺���)
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