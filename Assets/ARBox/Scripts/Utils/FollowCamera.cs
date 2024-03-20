using UnityEngine;

public class FollowCamera : MonoBehaviour
{

    private Camera mainCamera;
    private Transform mainCameraTransform;
    public float yOffset = 0f;
    public float moveSpeed = 5f; // Speed of the animation
    public GameObject baseObject;  // For length and width
    public float zDistance;
    public float padding = 0f; // Padding to keep the target within the viewport

    Vector3 bottomViewportPoint;
    Vector3 bottomScreenPoint;
    Vector3 currViewportPoint;


    private Vector3 targetPosition;


    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        mainCameraTransform = mainCamera.transform;
        //yOffset = BoundsUtil.GetBounds(baseObject).extents.y + .01f ;
        MoveToBottomCenter();
    }

    // Update is called once per frame
    void Update()
    {
        MatchCameraRotationAndZaxis();
        Follow();
    }



    private void Follow()
    {
        currViewportPoint = mainCamera.WorldToViewportPoint(transform.position);
        if (!IsObjectVisibleOnScreen(currViewportPoint))
        {
            MoveToBottomCenter();
        }
        else
        {
        }
    }

    //private void _Follow()
    //{
    //    currPosition = transform.position;
    //    Vector3 viewportPoint = mainCamera.WorldToViewportPoint(currPosition);
    //    transform.rotation = mainCamera.transform.rotation;
    //    //transform.forward = mainCamera.transform.forward;
    //    if (IsPointInsideViewPort(viewportPoint))
    //    {
    //        targetPosition = currPosition;
    //        DebugDjay.Log("Inside Viewport");
    //    }
    //    targetPosition.z = mainCamera.transform.position.z + zDistance;
    //    // Smoothly move the object towards the target position
    //    transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);

    //}

    private bool IsPointInsideViewPort(Vector3 viewportPoint)
    {
        DebugDjay.Log("CurrViewPort: " + viewportPoint);
        return viewportPoint.x >= 0 && viewportPoint.x <= 1 &&
            viewportPoint.y >= 0 && viewportPoint.y <= 1 &&
            viewportPoint.z >= 0
            ;
    }

    bool IsObjectVisibleOnScreen(Vector3 viewportPos)
    {
        // Check if the object's viewport coordinates are within the screen bounds
        bool isWithinScreenBounds = viewportPos.x >= 0 && viewportPos.x <= 1 &&
                                    viewportPos.y >= 0 && viewportPos.y <= 1 &&
                                    viewportPos.z > 0; // Ensure object is in front of the camera

        if (!isWithinScreenBounds)
            return false;

        // Check if the object is within the camera's field of view
        bool isWithinFieldOfView = Vector3.Angle(transform.position - Camera.main.transform.position, Camera.main.transform.forward) <= Camera.main.fieldOfView / 2f;

        // Check if the object's Z position is within the camera's near and far clipping planes
        bool isWithinClippingPlanes = (transform.position - Camera.main.transform.position).magnitude >= Camera.main.nearClipPlane &&
                                      (transform.position - Camera.main.transform.position).magnitude <= Camera.main.farClipPlane;

        // Check if the object's screen size is larger than a certain threshold
        float objectScreenSize = Mathf.Max(Screen.width, Screen.height) * (transform.lossyScale.magnitude / (transform.position - Camera.main.transform.position).magnitude);
        bool isLargeEnoughOnScreen = objectScreenSize >= 1f; // Adjust threshold as needed

        return isWithinFieldOfView && isWithinClippingPlanes && isLargeEnoughOnScreen;
    }

    private void MoveToLastVisibleViewportPosition()
    {

    }

    private void MoveToBottomCenter()
    {
        bottomViewportPoint = new Vector3(0.5f, 0f, mainCamera.nearClipPlane);
        bottomScreenPoint = mainCamera.ViewportToScreenPoint(bottomViewportPoint);
        DebugDjay.Log(bottomScreenPoint.ToString());
        // Convert the screen coordinates to world space
        targetPosition = mainCamera.ScreenToWorldPoint(bottomScreenPoint);
        targetPosition.y += yOffset; // Add the yOffset
        targetPosition.z = mainCameraTransform.position.z + zDistance;

        // Smoothly move the object towards the target position
        transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);

    }

    private void MatchCameraRotationAndZaxis()
    {
        transform.rotation = mainCameraTransform.rotation;
        targetPosition = transform.position;
        targetPosition.z = mainCameraTransform.position.z + zDistance;
        transform.position = targetPosition;
    }


}
