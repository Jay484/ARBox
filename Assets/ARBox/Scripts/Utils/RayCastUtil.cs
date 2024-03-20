using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastUtil
{
    public static bool GlbObjectRayCast(Ray ray, out RaycastHit hit)
    {
        return Physics.Raycast(ray, out hit, ARBoxObjectSpawner.glbLayerMask);
    }


}
