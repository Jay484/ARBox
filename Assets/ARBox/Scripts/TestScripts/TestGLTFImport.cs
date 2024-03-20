using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGLTFImport : MonoBehaviour
{
    public string glbfilepath;
    public float spawnDistance;
    public GameObject textPrefab;

    private GLBModelData gLBModel;

    // Start is called before the first frame update
    void Start()
    {
        ARBoxObjectSpawner spawner = new();
        gLBModel = new("SolarSystem");

        //var abspath = "file://" + Application.streamingAssetsPath + "/" + glbfilepath;
        var abspath = gLBModel.GetGLBModelPath();
        spawner.SpawnObject(gLBModel, spawnDistance, textPrefab);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
