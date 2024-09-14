using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class SketchfabCategories : MonoBehaviour
{

    List<CategoryModel> dataCategories = new();

    [SerializeField]
    GameObject categoryPrefab;

    [SerializeField]
    SketchfabModels sketchfabModels;

    // Start is called before the first frame update
    void OnEnable()
    {
        FetchCategories();
    }


    async void FetchCategories()
    {
        dataCategories = await SketchfabRepo.GetCategories();
        AddCategories(dataCategories);
    }

    async void AddCategories(List<CategoryModel> categoryModels)
    {
        foreach (var categoryModel in categoryModels)
        {
            var categoryObject = Instantiate(categoryPrefab, transform);
            var sketchfabCategoryHolder = categoryObject.GetComponent<SketchfabCategoryHolder>();
            sketchfabCategoryHolder.InitializeCategory(categoryModel,OnCategorySelected);
            await sketchfabCategoryHolder.LoadData();
        }
        OnCategorySelected(dataCategories[14].slug);
        AttachMovablePin();
    }

    public void OnCategorySelected(string categorySlug)
    {
        sketchfabModels.SetCategory(categorySlug);
    }

    void AttachMovablePin()
    {
        var movable = transform.parent.gameObject.GetComponent<MovableObject>();
        if(movable != null)
        {
            movable.Init();
        }
    }

}
