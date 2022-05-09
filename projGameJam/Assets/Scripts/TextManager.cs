using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
class TextManager
{
    public static void SetText(GameObject Object, string text)
    {
        Object.GetComponent<Text>().text = text;
    }

    public static void SetText(Text Object, int text)
    {
        Object.text = text.ToString();
    }

    public static void SetColor(GameObject Object, Color color)
    {
        Object.GetComponent<Text>().color = color;
    }
}
