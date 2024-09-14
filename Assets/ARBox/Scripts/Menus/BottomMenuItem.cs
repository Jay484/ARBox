using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class BottomMenuItem : UrlTextureLoadable
{
    GLBModelData modelData;
    public void SetModelData(GLBModelData modelData)
    {
        this.modelData = modelData;
        SetTextureURL(ImageGameobjectType.GAMEOBJECT, modelData.GetGLBThumbnailPath());
    }

    public GLBModelData GetGlbModelData()
    {
        return modelData;
    }

    public new async Task UpdateTexture()
    {
        await base.UpdateTexture();
    }

}
