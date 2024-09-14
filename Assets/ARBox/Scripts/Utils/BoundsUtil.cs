using UnityEngine;

public class BoundsUtil
{

    public static Bounds GetBounds(GameObject gameObject )
    {
        var bounds = new Bounds(gameObject.transform.position, Vector3.zero);
        Collider collider = gameObject.GetComponent<Collider>();
        if (collider != null)
        {
            bounds =  collider.bounds;
        }
        Renderer renderer = gameObject.GetComponent<Renderer>();
        if(renderer !=null)
        {
            bounds = renderer.bounds;
        }

        if (gameObject.transform.childCount == 0)
        {
            return bounds;
        }
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            var childBounds = GetBounds(gameObject.transform.GetChild(i).gameObject);
            bounds.Encapsulate(childBounds);
        }
        return bounds;
    }



    public static Vector3 GlobalToLocalScale(Transform parent, Vector3 desiredGlobalScale)
    {
        return new Vector3(
            desiredGlobalScale.x / parent.transform.localScale.x,
            desiredGlobalScale.y / parent.transform.localScale.y,
            desiredGlobalScale.z / parent.transform.localScale.z
        );

    }

    public static Vector3 GetTopRight(GameObject gameObject)
    {
        Bounds parentBounds = GetBounds(gameObject);
        var topRight = new Vector3(parentBounds.max.x, parentBounds.max.y, parentBounds.min.z);
        if (gameObject.transform.childCount == 0)
        {
            return topRight;
        }
        for(int i =0; i < gameObject.transform.childCount; i++)
        {
            var childTopRight = GetTopRight(gameObject.transform.GetChild(i).gameObject);
            topRight.x = Mathf.Max(topRight.x, childTopRight.x);
            topRight.y = Mathf.Max(topRight.y, childTopRight.y);
            topRight.z = Mathf.Min(topRight.z, childTopRight.z);
        }
        return topRight;
    }

}


/*
 * 
 using GLTFast;
using TMPro;
using UnityEngine;


public class CustomGameObjectInstantiator : GameObjectInstantiator
{
    GameObject infoPrefab;
    public CustomGameObjectInstantiator(GltfImport gltf, Transform parent, GameObject infoPrefab) : base(gltf,parent) {
        //gltfImport = gltf;
        this.infoPrefab = infoPrefab;
    }

    public override void AddPrimitive(uint nodeIndex, string meshName, MeshResult meshResult, uint[] joints = null, uint? rootJoint = null, float[] morphTargetWeights = null, int primitiveNumeration = 0)
    {
        base.AddPrimitive(nodeIndex, meshName, meshResult, joints, rootJoint, morphTargetWeights, primitiveNumeration);
        var currGameObject = m_Nodes[nodeIndex];
        SetupObject(currGameObject);
        InitializeInfo(currGameObject)
        
    }

    void InitializeInfo(GameObject parent)
    {
        var parentBounds = BoundsUtil.GetBounds(parent);
        var offset = new Vector3(0, 1, 0);
        var parentTopPosition = new Vector3(parentBounds.center.x, parentBounds.max.y, parentBounds.max.z);

        var info = Object.Instantiate(infoPrefab, parent.transform);
        info.name = InfoObjectName;
        info.transform.localPosition = parent.transform.position = parentTopPosition + offset;
        info.transform.localScale = BoundsUtil.GlobalToLocalScale(parent.transform, new Vector3(.5f, .5f, .5f));
        var textBox = info.GetComponent<TextMeshPro>();
        if (textBox == null)
        {
            DebugDjay.GetInstance().Log("Cant find textmesh");
        }
        else
        {
            textBox.text = "Test Object";
        }
        info.SetActive(false);
    }

    private void SetupObject(GameObject gameObject)
    {
        gameObject.AddComponent<MeshCollider>();

        var renderer = gameObject.GetComponent<Renderer>();
        var baseTexture = renderer.material.GetTexture("baseColorTexture");
        if (!renderer.material.IsKeywordEnabled("_EMISSIVE") && baseTexture != null)
        {
            renderer.material.EnableKeyword("_EMISSIVE");
            renderer.material.SetTexture("emissiveTexture", baseTexture);
        }
    }


    public static string InfoObjectName = "Info";


}
 * 
 */