using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleARViewScript : MonoBehaviour
{

    [SerializeField]
    Camera ArCamera;

    [SerializeField]
    RenderTexture ArCameraRenderTexture;

    [SerializeField]
    GameObject ArBox;

    public ARView aRView = ARView.ARBoxView;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleARView()
    {
        if(aRView == ARView.ARBoxView) {
            aRView = ARView.SingleScreenView;
            SetSingleScreenView();
        }
        else
        {
            aRView = ARView.ARBoxView;
            SetARBoxView();
        }
    }

    private void SetARBoxView()
    {
        ArCamera.targetTexture = ArCameraRenderTexture;
        ArBox.SetActive(true);
    }

    private void SetSingleScreenView()
    {
        ArCamera.targetTexture = null;
        ArBox.SetActive(false);
    }
}

public enum ARView
{
    SingleScreenView,
    ARBoxView
}