using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SketchfabModels : MonoBehaviour
{

    List<ModelModel> modelsData = new();
    string categorySlug = null;
    string next = null;
    string previous = null;
    List<GameObject> sketchfab_model_holder_objects = null;

    [SerializeField]
    GameObject sketchfabModelPrefab = null;
    [SerializeField]
    GameController gameController;


    private async void FetchModels(string categorySlug, string cursor = null)
    {
        var response = await SketchfabRepo.GetModels(categorySlug,cursor);
        modelsData = response.results;      //use .add for caching and handle pagination accordingly;
        AddModels(categorySlug, modelsData);
    }

    private async void FetchNext(string nextCursor)
    {
        FetchModels(categorySlug, next);
    }

    private async void FetchPrevious(string previousCursor)
    {
        FetchModels(categorySlug, previous);
    }

    void AddModels(string categorySlug, List<ModelModel> modelModels)
    {
        if(sketchfab_model_holder_objects == null)
        {
            InitModelHoldersObjects(modelModels.Count);
        }
        LoadModels(modelModels);
    }

    async void LoadModels(List<ModelModel> modelModels)
    {
        for(int i = 0;i < modelModels.Count; i++)
        {
            var sketchfab_model_holder = sketchfab_model_holder_objects[i].GetComponent<SketchfabModelHolder>();
            sketchfab_model_holder.InitializeModel(modelModels[i], SketchfabModelSelected);
            await sketchfab_model_holder.LoadData();
        }
    }

    private void InitModelHoldersObjects(int count)
    {
        sketchfab_model_holder_objects = new();
        for(int i = 0; i< count; i++)
        {
            sketchfab_model_holder_objects.Add(Instantiate(sketchfabModelPrefab, transform));
        }
    }

    public void SetCategory(string categorySlug)
    {
        if (this.categorySlug == categorySlug)
            return;
        this.categorySlug = categorySlug;
        FetchModels(categorySlug, null);
    }

    public void SketchfabModelSelected(ModelModel model)
    {
        gameController.SketchfabModelSelectted(model);
        DebugDjay.GetInstance().Error("Model selected models");
    }

}
