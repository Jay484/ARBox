using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class ModelsResponse
{
    string next;
    string previous;
    public List<ModelModel> results;
}
