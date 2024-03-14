using UnityEngine;
using GLTFast;

public class ARBoxObjectSpawner
{

    //[SerializeField]
    //GameObject demoTextPrefab;

    //[SerializeField]
    //string glbFilePath;



    //Vector3 rayOrigin;// Use the current object's position as the origin
    //Vector3 rayDirection; // Use the forward direction of the object (you can adjust this based on your needs)
    //float spawnDistance = 1f;

    private GltfImport gltf;

    // Start is called before the first frame update
    public ARBoxObjectSpawner()
    {
        gltf = new GltfImport();
    }

    // Update is called once per frame
    void Update()
    {

        //rayOrigin = Camera.main.transform.position;
        //rayDirection = transform.forward;

        //if (Keyboard.current.uKey.wasPressedThisFrame)
        //{
        //    Debug.Log("testJaySpawn: u key pressed");
        //    if (!isRayHittingObject())
        //        SpawnObject(glbFilePath, spawnDistance, demoTextPrefab);
        //}
    }

    public async void SpawnObject(GLBModelData gLBModel, float spawnDistance, GameObject demoTextPrefab)
    {
        DebugDjay.Log(gLBModel.GetGLBModelPath());
        Vector3 spawnPosition = Camera.main.transform.position + Camera.main.transform.forward * spawnDistance;
        Vector3 desiredScale = new(.05f, .05f, .05f); // Desired scale of the object

        var ActiveObject = new GameObject(gLBModel.modelName);
        ActiveObject.layer = glbLayerMask;
        // Set the object's new scale


        ActiveObject.transform.localScale = desiredScale;
        ActiveObject.transform.position = spawnPosition;

        gltf = new GltfImport();
        var glbData = await ObjectRepo.GetGlbData(gLBModel.GetGLBJsonPath());
        await gltf.Load(gLBModel.GetGLBModelPath());
        var instantiator = new CustomGameObjectInstantiator(gltf, ActiveObject.transform, demoTextPrefab, glbData);
        //var instantiator = new GameObjectInstantiator(gltf, ActiveObject.transform);
        var success = await gltf.InstantiateMainSceneAsync(instantiator);
        if (success)
        {
            var legacyAnimation = instantiator.SceneInstance.LegacyAnimation;
            if (legacyAnimation != null)
            {
                legacyAnimation.wrapMode = WrapMode.Once;
                legacyAnimation.Play();
            }
            Debug.Log("testJay: Success creating object");
        }
        else
        {
            Debug.Log("testJay: Failed creating object");
        }

    }

    public bool isRayHittingObject(float spawnDistance)
    {
        DebugDjay.Log(glbLayerMask.ToString());
        var cameraTransform = Camera.main.transform;
        return Physics.Raycast(cameraTransform.position, cameraTransform.forward, spawnDistance, glbLayerMask);
    }

    public static int glbLayerMask = LayerMask.GetMask("GLBObject");
    public static int nonGlbLayerMask = LayerMask.GetMask("NonGLBObjects");
}