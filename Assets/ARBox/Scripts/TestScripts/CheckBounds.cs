using UnityEngine;

public class CheckBounds : MonoBehaviour
{

    public GameObject game;
    public GameObject infoPrefab;
    Bounds bounds;
    GameObject top;
    int i = 1;

    // Start is called before the first frame update
    void Start()
    {

        //bounds = BoundsUtil.GetBounds(game);
        //top = Instantiate(infoPrefab,transform);
        //top.transform.position = new Vector3(bounds.center.x, bounds.max.y+1, bounds.max.z);
        //Vector3 scale = BoundsUtil.GlobalToLocalScale(transform, new Vector3(.5f, .5f, .5f));
        //top.transform.localScale = scale;
        ObjectData objectData = new();
        objectData.mesh_name = "info";
        objectData.info = "Mercury is the smallest planet in our solar system and the closest to the Sun. It has a rocky surface covered in craters and is extremely hot during the day but very cold at night. It has no atmosphere to trap heat, making its temperature extremes. Mercury takes just 88 Earth days to orbit the Sun.";
        CustomGameObjectInstantiator.InitializeInfo(game, infoPrefab,objectData,true);

    }

    // Update is called once per frame
    void Update()
    {
        //bounds = BoundsUtil.GetBounds(game);
        //DebugDjay.Log(bounds.max.ToString());
        //top.transform.position = new Vector3(bounds.center.x, bounds.max.y + 1, bounds.center.z);
        //top.transform.rotation = Camera.main.transform.rotation;
        if(i++ % 100 == 0)
        {
            transform.position = new Vector3(transform.position.x+.05f, transform.position.y+.05f, transform.position.z+.05f);
        }
    }
}
