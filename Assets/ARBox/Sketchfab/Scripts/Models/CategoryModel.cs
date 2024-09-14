using System;
using System.Collections.Generic;


[Serializable]
public class CategoryModel
{
    public string uid;
    public string name;
    public string slug;
    public string uri;
    public string icon;
    public List<ImageMetaData> thumbnails;

}

[Serializable]
public class ImageMetaData
{
    public string width;
    public string height;
    public string url;
}
