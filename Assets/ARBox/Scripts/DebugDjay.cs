using TMPro;
using UnityEngine;
public class DebugDjay
{

    static TextMeshProUGUI textMeshProUGUI = null;
    static bool hasTextMeshPro = false;

    string logs = "";

    static DebugDjay debugDjay = null;

    //private DebugDjay() { }

    public static DebugDjay GetInstance()
    {
        if (debugDjay == null)
        {
            var go = GameObject.Find("DebugLogs");
            if (go != null)
            {
                textMeshProUGUI = go.GetComponent<TextMeshProUGUI>();
                hasTextMeshPro = false;
            }
            debugDjay = new();
        }
        return debugDjay;
    }


    public void Log(object message)
    {
        if (hasTextMeshPro)
        {
            logs += "\n \n \n <color=#ffffff> " + message.ToString() + " </color>";
            textMeshProUGUI.text = logs;
        }
        else
        {
            Debug.Log("testJay: " + message);
        }
    }

    public void Error(object message)
    {
        if (hasTextMeshPro)
        {
            logs += "\n \n \n <color=#FF0000> " + message.ToString() + " </color>";
            textMeshProUGUI.text = logs;
        }
        else
        {
            Debug.LogError("testJay: " + message);
        }
    }

    public void Warning(object message)
    {
        if (hasTextMeshPro)
        {
            logs += "\n \n \n <color=#FF0000> " + message.ToString() + " </color>";
            textMeshProUGUI.text = logs;
        }
        else
        {
            Debug.LogWarning("testJay: " + message);

        }
    }

    public void LogWarning(object message)
    {
        Warning(message);
    }

    public void LogError(object message)
    {
        Error(message);
    }

}