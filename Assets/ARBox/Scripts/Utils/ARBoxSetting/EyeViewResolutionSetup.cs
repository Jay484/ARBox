using UnityEngine;
using UnityEngine.UI;

public class EyeViewResolutionSetup : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    GameObject leftEye;
    [SerializeField]
    GameObject rightEye;
    [SerializeField]
    RenderTexture eyeVieweRenderTexture;

    [SerializeField]
    Slider senstivitySlider;
    [SerializeField]
    Slider scaleSlider;


    private Display mainDisplay;
    private Vector3 leftEyePosition;
    private Vector3 rightEyePosition;

    [SerializeField]
    private float senstivity;

    [SerializeField]
    private float widthScale;
    public float heightScale;

    void Start()
    {
        
    }

    private void OnEnable()
    {
        //DebugDjay.GetInstance().Log(Display.main.systemHeight);
        //DebugDjay.GetInstance().Log(Display.main.systemWidth);
        mainDisplay = Display.main;
        InitializeEyeViewRenderTexture();
        InitializeEyeViewScale();
        leftEyePosition = leftEye.transform.localPosition;
        rightEyePosition = rightEye.transform.localPosition;
    }

    public void MoveEyesLeft() {
        leftEyePosition.x -= senstivity;
        rightEyePosition.x -= senstivity;
        UpdateEyeViewPosition();

    }

    public void MoveEyesRight() {
        leftEyePosition.x += senstivity;
        rightEyePosition.x += senstivity;
        UpdateEyeViewPosition();
    }

    public void MoveEyesAway(){
        leftEyePosition.x -= senstivity;
        rightEyePosition.x += senstivity;
        UpdateEyeViewPosition();
    }

    public void MoveEyesClose()
    {
        leftEyePosition.x += senstivity;
        rightEyePosition.x -= senstivity;
        UpdateEyeViewPosition();
    }

    public void UpdateSenstivity()
    {
        senstivity = senstivitySlider.value / 50f;
    }

    public void UpdateScale()
    {
        widthScale = scaleSlider.value/10f;
        heightScale = widthScale * (float)(mainDisplay.systemHeight / (float)mainDisplay.systemWidth);
        leftEye.transform.localScale = new Vector3(widthScale, .1f, heightScale);
        rightEye.transform.localScale = new Vector3(widthScale, .1f, heightScale);

    }

    private void UpdateEyeViewPosition()
    {
        leftEye.transform.localPosition = leftEyePosition;
        rightEye.transform.localPosition = rightEyePosition;
    }

    private void InitializeEyeViewRenderTexture()
    {
        eyeVieweRenderTexture.height = mainDisplay.systemHeight;
        eyeVieweRenderTexture.width = mainDisplay.systemWidth;
    }

    private void InitializeEyeViewScale()
    {
        UpdateScale();
    }
}
