using UnityEngine;
using GLTFast;
using System.Threading.Tasks;

public class ARBoxObjectSpawner
{

    //[SerializeField]
    //GameObject demoTextPrefab;

    //[SerializeField]
    //string glbFilePath;



    //Vector3 rayOrigin;// Use the current object's position as the origin
    //Vector3 rayDirection; // Use the forward direction of the object (you can adjust this based on your needs)
    //float spawnDistance = 1f;

    private static GltfImport gltf;
    private static ARBoxObjectSpawner aRBoxObjectSpawner = null;

    // Start is called before the first frame update
    private ARBoxObjectSpawner()
    {
        gltf = new GltfImport();
    }

    public static ARBoxObjectSpawner GetInstance()
    {
        if (aRBoxObjectSpawner == null)
        {
            aRBoxObjectSpawner = new();
            InitializeGltf();
        }
        return aRBoxObjectSpawner;
    }

    static async void InitializeGltf()
    {
        gltf = await Task.Run<GltfImport>(() =>
        {
            return new();
        });
    }

    // Update is called once per frame
    void Update()
    {

        //rayOrigin = Camera.main.transform.position;
        //rayDirection = transform.forward;

        //if (Keyboard.current.uKey.wasPressedThisFrame)
        //{
        //    DebugDjay.GetInstance().Log("testJaySpawn: u key pressed");
        //    if (!isRayHittingObject())
        //        SpawnObject(glbFilePath, spawnDistance, demoTextPrefab);
        //}
    }

    public async void SpawnObject(GLBModelData gLBModel,Vector3 spawnPosition, GameObject demoTextPrefab)
    {
        DebugDjay.GetInstance().Log(gLBModel.GetGLBModelPath());
        Vector3 desiredScale = new(.05f, .05f, .05f); // Desired scale of the object

        var ActiveObject = new GameObject(gLBModel.modelName);
        ActiveObject.layer = glbLayerMask;
        // Set the object's new scale


        ActiveObject.transform.localScale = desiredScale;
        ActiveObject.transform.position = spawnPosition;
        var glbData = await ObjectRepo.GetGlbData(gLBModel.GetGLBJsonPath());
        if(gltf == null)
            InitializeGltf();
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
            DebugDjay.GetInstance().Log("testJay: Success creating object");
        }
        else
        {
            DebugDjay.GetInstance().Log("testJay: Failed creating object");
        }

    }

    public async void SpawnObject(ModelModel model, Vector3 spawnPosition)
    {
        Vector3 desiredScale = new(.05f, .05f, .05f); // Desired scale of the object

        var ActiveObject = new GameObject(model.name);
        ActiveObject.layer = glbLayerMask;
        // Set the object's new scale


        ActiveObject.transform.localScale = desiredScale;
        ActiveObject.transform.position = spawnPosition;

        var gltfPath = await SketchfabRepo.GetGLTFFilePath(model.uid);
        if (gltf == null)
            InitializeGltf();
        await gltf.Load(gltfPath);
        var instantiator = new GameObjectInstantiator(gltf, ActiveObject.transform);
        var success = await gltf.InstantiateMainSceneAsync(instantiator);
        if (success)
        {
            var legacyAnimation = instantiator.SceneInstance.LegacyAnimation;
            if (legacyAnimation != null)
            {
                legacyAnimation.wrapMode = WrapMode.Once;
                legacyAnimation.Play();
            }
            DebugDjay.GetInstance().Log("testJay: Success creating object");
        }
        else
        {
            DebugDjay.GetInstance().Log("testJay: Failed creating object");
        }
    }

    public bool isRayHittingGlbObject(Ray ray,float spawnDistance)
    {
        return Physics.Raycast(ray.origin, ray.direction, spawnDistance);
    }

    public static int glbLayerMask = LayerMask.GetMask("GLBObject");
    public static int nonGlbLayerMask = LayerMask.GetMask("NonGLBObjects");
}