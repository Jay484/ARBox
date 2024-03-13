using UnityEngine;
public class DebugDjay
{
    public static void Log(string message)
    {
        Debug.Log("testJay: " + message);
    }

    public static void Error(string message)
    {
        Debug.LogError("testJay: "+message);
    }

}