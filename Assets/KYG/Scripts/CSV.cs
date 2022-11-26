using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using UnityEngine;

public class CSV : MonoBehaviour
{
    public static CSV instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    public List<T> Parsing<T>(string fileName) where T : new()
    {
        List<T> list = new();
        string path = Application.streamingAssetsPath + "/" + fileName + ".csv";
        //파일 열자
        byte[] byteData = File.ReadAllBytes(path);
        string stringData = Encoding.GetEncoding("euc-kr").GetString(byteData);
        //string stringData = File.ReadAllText(path);

        //엔터(\n) 기준으로 한줄씩 자르기
        string[] lines = stringData.Split("\n");
        //\r을 제거
        for (int i = 0; i < lines.Length; i++)
        {
            lines[i] = lines[i].Replace("\r", "");
        }
        //변수 나누기
        string[] variable = lines[0].Split(",");
        //값 나누기
        for (int i = 1; i < lines.Length; i++)
        {
            //만약 값이 없다면(길이가 0이라면)
            if (lines[i].Length == 0) continue;
            string[] value = lines[i].Split(",");
            //정보 만들기
            T data = new();
            for (int j = 0; j < variable.Length; j++)
            {
                //variable[0] = "name", variable[1] = "phone", variable [2] = "age"
                //해당 이름으로 되어있는 변수의 정보를 얻어오자
                System.Reflection.FieldInfo fieldInfo = typeof(T).GetField(variable[j]);
                //int.Parse, float.Parse... 무엇을 할지 결정해놓다
                TypeConverter typeConverter = TypeDescriptor.GetConverter(fieldInfo.FieldType);
                //값을 셋팅
                fieldInfo.SetValue(data, typeConverter.ConvertFrom(value[j]));
            }
            //정보 추가
            list.Add(data);
        }
        return list;
    }
}
