using UnityEngine.InputSystem;
using UnityEngine;

public class GameController : MonoBehaviour
{

    private Transform mainCameraTransform;

    [HideInInspector]
    public GameObject highlightedGameObject = null;

    [HideInInspector]
    public GameObject selectedGameObject = null;

    // Start is called before the first frame update
    void Start()
    {
        mainCameraTransform = Camera.main.transform;
        transform.parent = mainCameraTransform;
    }

    // Update is called once per frame
    void Update()
    {
        // Define the ray origin and direction

        Vector3 rayOrigin = Camera.main.transform.position; // Use the current object's position as the origin
        Vector3 rayDirection = transform.forward; // Use the forward direction of the object (you can adjust this based on your needs)


        // Create a RaycastHit variable to store information about the hit
        RaycastHit hit;

        // Perform the raycast
        if (Physics.Raycast(rayOrigin, rayDirection, out hit))
        {
            // A hit occurred. You can access information about the hit using the 'hit' variable.
            // For example, you can get the name of the object hit:
            GameObject currObject = hit.collider.gameObject;

            if (highlightedGameObject != currObject)
            {
                if(highlightedGameObject != null)
                    Highlight(highlightedGameObject, false);
                highlightedGameObject = currObject;
                Highlight(highlightedGameObject, true);
            }

            if (Keyboard.current.jKey.isPressed)
            {
                MoveDown(highlightedGameObject);
            }
            else if (Keyboard.current.yKey.isPressed)
            {
                MoveUp(highlightedGameObject);
            }
            else if (Keyboard.current.xKey.isPressed)
            {
                MoveLeft(highlightedGameObject);
            }
            else if (Keyboard.current.wKey.isPressed)
            {
                MoveRight(highlightedGameObject);
            }
            else if (Keyboard.current.aKey.isPressed)
            {
                MoveForward(highlightedGameObject);
            }
            else if (Keyboard.current.dKey.isPressed)
            {
                MoveBack(highlightedGameObject);
            }else if (Keyboard.current.hKey.wasPressedThisFrame)
            {
                DebugDjay.Log("H key pressed");
                ObjectSelected(currObject);
            }

            // You can also perform actions based on the hit, such as moving the hit object, triggering events, etc.
        }
        else
        {
            // No hit occurred within the specified distance.
            if (highlightedGameObject != null && highlightedGameObject != selectedGameObject)
            {
                Highlight(highlightedGameObject, false);
                highlightedGameObject = null;
            }
            if (Keyboard.current.hKey.wasPressedThisFrame)
            {
                ObjectSelected(null);
            }
        }


        UpdateTransformOfHighlightedInfo();
        UpdateTransformOfSelectedInfo();
    }

    private void  UpdateTransformOfHighlightedInfo()
    {
        if (highlightedGameObject == null)
        {
            return;
        }

        var textBox = CustomGameObjectInstantiator.GetTextBox(highlightedGameObject);

        var parentBounds = BoundsUtil.GetBounds(highlightedGameObject);

        DebugDjay.Log("Bounds: " + parentBounds.max.ToString());
        var offset = new Vector3(0, 2, 0) + Camera.main.transform.up;
        var parentTopPosition = new Vector3(parentBounds.center.x, parentBounds.max.y, parentBounds.center.z);
        textBox.transform.localPosition = parentTopPosition + offset;
        textBox.transform.rotation = Camera.main.transform.rotation;
        textBox.transform.localScale = BoundsUtil.GlobalToLocalScale(highlightedGameObject.transform, new Vector3(1f, 1f, 1f));
    }

    private void UpdateTransformOfSelectedInfo()
    {
        if(selectedGameObject == null)
        {
            return;
        }

        var textBox = CustomGameObjectInstantiator.GetTextBox(selectedGameObject);

        var parentBounds = BoundsUtil.GetBounds(selectedGameObject);

        DebugDjay.Log("Bounds: " + parentBounds.max.ToString());
        var offset = new Vector3(0, 2, 0) + Camera.main.transform.up;
        var parentTopPosition = new Vector3(parentBounds.center.x, parentBounds.max.y, parentBounds.center.z);
        textBox.transform.localPosition = parentTopPosition + offset;
        textBox.transform.rotation = Camera.main.transform.rotation;
        textBox.transform.localScale = BoundsUtil.GlobalToLocalScale(selectedGameObject.transform, new Vector3(1f, 1f, 1f));
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
            DebugDjay.Log("focused before"+emissionIntensity);
            emissionIntensity += 2;
            DebugDjay.Log("focused after" + emissionIntensity);
            renderer.material.SetColor("emissiveFactor", Color.white * emissionIntensity);
        }
        else
        {
            float emissionIntensity = renderer.material.GetColor("emissiveFactor").b / Color.white.b;
            DebugDjay.Log("not before" + emissionIntensity);
            emissionIntensity -= 2;
            DebugDjay.Log("not after" + emissionIntensity);
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




}