using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ImageController : MonoBehaviour
{
    [SerializeField] private ARTrackedImageManager aRTrackedImageManager;
    [SerializeField] private GameObject imageControllerPrefab;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private float DefaultRayLength = 20;
    private float rayLength;
    private GameObject imageControllerObject = null;
    private Ray ray = new();
    private RaycastHit hit;
    [SerializeField] private Material greenMaterial;
    [SerializeField] private Material redMaterial;


    private void Start()
    {
        ResetRayLength();
    }

    private void OnEnable()
    {
        aRTrackedImageManager.trackedImagesChanged += ImageFound;
    }

    private void OnDisable()
    {
        aRTrackedImageManager.trackedImagesChanged -= ImageFound;
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    private void ImageFound(ARTrackedImagesChangedEventArgs obj)
    {
        foreach (var trackedImage in obj.added)
        {
            imageControllerObject = Instantiate(imageControllerPrefab,trackedImage.transform);
            imageControllerObject.transform.Rotate(new Vector3(45, 0, 0));
            UpdateRay();
        }
        foreach (var trackedImage in obj.updated)
        {
            if (trackedImage.trackingState == TrackingState.Tracking)
            {
                if(imageControllerObject != null)
                {
                    if (!imageControllerObject.activeInHierarchy)
                    {
                        imageControllerObject.SetActive(true);
                        lineRenderer.enabled = true;
                    }
                    UpdateRay();


                    lineRenderer.SetPosition(0, ray.origin);

                    if (Physics.Raycast(ray, out hit, rayLength))
                    {
                        RayCollidingAt(hit.point);
                    }
                    else
                    {
                        RayNotColliding();
                    }
                }
            }
            else if (trackedImage.trackingState == TrackingState.Limited)
            {
                if(imageControllerObject != null)
                {
                    if (imageControllerObject.activeInHierarchy)
                    {
                        imageControllerObject.SetActive(false);
                        lineRenderer.enabled = false;
                    }
                }
            }
        }
    }


    public void DrawRay(GameObject imageControllerObject,LineRenderer lineRenderer)
    {
        if (!imageControllerObject.activeInHierarchy)
            return;

        Ray ray = new(imageControllerObject.transform.position, imageControllerObject.transform.forward);
        DebugDjay.GetInstance().Log(lineRenderer.materials.Length);
        lineRenderer.material = lineRenderer.materials[0];
        lineRenderer.SetPosition(0, ray.origin);
        lineRenderer.SetPosition(1, ray.GetPoint(100));
    }

    private void UpdateRay()
    {
        if (imageControllerObject == null)
            return;
        ray.origin = imageControllerObject.transform.position;
        ray.direction = imageControllerObject.transform.forward;
    }

    private void RayCollidingAt(Vector3 point)
    {
        lineRenderer.SetPosition(1, point);
        lineRenderer.material = redMaterial;
    }


    private void RayNotColliding()
    {
        lineRenderer.SetPosition(1, ray.GetPoint(rayLength));
        lineRenderer.material = greenMaterial;
    }

    public bool IsRayCasting()
    {
        return lineRenderer.enabled;
    }

    public Ray GetRay()
    {
        //return new Ray(Camera.main.transform.position, Camera.main.transform.forward); //Debug ray
        return ray;
    }

    public void SetRayLength(float length)
    {
        rayLength = length;
    }

    public void ResetRayLength()
    {
        DebugDjay.GetInstance().Error("reset raylength " + DefaultRayLength);
        rayLength = DefaultRayLength;
    }

}
