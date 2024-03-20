using System.Collections.Generic;

[System.Serializable]
public class GLBObjectMetaData
{
    int id;
    string name;
    string url;

    public GLBObjectMetaData(int id, string name, string url)
    {
        this.id = id;
        this.name = name;
        this.url = url;
    }
    public GLBObjectMetaData() { }
}
