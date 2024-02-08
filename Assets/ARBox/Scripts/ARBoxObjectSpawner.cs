using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.ARFoundation;

public class ARBoxObjectSpawner : MonoBehaviour
{

    [SerializeField]
    GameObject gameObjectPrefab;


    Vector3 rayOrigin;// Use the current object's position as the origin
    Vector3 rayDirection; // Use the forward direction of the object (you can adjust this based on your needs)
    float spawnDistance = .5f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        rayOrigin = Camera.main.transform.position;
        rayDirection = transform.forward;

        if (Keyboard.current.uKey.wasPressedThisFrame)
        {
            Debug.Log("testJaySpawn: u key pressed");
            if (!isRayHittingObject())
                SpawnObject(gameObjectPrefab, spawnDistance);
        }
    }

    public void SpawnObject(GameObject prefab, float spawnDistance)
    {
        Vector3 spawnPosition = Camera.main.transform.position + Camera.main.transform.forward * spawnDistance;

        // Spawn the object at the calculated position
        Instantiate(prefab , spawnPosition, Quaternion.identity);
    }

    private bool isRayHittingObject()
    {
        if(Physics.Raycast(rayOrigin, rayDirection, spawnDistance))
            Debug.Log("testJaySpawn: Ray is hitting the object");
        return Physics.Raycast(rayOrigin, rayDirection, spawnDistance);
    }
}
