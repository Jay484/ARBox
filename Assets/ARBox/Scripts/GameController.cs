using UnityEngine.InputSystem;
using UnityEngine;

public class GameController : MonoBehaviour
{

    [SerializeField]
    GameObject textPrefab;

    //private Transform mainCameraTransform;

    [HideInInspector]
    public GameObject highlightedGameObject = null;

    [HideInInspector]
    public GameObject selectedGameObject = null;

    private ARBoxObjectSpawner aRBoxObjectSpawner;
    private GLBModelData glbModel = null;
    private ModelModel sketchfabModel = null;

    public float spawnDistance = 0f;
    private Camera mainCamera;
    private Transform mainCameraTransform;
    // Create a RaycastHit variable to store information about the hit
    RaycastHit hit;
    private Pin grabbedObject = null;
    private RayCastableBackground rayCastableBackground = null;

    public ImageController imageController;

    private void OnEnable()
    {
        mainCamera = Camera.main;
        transform.parent = mainCamera.transform;
        aRBoxObjectSpawner = ARBoxObjectSpawner.GetInstance();
    }

    // Update is called once per frame
    void Update()
    {
        // Define the ray origin and direction
        // Use the current object's position as the origin
        // Use the forward direction of the object (you can adjust this based on your needs)
        mainCameraTransform = mainCamera.transform;

        // Perform the raycast


        GLBObjectInteraction();
        CanvasObjectInteraction();
        UpdateTransformOfHighlightedInfo();
        UpdateTransformOfSelectedInfo();
        SpawnObject();
        MoveGrabbedObject();

    }

    void GLBObjectInteraction()
    {
        if (Physics.Raycast(imageController.GetRay(), out hit, ARBoxObjectSpawner.glbLayerMask))
        {

            //DebugDjay.GetInstance().Log("HItting");
            // A hit occurred. You can access information about the hit using the 'hit' variable.
            // For example, you can get the name of the object hit:
            GameObject currObject = hit.collider.gameObject;

            if (highlightedGameObject != currObject)
            {
                if (highlightedGameObject != null)
                    Highlight(highlightedGameObject, false);
                highlightedGameObject = currObject;
                Highlight(highlightedGameObject, true);
            }

            //if (Keyboard.current.jKey.isPressed)
            //{
            //    MoveDown(highlightedGameObject);
            //}
            //else if (Keyboard.current.yKey.isPressed)
            //{
            //    MoveUp(highlightedGameObject);
            //}
            //else if (Keyboard.current.xKey.isPressed)
            //{
            //    MoveLeft(highlightedGameObject);
            //}
            //else if (Keyboard.current.wKey.isPressed)
            //{
            //    MoveRight(highlightedGameObject);
            //}
            //else if (Keyboard.current.aKey.isPressed)
            //{
            //    MoveForward(highlightedGameObject);
            //}
            //else if (Keyboard.current.dKey.isPressed)
            //{
            //    MoveBack(highlightedGameObject);
            //}
            //else
            if (ControllerKeyboardBinding.WasConfirmKeyPressedThisFrame())
            {
                ObjectSelected(currObject);
            }
            else if (ControllerKeyboardBinding.IsConfirmKeyPressed())
            {
                ObjectGrabbed(currObject, hit);
            }
            else if (ControllerKeyboardBinding.WasConfirmKeyReleasedThisFrame())
            {
                ObjectReleased();
            }

            // You can also perform actions based on the hit, such as moving the hit object, triggering events, etc.
        }
        else
        {
            //DebugDjay.GetInstance().Log("not HItting");
            // No hit occurred within the specified distance.
            if (highlightedGameObject != null && highlightedGameObject != selectedGameObject)
            {
                Highlight(highlightedGameObject, false);
                highlightedGameObject = null;
            }
            if (ControllerKeyboardBinding.WasConfirmKeyReleasedThisFrame())
            {
                ObjectSelected(null);
                ObjectReleased();
            }
        }
    }

