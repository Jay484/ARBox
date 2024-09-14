using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableObject : MonoBehaviour
{
    [SerializeField]
    GameObject pinPrefab;

    GameObject pinObject;

    Transform parent;

    Vector3 offset = new(0f, 0f, 0f);

    public void Init()
    {
        pinObject = Instantiate(pinPrefab);
        parent = transform.parent;
        var pinBounds = BoundsUtil.GetBounds(pinObject);
        var topRight = BoundsUtil.GetTopRight(gameObject);
        offset.x = pinBounds.size.x/2;
        offset.y = pinBounds.size.y/2;
        pinObject.transform.localPosition = topRight + offset;
        pinObject.transform.SetParent(transform);
        pinObject.GetComponent<Pin>().SetPinParent(this);
    }

    public void Grabbed()
    {
        pinObject.transform.SetParent(null);
        transform.SetParent(pinObject.transform);
    }


    public void Released()
    {
        transform.SetParent(parent);
        pinObject.transform.SetParent(transform);
    }

}
