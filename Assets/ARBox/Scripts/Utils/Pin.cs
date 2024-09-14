using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour
{
    private MovableObject _parent;
    private float distance = -1f;


    public void SetPinParent(MovableObject _parent)
    {
        this._parent = _parent;
    }

    public void Grabbed(float distance)
    {
        this.distance = distance;
        _parent.Grabbed();
    }

    public void Move(Ray _ray)
    {
        if (distance == -1f)
            return;
        transform.position = _ray.GetPoint(distance);
    }

    public void Released()
    {
        distance = -1f;
        _parent.Released();
    }

    public void FaceCamera()
    {
        transform.forward = Camera.main.transform.forward;
    }
}
