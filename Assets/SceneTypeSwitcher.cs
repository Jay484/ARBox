using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneTypeSwitcher : MonoBehaviour
{
    [SerializeField]
    Toggle haveArGlasses;

    [SerializeField]
    Camera mainCamera;

    [SerializeField]
    Camera arboxCamera;

    [SerializeField]
    GameObject Eyes;

    [SerializeField]
    RenderTexture eyeViewRenderTexture;


    public void OnToggleValueChanged()
    {
        if (haveArGlasses.isOn)
        {
            SetSplitEyeView();
        }
        else
        {
            SetSingleScreenView();
        }
    }

    private void SetSplitEyeView()
    {
        Eyes.SetActive(true);
        mainCamera.targetTexture = eyeViewRenderTexture;
        arboxCamera.enabled = true;
    }

    private void SetSingleScreenView()
    {
        Eyes.SetActive(false);
        mainCamera.targetDisplay = 0;
        mainCamera.targetTexture = null;
        arboxCamera.enabled = false;
    }
}
