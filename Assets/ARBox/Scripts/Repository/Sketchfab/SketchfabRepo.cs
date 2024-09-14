using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class SketchfabRepo
{
    public static async Task<List<CategoryModel>> GetCategories()
    {
        var categoriesUrl =  base_url +  "/categories";
        var downloadHandler = await HTTPRequest.SendGetRequestAsync(categoriesUrl);
        var data = JsonUtility.FromJson<CategoriesResponse>(downloadHandler.text);
        return data.results;
    }


    public static async Task<ModelsResponse> GetModels(string categorySlug,string cursor)
    {
        var modelsUrl = base_url + "/models";
        Dictionary<string, string> queryParams = new();
        if(cursor != null)
        {
            queryParams.Add("cursor", cursor);
        }
        queryParams.Add("downloadable", "true");
        queryParams.Add("categories", categorySlug);
        var downloadHandler = await HTTPRequest.SendGetRequestAsync(modelsUrl,new HTTPRequest.Parameters(queryParams));
        var data = JsonUtility.FromJson<ModelsResponse>(downloadHandler.text);
        return data;
    }

    public static async Task<ModelDownloadInfoModel> GetModelDownloadInfo(string modelUri)
    {
        var downloadUrl = base_url + $"/models/{modelUri}/download";
        Dictionary<string, string> headers = new();
        headers.Add("Authorization", $"Token {token}");
        var downloadHandler = await HTTPRequest.SendGetRequestAsync(downloadUrl, null, new HTTPRequest.Headers(headers));
        //DebugDjay.GetInstance().Error(downloadHandler.text);
        if(downloadHandler == null)
            return null;
        var data = JsonUtility.FromJson<ModelDownloadInfoModel>(downloadHandler.text);
        return data;
    }

    public static async Task<string> GetGLTFFilePath(string modelUri)
    {
        var modelDownloadInfo = await GetModelDownloadInfo(modelUri);
        if (modelDownloadInfo == null)
            return null;
        var srcUrl = modelDownloadInfo.gltf.url;
        var fileName = getZipFileNameFromUrl(srcUrl);
        var extractedFolder = await HTTPRequest.DownloadAndExtractZip(localDataPath, fileName, srcUrl);
        if (extractedFolder == null)
            return null;
        return "file://"+extractedFolder + "/" + "scene.gltf";
    }

    public static string localDataPath = Application.persistentDataPath + "/" + "SketchfabData";
    public static string base_url = "https://api.sketchfab.com/v3";
    public static string token = "4ce1c24e8e004b2e96daaed3aa4e4734";

    public static string getZipFileNameFromUrl(string url)
    {
        var path = url.Split('?')[0];
        var paths = path.Split('/');
        var filename = paths[paths.Length - 1].Split('.')[0];
        return filename;
    }
}
