
public class GLBModelData
{
    public string modelName;
    public GLBModelData(string modelName)
    {
        this.modelName = modelName;
    }

    public string GetGLBModelPath()
    {
        return "https://arstudies.s3.ap-south-1.amazonaws.com/" + modelName + ".glb";
    }

    public string GetGLBJsonPath()
    {
        return "https://arstudies.s3.ap-south-1.amazonaws.com/" + modelName + ".json";
    }

    public string GetGLBThumbnailPath()
    {
        return "https://arstudies.s3.ap-south-1.amazonaws.com/" + modelName + ".jpg";
    }
}
