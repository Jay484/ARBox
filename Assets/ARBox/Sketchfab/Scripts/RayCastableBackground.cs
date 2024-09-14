using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastableBackground : MonoBehaviour
{
    private InteractableObject parent;
    private void OnEnable()
    {
        parent = transform.parent.GetComponent<InteractableObject>();
    }

    public void OnConfirmClicked()
    {
        parent.ConfirmKeyClicked();
    }

    public void StartHovering()
    {
        parent.RayStartedHitting();
    }

    public void StopHovering()
    {
        parent.RayStoppedHitting();
    }

    public bool IsHovering()
    {
        return parent.IsHighlighted();
    }

}
