using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestImageControllerRay : MonoBehaviour
{

    [SerializeField] GameObject gameController;
    [SerializeField] LineRenderer lineRenderer;

    ImageController imageController;

    // Start is called before the first frame update
    void Start()
    {
        imageController = new();
    }

    // Update is called once per frame
    void Update()
    {
        imageController.DrawRay(gameController, lineRenderer);
    }
}
