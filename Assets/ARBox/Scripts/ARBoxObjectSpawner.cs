using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.ARFoundation;
using System.IO;
using GLTFast;

public class ARBoxObjectSpawner : MonoBehaviour
{

    [SerializeField]
    GameObject gameObjectPrefab;

    [SerializeField]
    string glbFilePath;



    Vector3 rayOrigin;// Use the current object's position as the origin
    Vector3 rayDirection; // Use the forward direction of the object (you can adjust this based on your needs)
    float spawnDistance = .5f;

    private GltfImport gltf;

    // Start is called before the first frame update
    void Start()
    {
        gltf = new GltfImport();
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
                SpawnObject(glbFilePath, spawnDistance);
        }
    }

    private async void SpawnObject(string glbFilePath, float spawnDistance)
    {
        Vector3 spawnPosition = Camera.main.transform.position + Camera.main.transform.forward * spawnDistance;
        var absGlbFilePath = "file://"+Application.streamingAssetsPath+"/"+ glbFilePath;
        Debug.Log($"testJay: file path {absGlbFilePath}");

        var ActiveObject = new GameObject();

         Vector3 desiredScale = new Vector3(.05f, .05f, .05f); // Desired scale of the object

        // Set the object's new scale
        ActiveObject.transform.localScale = desiredScale;
        ActiveObject.transform.position = Camera.main.transform.position;
        //var gltfcomponent = ActiveObject.AddComponent<GltfAsset>();
        //gltfcomponent.Url = absGlbFilePath;

        gltf = new GltfImport();
        await gltf.Load(absGlbFilePath);
        var instantiator = new CustomGameObjectInstantiator(gltf, ActiveObject.transform);
        var success = await gltf.InstantiateMainSceneAsync(instantiator);
        if (success)
        {
            Debug.Log("testJay: Success creating object");
        }
        else
        {
            Debug.Log("testJay: Failed creating object");
        }

    }

    private bool isRayHittingObject()
    {
        if(Physics.Raycast(rayOrigin, rayDirection, spawnDistance))
            Debug.Log("testJaySpawn: Ray is hitting the object");
        return Physics.Raycast(rayOrigin, rayDirection, spawnDistance);
    }
}