    void CanvasObjectInteraction()
    {
        if(Physics.Raycast(imageController.GetRay(), out hit)){
            var currRayCastableBackground = hit.collider.gameObject.GetComponent<RayCastableBackground>();
            if (currRayCastableBackground == null)
                return;

            if (rayCastableBackground != null && rayCastableBackground != currRayCastableBackground)
            {
                rayCastableBackground.StopHovering();
            }
            rayCastableBackground = currRayCastableBackground;
            rayCastableBackground.StartHovering();


            if (ControllerKeyboardBinding.WasConfirmKeyReleasedThisFrame())
            {
                DebugDjay.GetInstance().Error("Confirm key clicked: " + rayCastableBackground);
                if (rayCastableBackground != null)
                    rayCastableBackground.OnConfirmClicked();
            }
        }
        else
        {
            if(rayCastableBackground != null)
            {
                rayCastableBackground.StopHovering();
                rayCastableBackground = null;
            }
        }
    }

    private void SpawnObject()
    {
        if (ControllerKeyboardBinding.WasConfirmKeyReleasedThisFrame() && imageController.IsRayCasting())
        {
            DebugDjay.GetInstance().Log("testJaySpawn: u key pressed");
            if (!aRBoxObjectSpawner.isRayHittingGlbObject(imageController.GetRay(), spawnDistance))
            {
                if (glbModel != null)
                {
                    aRBoxObjectSpawner.SpawnObject(glbModel, imageController.GetRay().GetPoint(spawnDistance), textPrefab);
                    glbModel = null;
                }
                else if(sketchfabModel != null)
                {
                    aRBoxObjectSpawner.SpawnObject(sketchfabModel, imageController.GetRay().GetPoint(spawnDistance));
                    sketchfabModel = null;
                }
                else
                {
                    DebugDjay.GetInstance().Error("No glbmodel set");
                }

                imageController.ResetRayLength();
            }
            else
            {
                DebugDjay.GetInstance().Log("Hitting gameobject");
            }
        }
    }

    private void  UpdateTransformOfHighlightedInfo()
    {
        if (highlightedGameObject == null)
        {
            return;
        }

        var textBox = CustomGameObjectInstantiator.GetTextBox(highlightedGameObject);
        if (textBox == null)
            return;
        //var parentBounds = BoundsUtil.GetBounds(highlightedGameObject);

        //DebugDjay.GetInstance().Log("Bounds: " + parentBounds.max.ToString());
        textBox.transform.rotation = Camera.main.transform.rotation;
        //textBox.transform.localScale = BoundsUtil.GlobalToLocalScale(highlightedGameObject.transform, new Vector3(1f, 1f, 1f));
    }

    private void UpdateTransformOfSelectedInfo()
    {
        if(selectedGameObject == null)
        {
            return;
        }

        var textBox = CustomGameObjectInstantiator.GetTextBox(selectedGameObject);

        //var parentBounds = BoundsUtil.GetBounds(selectedGameObject);

        //DebugDjay.GetInstance().Log("Bounds: " + parentBounds.max.ToString());
        //var offset = new Vector3(0, 2, 0) + Camera.main.transform.up;
        //var parentTopPosition = new Vector3(parentBounds.center.x, parentBounds.max.y, parentBounds.center.z);
        //textBox.transform.localPosition = parentTopPosition + offset;
        textBox.transform.rotation = Camera.main.transform.rotation;
        //textBox.transform.localScale = BoundsUtil.GlobalToLocalScale(selectedGameObject.transform, new Vector3(1f, 1f, 1f));
    }

    void ObjectSelected(GameObject gameObject)
    {
        if(gameObject == null)
        {
            Highlight(selectedGameObject, false);
            selectedGameObject = null;
            return;
        }

        if(selectedGameObject == null)
        {
            selectedGameObject = gameObject;
            Highlight(selectedGameObject, true);
            return;
        }
        Highlight(selectedGameObject, false);
        SetTextBoxVisibility(selectedGameObject, false);
        if (selectedGameObject == gameObject)
        {
            selectedGameObject = null;
        }
        else
        {
            selectedGameObject = gameObject;
            Highlight(selectedGameObject, true);
        }
    }

