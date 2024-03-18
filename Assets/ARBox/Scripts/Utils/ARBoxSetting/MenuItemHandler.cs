using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuItemHandler : MonoBehaviour
{
    bool visibility = false;

    public void ToggleVisibility()
    {
        visibility = !visibility;
        gameObject.SetActive(visibility);
    }
}
