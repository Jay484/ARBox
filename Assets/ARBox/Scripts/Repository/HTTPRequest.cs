using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class HTTPRequest
{


    public static async Task<DownloadHandler> SendGetRequestAsync(string url, Parameters parameters =null, Headers headers = null)
    {
        // Create a UnityWebRequest object to send the GET request
        UriBuilder uriBuilder = new(url);
        if (parameters != null)
        {
            foreach (var keypair in parameters.queryParams)
            {
                uriBuilder.Query += $"{keypair.Key}={keypair.Value}&";
            }
        }

        UnityWebRequest request = UnityWebRequest.Get(uriBuilder.Uri);

        if (headers != null)
        {
            foreach (var keypair in headers.headers)
            {
                request.SetRequestHeader(keypair.Key, keypair.Value);
            }
        }

        DebugDjay.GetInstance().Error(request.url);
        // Send the request asynchronously and wait for the response
        var operation = request.SendWebRequest();
        while (!operation.isDone)
        {
            await Task.Yield();
        }

        // Check for errors
        if (request.result == UnityWebRequest.Result.ConnectionError ||
            request.result == UnityWebRequest.Result.ProtocolError)
        {
            DebugDjay.GetInstance().LogError("Error: " + request.error);
            return null;
        }
        else
        {
            return request.downloadHandler;
        }
    }

    public static async Task<string> DownloadAndExtractZip(string downloadPath,string fileName, string url, Parameters parameters = null, Headers headers = null)
    {
        var downloadHandler = await SendGetRequestAsync(url, parameters, headers);
        if(downloadHandler != null)
        {
            if(!Directory.Exists(downloadPath))
            {
                Directory.CreateDirectory(downloadPath);
            }
            var extractedFolderPath = downloadPath + "/" + fileName;
            if (Directory.Exists(extractedFolderPath))
            {
                return extractedFolderPath;
            }
            Directory.CreateDirectory(extractedFolderPath);
            var zipFilePath = extractedFolderPath + ".zip";
            File.WriteAllBytes(zipFilePath, downloadHandler.data);
            System.IO.Compression.ZipFile.ExtractToDirectory(zipFilePath, extractedFolderPath);
            File.Delete(zipFilePath);
            return extractedFolderPath;
        }
        else
        {
            return null;
        }
    }


    public static async Task<DownloadHandlerTexture> GetTexture(string url)
    {
        // Create a UnityWebRequest object to send the GET request
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);

        // Send the request asynchronously and wait for the response
        var operation = request.SendWebRequest();
        while (!operation.isDone)
        {
            await Task.Yield();
        }

        // Check for errors
        if (request.result == UnityWebRequest.Result.ConnectionError ||
            request.result == UnityWebRequest.Result.ProtocolError)
        {
            DebugDjay.GetInstance().LogError("Error: " + request.error);
            return null;
        }
        else
        {
            return (DownloadHandlerTexture)request.downloadHandler;
        }
    }

    public class Headers{
        public Dictionary<string, string> headers = new();

        public Headers(Dictionary<string, string> dict)
        {
            headers = dict;
        }
    }

    public class Parameters
    {
        public Dictionary<string,string> queryParams = new();

        public Parameters(Dictionary<string,string> dict)
        {
            queryParams = dict;
        }
    }

}
