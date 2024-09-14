using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SketchfabCategoryHolder : UrlTextureLoadable,InteractableObject
{
    // Start is called before the first frame update
    private CategoryModel categoryModel = null;

    [SerializeField]
    private UrlTextureLoadable urlTextureLoadable_thumbnail = null;
    [SerializeField]
    private TextMeshProUGUI textMeshPro_name = null;

    private Action<string> confirmKeyCallback;
    HighlightableItem highlightableItem;

    public void InitializeCategory(CategoryModel categoryModel, Action<string> confirmKeyCallback)
    {
        this.categoryModel = categoryModel;
        this.confirmKeyCallback = confirmKeyCallback;
        highlightableItem = GetComponent<HighlightableItem>();
    }

    public async Task LoadData()
    {
        if (categoryModel == null)
            return;
        textMeshPro_name.text = categoryModel.name;
        urlTextureLoadable_thumbnail.SetTextureURL(ImageGameobjectType.RAW_IMAGE, categoryModel.thumbnails[0].url);
        await urlTextureLoadable_thumbnail.UpdateTexture();
    }

    public void ConfirmKeyClicked()
    {
        confirmKeyCallback?.Invoke(categoryModel.slug);
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
