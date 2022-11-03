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

// 행성의 이름에 맞게 
public class KJH_Data : MonoBehaviour
{
    public List<string> cbNames;

    // 천체 정보보기 를 제외한 정보 내용 리스트
    // index 0 = 태양, 1=수성, 2=금성 ...
    public List<List<string>> infos;

    // 행성 정보보기 내용 리스트
    // {{"적도지름,값", "질량,값", ...}, {}}
    public List<List<string>> detailInfos;

    // 내부구조
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
            string[] _infoList = ((string)infoData[i]["정보목록"]).Split(":");
            string[] _detailInfoTitles = ((string)infoData[i]["정보보기상세목록"]).Split(":");
            
            // 읽어온 data에서 value값만 가져오기
            List<object> _values = new List<object>(infoData[i].Values);
            List<string> _infos = new List<string>();
            List<string> _detailInfos = new List<string>();

            cbNames.Add((string)infoData[i]["오브젝트이름"]);
            _infos.Add((string)infoData[i]["천체이름"]);
            _infos.Add((string)infoData[i]["천체종류"]);

            // 정보보기 내용
            for (int j = 0; j < _infoList.Length; j++)
            {
                string s = _infoList[j] + "," + _values[j + 5];
                _infos.Add(s);
            }

            // 정보보기상세목록 내용정보값(상세보기)
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
            string[] _strucList = ((string)structureData[i]["내부구조목록"]).Split(":");

            List<object> _values = new List<object>(structureData[i].Values);

            List<string> _strucInfos = new List<string>();

            // 내부구조 내용
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