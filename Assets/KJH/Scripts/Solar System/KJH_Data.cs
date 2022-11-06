using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class KJH_CSVReader
{
    //static string SPLIT_RE = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";
    static string SPLIT_RE = @",";
    static string LINE_SPLIT_RE = @"\r\n|\n\r|\n|\r";
    static char[] TRIM_CHARS = { '\"' };

    public static List<Dictionary<string, object>> Read(string file)
    {
        var list = new List<Dictionary<string, object>>();
        TextAsset data = Resources.Load(file) as TextAsset;

        var lines = Regex.Split(data.text, LINE_SPLIT_RE);

        if (lines.Length <= 1) return list;

        var header = Regex.Split(lines[0], SPLIT_RE);
        for (var i = 1; i < lines.Length; i++)
        {
            var values = Regex.Split(lines[i], SPLIT_RE);
            if (values.Length == 0 || values[0] == "") continue;

            var entry = new Dictionary<string, object>();
            for (var j = 0; j < header.Length && j < values.Length; j++)
            {
                string value = values[j];
                value = value.TrimStart(TRIM_CHARS).TrimEnd(TRIM_CHARS).Replace("\\", "");
                object finalvalue = value;
                int n;
                float f;

                if (int.TryParse(value, out n))
                {
                    finalvalue = n;
                }
                else if (float.TryParse(value, out f))
                {
                    finalvalue = f;
                }
                entry[header[j]] = finalvalue;
            }
            list.Add(entry);
        }

        return list;
    }
}

// �༺�� �̸��� �°� 
public class KJH_Data : MonoBehaviour
{
    public List<string> cbNames;

    // õü �������� �� ������ ���� ���� ����Ʈ
    // index 0 = �¾�, 1=����, 2=�ݼ� ...
    public List<List<string>> infos;

    // �༺ �������� ���� ����Ʈ
    // {{"��������,��", "����,��", ...}, {}}
    public List<List<string>> detailInfos;

    // ���α���
    public List<List<string>> strucInfos;


    void Awake()
    {
        cbNames = new List<string>();
        infos = new List<List<string>>();
        detailInfos = new List<List<string>>();
        strucInfos = new List<List<string>>();

        List<Dictionary<string, object>> infoData = KJH_CSVReader.Read("InfoData");
        List<Dictionary<string, object>> structureData = KJH_CSVReader.Read("StructureData");


        // infoData Read
        for (int i = 0; i < infoData.Count; i++)
        {
            string[] _infoList = ((string)infoData[i]["�������"]).Split(":");
            string[] _detailInfoTitles = ((string)infoData[i]["��������󼼸��"]).Split(":");
            
            // �о�� data���� value���� ��������
            List<object> _values = new List<object>(infoData[i].Values);
            List<string> _infos = new List<string>();
            List<string> _detailInfos = new List<string>();

            cbNames.Add((string)infoData[i]["������Ʈ�̸�"]);
            _infos.Add((string)infoData[i]["õü�̸�"]);
            _infos.Add((string)infoData[i]["õü����"]);

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

        // structureData read
        for(int i=0; i<structureData.Count; i++)
        {
            string[] _strucList = ((string)structureData[i]["���α������"]).Split(":");

            List<object> _values = new List<object>(structureData[i].Values);

            List<string> _strucInfos = new List<string>();

            // ���α��� ����
            for (int j = 0; j < _strucList.Length; j++)
            {
                string s = _strucList[j] + "," + _values[j + 4];
                _strucInfos.Add(s);
            }

            strucInfos.Add(_strucInfos);
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