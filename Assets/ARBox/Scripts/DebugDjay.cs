using UnityEngine;
public class DebugDjay
{
    public static void Log(object message)
    {
        Debug.Log("testJay: " + message);
    }

    public static void Error(object message)
    {
        Debug.LogError("testJay: "+message);
    }

}