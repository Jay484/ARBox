using System.Collections;
using System.Collections.Generic;
using GLTFast;
using GLTFast.Schema;
using UnityEngine;


public class CustomGameObjectInstantiator : GameObjectInstantiator
{
    GltfImport gltfImport;
    public CustomGameObjectInstantiator(GltfImport gltf, Transform parent) : base(gltf,parent) {
        gltfImport = gltf;
    }

    public override void AddPrimitive(uint nodeIndex, string meshName, MeshResult meshResult, uint[] joints = null, uint? rootJoint = null, float[] morphTargetWeights = null, int primitiveNumeration = 0)
    {
        base.AddPrimitive(nodeIndex, meshName, meshResult, joints, rootJoint, morphTargetWeights, primitiveNumeration);
        var currGameObject = m_Nodes[nodeIndex];
        Debug.Log("testJay" + meshName) ;
        currGameObject.AddComponent<MeshCollider>();
        var renderer = currGameObject.GetComponent<Renderer>();
        DebugDjay.Log("renderer: " + renderer.ToString());
        var baseTexture = renderer.material.GetTexture("baseColorTexture");
        if(!renderer.material.IsKeywordEnabled("_EMISSIVE") && baseTexture != null)
        {
            renderer.material.EnableKeyword("_EMISSIVE");
            renderer.material.SetTexture("emissiveTexture", baseTexture);
        }
    }

}