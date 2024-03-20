using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class HTTPRequest
{


    public static async Task<DownloadHandler> SendGetRequestAsync(string url)
    {
        // Create a UnityWebRequest object to send the GET request
        UnityWebRequest request = UnityWebRequest.Get(url);

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
            Debug.LogError("Error: " + request.error);
            return null;
        }
        else
        {
            return request.downloadHandler;
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
            Debug.LogError("Error: " + request.error);
            return null;
        }
        else
        {
            return (DownloadHandlerTexture) request.downloadHandler;
        }
    }

}
