using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class BottomMenuItem : UrlTextureLoadable
{
    GLBModelData modelData;
    private bool isHighlighted = false;


    public float scaleFactor = 1.3f; // 10% increase
    public float highlighPositionZOffset = 0.01f;
    public float animationDuration = .1f; // Duration of the animation in seconds
    private Vector3 normalScale;
    private Vector3 highlightScale;
    private Vector3 normalLocalPosition;
    private Vector3 highlighLocalPosition;

    void Start()
    {
        normalScale = transform.localScale;
        highlightScale = normalScale * scaleFactor;
        normalLocalPosition = transform.localPosition;
        highlighLocalPosition = new Vector3(normalLocalPosition.x, normalLocalPosition.y, normalLocalPosition.z - highlighPositionZOffset);
    }

    public void SetModelData(GLBModelData modelData)
    {
        this.modelData = modelData;
        SetTextureURL(modelData.GetGLBThumbnailPath());
    }

    public GLBModelData GetGlbModelData()
    {
        return modelData;
    }

    public new async Task UpdateTexture()
    {
        await base.UpdateTexture();
    }

    public void Highlight()
    {
        if (isHighlighted)
            return;
        DebugDjay.Error("HighLight");
        isHighlighted = true;
        transform.localPosition = highlighLocalPosition ;
        AnimateScale(normalScale, highlightScale);
    }

    public void UnHighlight()
    {
        if (!isHighlighted)
            return;
        DebugDjay.Error("UnHighLight");
        isHighlighted = false;
        transform.localPosition = normalLocalPosition;
        AnimateScale(highlightScale, normalScale);
    }


    async void AnimateScale(Vector3 originalScale, Vector3 targetScale){
        float timer = 0f;
        while (timer < animationDuration)
        {
            // Interpolate the scale over time
            transform.localScale = Vector3.Lerp(originalScale, targetScale, timer / animationDuration);
            timer += Time.deltaTime;
            await Task.Yield(); // Yield to allow other async tasks to run
        }

        // Ensure that the object reaches the target scale exactly
        transform.localScale = targetScale;
    }


}
