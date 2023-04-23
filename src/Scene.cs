using System.Numerics;
using SharpGLTF.Geometry;
using SharpGLTF.Materials;
using SharpGLTF.Scenes;
using SharpGLTF.Schema2;

namespace RugViewAR.MeshGen;

public class Scene
{
    protected SceneBuilder Builder;

    protected ModelRoot Model;
    
    public void Export(string path)
    {
        this.Model = this.Builder.ToGltf2();
        
        this.Model.SaveGLTF(path);
    }

    public void AddMesh(IMeshBuilder<MaterialBuilder> mesh)
    {
        this.Builder.AddRigidMesh(mesh, Matrix4x4.Identity);
    }
    
    public Scene()
    {
        this.Builder = new SceneBuilder();
    }
}