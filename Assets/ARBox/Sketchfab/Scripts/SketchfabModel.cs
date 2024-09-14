using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class SketchfabModelHolder : MonoBehaviour,InteractableObject
{
    private ModelModel modelModel = null;


    [SerializeField]
    private UrlTextureLoadable thumbnail = null;

    [SerializeField]
    private TextMeshProUGUI modelName = null;

    [SerializeField]
    private TextMeshProUGUI userName = null;

    [SerializeField]
    private UrlTextureLoadable userProfile = null;

    HighlightableItem highlightableItem;
    Action<ModelModel> OnModelSelected;

    public void InitializeModel(ModelModel modelModel, Action<ModelModel> OnModelSelected)
    {
        this.modelModel = modelModel;
        this.OnModelSelected = OnModelSelected;
        highlightableItem = GetComponent<HighlightableItem>();
    }

    public async Task LoadData()
    {
        if (modelModel == null)
            return;
        modelName.text = modelModel.name;
        userName.text = modelModel.user.displayName;
        thumbnail.SetTextureURL(UrlTextureLoadable.ImageGameobjectType.RAW_IMAGE, modelModel.GetThumbnail().url);
        await thumbnail.UpdateTexture();
    }

    public void ResetModelHolder()
    {

    }

    public void ConfirmKeyClicked()
    {
        OnModelSelected.Invoke(modelModel);
        DebugDjay.GetInstance().Error("Model selected model");

    }

    public void RayStartedHitting()
    {
        if (highlightableItem != null)
        {
            highlightableItem.Highlight();

        }
    }

    public void RayStoppedHitting()
    {
        if (highlightableItem != null)
        {
            highlightableItem.UnHighlight();
        }
    }

    public bool IsHighlighted()
    {
        return highlightableItem.isHighlighted;
    }
}
