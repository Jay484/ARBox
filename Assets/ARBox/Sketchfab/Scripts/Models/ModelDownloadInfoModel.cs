using System;

[Serializable]
public class ModelDownloadInfoModel
{
    public Source source;
    public Source gltf;
    public Source usdz;
    public Source glb;
}


[Serializable]
public class Source
{
    public string url;
    public int size;
    public int expires;
}