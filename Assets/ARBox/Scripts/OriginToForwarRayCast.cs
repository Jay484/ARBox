using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class OriginToForwarRayCast : MonoBehaviour
{

    private Transform mainCameraTransform;

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
        GameObject highlightedGameObject = null;

        // Perform the raycast
        if (Physics.Raycast(rayOrigin, rayDirection, out hit))
        {
            // A hit occurred. You can access information about the hit using the 'hit' variable.
            // For example, you can get the name of the object hit:
            string hitObjectName = hit.collider.gameObject.name;
            if (highlightedGameObject != hit.collider.gameObject)
            {
                Highlight(highlightedGameObject, false);
                highlightedGameObject = hit.collider.gameObject;
            }
            Highlight(highlightedGameObject, true);



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
            }else if (Keyboard.current.aKey.isPressed)
            {
                MoveForward(highlightedGameObject);
            }else if (Keyboard.current.dKey.isPressed)
            {
                MoveBack(highlightedGameObject);
            }



            DebugDjay.GetInstance().Log("testJay Ray hit: " + hitObjectName);

            // You can also perform actions based on the hit, such as moving the hit object, triggering events, etc.
        }
        else
        {
            // No hit occurred within the specified distance.
            if (Keyboard.current.hKey.isPressed && highlightedGameObject != null)
            {
                Highlight(highlightedGameObject, false);
            }
        }
    }

    void MoveUp(GameObject gameObject, float moveSpeed = 0.1f)
    {
        gameObject.transform.Translate(mainCameraTransform.up * moveSpeed * Time.deltaTime);

    }

    void MoveDown(GameObject gameObject, float moveSpeed = 0.5f)
    {
        gameObject.transform.Translate(mainCameraTransform.up * -1 * moveSpeed * Time.deltaTime);
    }

    void MoveRight(GameObject gameObject, float moveSpeed = 0.5f)
    {
        gameObject.transform.Translate(mainCameraTransform.right * moveSpeed * Time.deltaTime);
    }

    void MoveLeft(GameObject gameObject, float moveSpeed = 0.5f)
    {
        gameObject.transform.Translate(mainCameraTransform.right * -1 * moveSpeed * Time.deltaTime);
    }

    void MoveForward(GameObject gameObject, float moveSpeed = 0.5f)
    {
        gameObject.transform.Translate(mainCameraTransform.forward * moveSpeed * Time.deltaTime);
    }
    void MoveBack(GameObject gameObject, float moveSpeed = 0.5f)
    {
        gameObject.transform.Translate(mainCameraTransform.forward * -1 * moveSpeed * Time.deltaTime);
    }

    private void Highlight(GameObject gameObject, bool setHighLigh = true)
    {

    }
}