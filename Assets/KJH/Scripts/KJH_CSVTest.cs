using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// �༺�� �̸��� �°� 
public class KJH_CSVTest : MonoBehaviour
{
    //public List<string> cbNames = new List<string> { "SunModel", "MercuryModel", "VenusModel", "EarthModel", "MarsModel", "JupiterModel", "SaturnModel", "UranusModel", "NeptuneModel" };
    public List<string> cbNames;

    // õü �������� �� ������ ���� ���� ����Ʈ
    // index 0 = �¾�, 1=����, 2=�ݼ� ...
    public List<List<string>> infos;

    // �༺ �������� ���� ����Ʈ
    // {{"��������,��", "����,��", ...}, {}}
    public List<List<string>> detailInfos;


    void Awake()
    {
        cbNames = new List<string>();
        infos = new List<List<string>>();
        detailInfos = new List<List<string>>();

        List<Dictionary<string, object>> data = KJH_CSVReader.Read("Data");

        for (int i = 0; i < data.Count; i++)
        {
            string[] _infoList = ((string)data[i]["�������"]).Split(":");
            string[] _detailInfoTitles = ((string)data[i]["��������󼼸��"]).Split(":");
            
            // �о�� data���� value���� ��������
            List<object> _values = new List<object>(data[i].Values);
            List<string> _infos = new List<string>();
            List<string> _detailInfos = new List<string>();

            cbNames.Add((string)data[i]["������Ʈ�̸�"]);
            _infos.Add((string)data[i]["õü�̸�"]);
            _infos.Add((string)data[i]["õü����"]);

            // �������� ����
            for (int j = 0; j < _infoList.Length; j++)
            {
                string s = _infoList[j] + "," + _values[j + 5];
                _infos.Add(s);
            }

            // ��������󼼸�� ����������(�󼼺���)
            for (int k = 0; k < _detailInfoTitles.Length; k++)
            {
                string s = _detailInfoTitles[k] + "," + _values[k + 15];
                _detailInfos.Add(s);
            }

            infos.Add(_infos);
            detailInfos.Add(_detailInfos);
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