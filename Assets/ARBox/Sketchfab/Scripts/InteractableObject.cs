using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface InteractableObject
{
    public void ConfirmKeyClicked();

    public void RayStartedHitting();

    public void RayStoppedHitting();

    public bool IsHighlighted();
}