    void ObjectGrabbed(GameObject _gameObject, RaycastHit _raycastHit)
    {
        if (IsPin(_gameObject))
        {
            grabbedObject = _gameObject.GetComponent<Pin>();
            grabbedObject.Grabbed(_raycastHit.distance);
        }
    }

    void ObjectReleased()
    {
        if(grabbedObject != null)
        {
            grabbedObject.Released();
        }
        grabbedObject = null;
    }

    void MoveGrabbedObject()
    {
        if (grabbedObject == null)
            return;
        grabbedObject.Move(imageController.GetRay());
        grabbedObject.FaceCamera();
    }

    void MoveUp(GameObject gameObject, float moveSpeed = 0.1f)
    {
        if (gameObject == null)
            return;
        gameObject.transform.Translate(mainCameraTransform.up * moveSpeed * Time.deltaTime);

    }

    void MoveDown(GameObject gameObject, float moveSpeed = 0.5f)
    {
        if (gameObject == null)
            return;
        gameObject.transform.Translate(mainCameraTransform.up * -1 * moveSpeed * Time.deltaTime);
    }

    void MoveRight(GameObject gameObject, float moveSpeed = 0.5f)
    {
        if (gameObject == null)
            return;
        gameObject.transform.Translate(mainCameraTransform.right * moveSpeed * Time.deltaTime);
    }

    void MoveLeft(GameObject gameObject, float moveSpeed = 0.5f)
    {
        if (gameObject == null)
            return;
        gameObject.transform.Translate(mainCameraTransform.right * -1 * moveSpeed * Time.deltaTime);
    }

    void MoveForward(GameObject gameObject, float moveSpeed = 0.5f)
    {
        if (gameObject == null)
            return;
        gameObject.transform.Translate(mainCameraTransform.forward * moveSpeed * Time.deltaTime);
    }

    void MoveBack(GameObject gameObject, float moveSpeed = 0.5f)
    {
        if (gameObject == null)
            return;
        gameObject.transform.Translate(mainCameraTransform.forward * -1 * moveSpeed * Time.deltaTime);
    }

    private void Highlight(GameObject gameObject, bool setHighLight = true)
    {
        if (gameObject == null)
            return;
        var renderer = gameObject.GetComponent<Renderer>();
        if (renderer!=null && setHighLight)
        {
            float emissionIntensity = renderer.material.GetColor("emissiveFactor").b / Color.white.b;
            emissionIntensity += 2;
            renderer.material.SetColor("emissiveFactor", Color.white * emissionIntensity);
        }
        else
        {
            float emissionIntensity = renderer.material.GetColor("emissiveFactor").b / Color.white.b;
            emissionIntensity -= 2;
            renderer.material.SetColor("emissiveFactor", Color.white * emissionIntensity);
            
        }

        if (gameObject != selectedGameObject)
        {
            SetTextBoxVisibility(gameObject, setHighLight);
        }
    }

    private void SetTextBoxVisibility(GameObject gameObject, bool visible)
    {
        var textBox = CustomGameObjectInstantiator.GetTextBox(gameObject);
        if (textBox != null)
            textBox.SetActive(visible);
    }


    public void GlbModelSelected(GLBModelData glbModel)
    {
        if (sketchfabModel != null)
            sketchfabModel = null;
        this.glbModel = glbModel;
        imageController.SetRayLength(spawnDistance);
        DebugDjay.GetInstance().Log("Model changed to " + glbModel.modelName);
    }

    public void SketchfabModelSelectted(ModelModel sketchfabModel)
    {
        if (glbModel != null)
            glbModel = null;
        this.sketchfabModel = sketchfabModel;
        imageController.SetRayLength(spawnDistance);
        DebugDjay.GetInstance().Log("Model changed to " + sketchfabModel.name);
    }

    private bool IsPin(GameObject gameobject)
    {
        return gameobject.GetComponent<Pin>() != null;
    }

}