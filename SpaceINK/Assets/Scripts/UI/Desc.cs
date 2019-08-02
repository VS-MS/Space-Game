using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Desc
{
    public long value;
    public string shortName;
    public Desc(long v, string s)
    {
        value = v;
        shortName = s;
    }
}

public class Desc_
{
    //структура для убирания нулей у больших цифр
    private readonly Desc[] allDesc = new Desc[]
    {
            new Desc(1000000000000, "T"),
            new Desc(1000000000,    "B"),
            new Desc(1000000,       "M"),
            new Desc(1000,          "K")
    };

    //процедура для убирания нулей у больших цифр
    public string ShortNumber(long value)
    {
        foreach (var d in allDesc)
            if (value > d.value)
            {
                return $"{value / (float)d.value:F2} {d.shortName}";
            }    
        return $"{value}";
    }
}
