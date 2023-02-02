using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class KJH_CSVReader
{
    //static string SPLIT_RE = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";
    static string COL_SPLIT_RE = @",";
    static string ROW_SPLIT_RE = @"\r\n|\n\r|\n|\r";
    static char[] TRIM_CHARS = { '\"' };

    public static List<Dictionary<string, object>> Read(string file)
    {
        var list = new List<Dictionary<string, object>>();
        TextAsset data = Resources.Load(file) as TextAsset;

        /* �� ������ */
        var lines = Regex.Split(data.text, ROW_SPLIT_RE);
        if (lines.Length <= 1) return list;

        /* �� ������ */
        // 0�� : �� ���� �и�
        var header = Regex.Split(lines[0], COL_SPLIT_RE);
        // 1��~ : �� ���� �и�
        for (var i = 1; i < lines.Length; i++)
        {
            var values = Regex.Split(lines[i], COL_SPLIT_RE);
            if (values.Length == 0 || values[0] == "") continue;

            // <�� ���, ��>
            var entry = new Dictionary<string, object>();
            for (var j = 0; j < header.Length && j < values.Length; j++)
            {
                string value = values[j];
                // �� �� ū����ǥ�� �������� ����
                value = value.TrimStart(TRIM_CHARS).TrimEnd(TRIM_CHARS).Replace("\\", "");
                object finalvalue = value;
                int n;
                float f;

                // �� ��ȯ
                if (int.TryParse(value, out n))
                    finalvalue = n;
                else if (float.TryParse(value, out f))
                    finalvalue = f;
                entry[header[j]] = finalvalue;
            }
            list.Add(entry);
        }
        return list;
    }
}