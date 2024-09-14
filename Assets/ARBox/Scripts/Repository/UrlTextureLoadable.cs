using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class UrlTextureLoadable : MonoBehaviour
{
    public string TextureURL = "";
    public bool loadTexture = false;
    public bool isLoading = false;

    private ImageGameobjectType type = ImageGameobjectType.NONE;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //if (loadTexture)
        //{
        //    loadTexture = false;
        //    StartCoroutine(LoadImage());
        //}
    }

    public void SetTextureURL(ImageGameobjectType type, string url)
    {
        TextureURL = url;
        this.type = type;
    }

    public async Task UpdateTexture()
    {
        loadTexture = true;
        await LoadImage();
    }

    private async Task LoadImage()
    {
        isLoading = true;
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(TextureURL);
        var operation = request.SendWebRequest();
        while (!operation.isDone)
            await Task.Yield();
        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            DebugDjay.GetInstance().Log(request.error);
        else
        {
            Texture2D texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
            if (texture != null)
            {
                switch (type)
                {
                    case ImageGameobjectType.GAMEOBJECT:
                        LoadGameobjectTexture(texture);
                        break;
                    case ImageGameobjectType.RAW_IMAGE:
                        LoadRawImageTexture(texture);
                        break;
                    default:
                        break;

                }

                

            }
        }
        isLoading = false;
    }

    void LoadGameobjectTexture(Texture2D texture)
    {
        Renderer renderer = gameObject.GetComponent<Renderer>();
        Material material = renderer.material;
        texture.filterMode = FilterMode.Trilinear;
        texture.Apply(true);
        material.mainTexture = texture;


        // Calculate aspect ratio of texture and renderer
        float textureAspectRatio = (float)texture.width / texture.height;
        float rendererAspectRatio = renderer.bounds.size.x / renderer.bounds.size.y;

        // Calculate scale factors for texture coordinates (UVs)
        float scaleX = 1f;
        float scaleY = 1f;

        if (textureAspectRatio > rendererAspectRatio)
        {
            // Texture is wider than the renderer
            scaleX = rendererAspectRatio / textureAspectRatio;
        }
        else
        {
            // Texture is taller than the renderer
            scaleY = textureAspectRatio / rendererAspectRatio;
        }

        material.mainTextureScale = new Vector2(scaleX, scaleY);
    }

    void LoadRawImageTexture(Texture2D texture)
    {
        GetComponent<RawImage>().texture = texture;
    }

    public enum ImageGameobjectType
    {
        SPRITE_IMAGE,
        RAW_IMAGE,
        GAMEOBJECT,
        NONE
    }
}