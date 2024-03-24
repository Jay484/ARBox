using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

public class BottomMenu : MonoBehaviour
{
    private Camera mainCamera;
    List<GameObject> loadables = new ();
    bool load = true;
    // Create a RaycastHit variable to store information about the hit
    private RaycastHit hit;
    private string hitObjectName = string.Empty;
    private GameObject highLightedBottomMenuItem;
    private int ColumnSize = 5;
    private int RowSize = 3;
    private int hitIndex = -1;
    private int prevHitIndex = -1;
    public GameController gameController;
    [SerializeField] ImageController imageController = null;

    private void OnEnable()
    {
        mainCamera = Camera.main;
        initializeLoadableArray();
        InitializeBottomMenu();
    }

    // Update is called once per frame
    void Update()
    {
        RayCastToHighlightMenuItem();
        
    }

    private async void InitializeBottomMenu()
    {
        List<GLBModelData> dataSet = await ObjectRepo.GetGLBObjectsMetaData("", 1);
        LoadModelData(dataSet,dataSet.Count);

    }

    public async void LoadModelData(List<GLBModelData> dataSet, int size)
    {
        for (int i = 0; i < size; i++)
        {
            loadables[i].SetActive(true);
            var img = loadables[i].GetComponent<BottomMenuItem>();
            img.SetModelData(dataSet[i]);
            await img.UpdateTexture();
        }
        for(int i = size; i < 15; i++)
        {
            loadables[i].SetActive(false);
        }

    }


    private void initializeLoadableArray()
    {
        for(int i=0; i< RowSize; i++)
        {
            for(int j = 0; j< ColumnSize; j++)
            {
                var name = loadableGameobjectNamePrefix + i.ToString() + j.ToString();
                var loadable = transform.Find(name).gameObject;
                loadable.AddComponent<BottomMenuItem>();
                loadables.Add(loadable);
            }
        }
    }

    private void RayCastToHighlightMenuItem()
    {
        if (imageController == null || !imageController.IsRayCasting())
        {
            ResetSelectedMenuItem();
            return;
        }

        if (Physics.Raycast(imageController.GetRay(), out hit, ARBoxObjectSpawner.nonGlbLayerMask))
        {
            hitObjectName = hit.transform.gameObject.name;
            if (!IsValidBottomMenuItemName(hitObjectName))
                return;
            hitIndex = Get1DIndexByName(hitObjectName);
            if (prevHitIndex != hitIndex)
            {
                if (prevHitIndex != -1)
                {
                    highLightedBottomMenuItem.GetComponent<BottomMenuItem>().UnHighlight();
                }
                prevHitIndex = hitIndex;
                if (prevHitIndex != -1)
                {
                    highLightedBottomMenuItem = loadables[prevHitIndex];
                    highLightedBottomMenuItem.GetComponent<BottomMenuItem>().Highlight();
                }
            }
            if (ControllerKeyboardBinding.WasConfirmKeyReleasedThisFrame())
            {
                gameController.GlbModelSelected(highLightedBottomMenuItem.GetComponent<BottomMenuItem>().GetGlbModelData());
            }
        }
        else
        {
            ResetSelectedMenuItem();
        }
    }

    private void ResetSelectedMenuItem()
    {
        if (prevHitIndex != -1)
        {
            highLightedBottomMenuItem.GetComponent<BottomMenuItem>().UnHighlight();
            highLightedBottomMenuItem = null;
            prevHitIndex = hitIndex = -1;
            hitObjectName = string.Empty;
        }
    }

    private Vector2 Get2DIndexByName(string BottomMenuItemName) {
        string index = BottomMenuItemName.Split('_')[1];
        string Iindex = index[0].ToString();
        string Jindex = index[1].ToString();
        return new Vector2(int.Parse(Iindex, NumberStyles.HexNumber), int.Parse(Jindex, NumberStyles.HexNumber));
    }

    private int Get1DIndexByName(string BottomMenuItemName)
    {
        Vector2 indices = Get2DIndexByName(BottomMenuItemName);
        if (indices.x >= RowSize || indices.y >= ColumnSize)
            return -1;
        int index = ColumnSize * ((int)indices.x) + ((int)indices.y);
        return index;
    }

    bool IsValidBottomMenuItemName(string name)
    {
        var parts = name.Split('_');
        return parts.Length == 2 &&
            parts[1].Length == 2 &&
            char.IsLetterOrDigit(parts[1][0]) &&
            char.IsLetterOrDigit(parts[1][1]);
    }


    //Loadables should follow the ruls
    //i. keep following prefix
    //ii. use index after prefix
    //iii. if index > 9 use hexadecimal convention and handle the loop accordingly
    private string loadableGameobjectNamePrefix = "image_";

}