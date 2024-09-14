using System;
using System.Collections.Generic;

[Serializable]
public class ModelModel
{
    public string uri;
    public string uid;
    public string name;
    public int viewCount;
    public int likeCount;
    public int animationCount;
    public int commentCount;
    public string publishedAt;
    public Thumbnails thumbnails;
    public User user;
    public string description;
    public string createdAt;
    public int soundCount;

    public SketchfabImage GetThumbnail()
    {
        var imagesCount = thumbnails.images.Count;
        if (imagesCount > 3)
        {
            return thumbnails.images[3];
        }
        else
        {
            return thumbnails.images[imagesCount - 1];
        }
    }
}

[Serializable]
public class Thumbnails
{
    public List<SketchfabImage> images;
}


[Serializable]
public class SketchfabImage
{
    public string uid;
    public int size;
    public int width;
    public int height;
    public string url;
}

[Serializable]
public class User
{
    public string uid;
    public string username;
    public string displayName;
    public string profileUrl;
    public string account;
    public Avatar avatar;
}

[Serializable]
public class Avatar
{
    public string uri;
    public List<SketchfabImage> images;
}