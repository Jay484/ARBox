using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public static class ObjectRepo
{
    public static async Task<GLBObject> GetGlbData(string glbJsonPath)
    {
        DebugDjay.Log("url: " + glbJsonPath);
        var downloadHandler =  await HTTPRequest.SendGetRequestAsync(glbJsonPath);
        byte[] jsonData = downloadHandler.data;

        // Convert the byte array to a string
        string jsonGlbData = System.Text.Encoding.UTF8.GetString(jsonData);

        DebugDjay.Log(jsonGlbData);
        var glbData = JsonUtility.FromJson<GLBObject>(jsonGlbData);
        return glbData;
    }

    public static async Task<List<GLBModelData>> GetGLBObjectsMetaData(
        string url,
        int page
    )
    {
        await Task.Delay(1000);
        List<GLBModelData> metaDataArray = new();
        for(int i =0; i< 15; i++)
        {
            metaDataArray.Add(new GLBModelData("SolarSystem"));
        }
        return metaDataArray;
    }

}
