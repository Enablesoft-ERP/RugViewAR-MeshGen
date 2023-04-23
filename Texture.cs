using SharpGLTF.Materials;

namespace RugViewAR.MeshGen;

public class Texture
{
    public MaterialBuilder Material;

    public string Name;

    public Texture(string path)
    {
        string[] splitArray = path.Split('/');

        this.Name = splitArray[splitArray.Length - 1];
        
        Console.WriteLine("Called");
        this.Material = new MaterialBuilder()
            .WithMetallicRoughnessShader()
            .WithChannelImage(KnownChannel.BaseColor, path);
    }
}
