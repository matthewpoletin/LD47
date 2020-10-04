﻿using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public static class CsvReader
{
    private const string SPLIT_RE = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";
    private const string LINE_SPLIT_RE = @"\r\n|\n\r|\n|\r";
    private static readonly char[] TRIM_CHARS = {'\"'};

    public static List<Dictionary<string, object>> Read(string file)
    {
        var data = Resources.Load(file) as TextAsset;
        return Read(data);
    }

    public static List<Dictionary<string, object>> Read(TextAsset data)
    {
        var list = new List<Dictionary<string, object>>();

        var lines = Regex.Split(data.text, LINE_SPLIT_RE);

        if (lines.Length <= 1) return list;

        var header = Regex.Split(lines[0], SPLIT_RE);
        for (var i = 1; i < lines.Length; i++)
        {
            var values = Regex.Split(lines[i], SPLIT_RE);
            if (values.Length == 0 || values[0] == "")
            {
                continue;
            }

            var entry = new Dictionary<string, object>();
            for (var j = 0; j < header.Length && j < values.Length; j++)
            {
                var value = values[j];
                value = value.TrimStart(TRIM_CHARS).TrimEnd(TRIM_CHARS).Replace("\\", "");
                object finalValue = value;
                if (int.TryParse(value, out var n))
                {
                    finalValue = n;
                }
                else if (float.TryParse(value, out var f))
                {
                    finalValue = f;
                }

                entry[header[j]] = finalValue;
            }

            list.Add(entry);
        }

        return list;
    }
}