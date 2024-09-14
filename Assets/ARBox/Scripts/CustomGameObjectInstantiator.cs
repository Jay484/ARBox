using GLTFast;
using TMPro;
using UnityEngine;


public class CustomGameObjectInstantiator : GameObjectInstantiator
{
    GameObject infoPrefab;
    GLBObject gLBData;
    
    public CustomGameObjectInstantiator(GltfImport gltf, Transform parent, GameObject infoPrefab, GLBObject gLBData) : base(gltf, parent)
    {
        //gltfImport = gltf;
        this.infoPrefab = infoPrefab;
        this.gLBData = gLBData;
    }

    public override void AddPrimitive(uint nodeIndex, string meshName, MeshResult meshResult, uint[] joints = null, uint? rootJoint = null, float[] morphTargetWeights = null, int primitiveNumeration = 0)
    {
        base.AddPrimitive(nodeIndex, meshName, meshResult, joints, rootJoint, morphTargetWeights, primitiveNumeration);
        var currGameObject = m_Nodes[nodeIndex];

        DebugDjay.GetInstance().Log(meshName);
        SetupObject(currGameObject);
        if (gLBData == null)
            return;
        ObjectData objectData = gLBData.objects.Find((item) => item.mesh_name == meshName);
        if(objectData != null)
            InitializeInfo(currGameObject, infoPrefab, objectData);

    }

    public static void InitializeInfo(GameObject parent,GameObject infoPrefab,ObjectData objectData, bool isInfoActive = false)
    {
        var info = Object.Instantiate(infoPrefab);
        var infoScale = new Vector3(.05f, .05f, .05f);
        info.transform.localScale = infoScale;
        info.name = InfoObjectName;

        var parentBounds = BoundsUtil.GetBounds(parent);
        var offset = new Vector3(0, 0, 0);
        var parentTopPosition = new Vector3(parentBounds.center.x, parentBounds.max.y, parentBounds.center.z);
        info.transform.localPosition = parentTopPosition + offset;

        //Set position of info textbox
        //info.transform.localScale = BoundsUtil.GlobalToLocalScale(parent.transform,infoScale);
        var textBox = info.GetComponent<TextMeshPro>();
        if (textBox == null)
        {
            DebugDjay.GetInstance().Log("Cant find textmesh");
        }
        else
        {
            textBox.text = objectData.info;
        }


        info.transform.SetParent(parent.transform);

        info.SetActive(isInfoActive);
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

    public static GameObject GetTextBox(GameObject gameObject)
    {
        var textBoxTransform = gameObject.transform.Find(CustomGameObjectInstantiator.InfoObjectName);
        if (textBoxTransform == null)
            return null;
        return textBoxTransform.gameObject;
    }


    public static string InfoObjectName = "Info";

}

/*
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
        DebugDjay.GetInstance().Log("testJay" + meshName) ;

        var info = Object.Instantiate(infoPrefab, currGameObject.transform);
        info.name = InfoObjectName;
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


        currGameObject.AddComponent<MeshCollider>();
        var renderer = currGameObject.GetComponent<Renderer>();
        DebugDjay.GetInstance().Log("renderer: " + renderer.ToString());
        var baseTexture = renderer.material.GetTexture("baseColorTexture");
        if(!renderer.material.IsKeywordEnabled("_EMISSIVE") && baseTexture != null)
        {
            renderer.material.EnableKeyword("_EMISSIVE");
            renderer.material.SetTexture("emissiveTexture", baseTexture);
        }
    }



    
    public static string InfoObjectName = "Info";


}
*/