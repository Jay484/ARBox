using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSketchfabApi : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetDownloadInfo();
        DebugDjay.GetInstance().Log("this is test Log");
        DebugDjay.GetInstance().Error("this is test error");
        DebugDjay.GetInstance().Error("this is test error");
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    async void GetDownloadInfo()
    {
        var filePath = await SketchfabRepo.GetGLTFFilePath("1181d0bf729b43a39f53ebf3c0bec3be");
        DebugDjay.GetInstance().Error(filePath);
    }
}
