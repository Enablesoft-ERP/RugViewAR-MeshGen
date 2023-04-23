using System.Numerics;
using SharpGLTF.Geometry;
using SharpGLTF.Geometry.VertexTypes;
using SharpGLTF.Materials;
using SharpGLTF.Schema2;

namespace RugViewAR.MeshGen;

public class Plane
{
    public Vector2 Position;

    public Vector2 Dimensions;

    public MaterialBuilder Texture;

    public MeshBuilder<VertexPosition, VertexTexture1> Mesh; 
    
    protected PrimitiveBuilder<MaterialBuilder, VertexPosition, VertexTexture1, VertexEmpty> Primitive;

    protected VertexPosition[] Positions;

    protected VertexTexture1[] TextureCoordinates;

    protected VertexBuilder<VertexPosition, VertexTexture1, VertexEmpty>[] Buffer;


    public void Rotate(Vector3 vector)
    {
        for (int x = 0; x < this.Positions.Length; x++)
        {
            this.Positions[x].ApplyTransform(Matrix4x4.CreateRotationX(vector.X));
            this.Positions[x].ApplyTransform(Matrix4x4.CreateRotationY(vector.Y));
            this.Positions[x].ApplyTransform(Matrix4x4.CreateRotationZ(vector.Z));
        }
    }

    public void Translate(Vector3 vector)
    {
        for (int x = 0; x < this.Positions.Length; x++)
        {
            this.Positions[x].Position += vector;
        }
    }

    public void SetTexture(Texture texture)
    {
        this.Texture = texture.Material;
        this.GenerateMesh();
    }

    protected void Initialize()
    {
        this.Buffer = new VertexBuilder<VertexPosition, VertexTexture1, VertexEmpty>[4];

        this.TextureCoordinates = new VertexTexture1[] {
            new VertexTexture1(new Vector2(0, 0)),
            new VertexTexture1(new Vector2(0, 1)),
            new VertexTexture1(new Vector2(1, 1)),
            new VertexTexture1(new Vector2(1, 0))
        };

        this.Positions = new VertexPosition[] {
            new VertexPosition(this.Position.X,this.Position.Y,0),
            new VertexPosition(Position.X, this.Position.Y + this.Dimensions.Y, 0),
            new VertexPosition(this.Position.X + this.Dimensions.X, this.Position.Y + this.Dimensions.Y, 0),
            new VertexPosition(this.Position.X + this.Dimensions.X, this.Position.Y, 0)
        };

        for (int x = 0; x < this.Positions.Length; x++)
            this.Buffer[x] = new VertexBuilder<VertexPosition, VertexTexture1, VertexEmpty>(this.Positions[x], this.TextureCoordinates[x]);

        this.GenerateMesh();
    }

    public void GenerateMesh()
    {
        this.Primitive = this.Mesh.UsePrimitive(this.Texture);

        this.Primitive.AddTriangle(this.Buffer[0], this.Buffer[1], this.Buffer[2]);
        this.Primitive.AddTriangle(this.Buffer[0], this.Buffer[2], this.Buffer[3]);
    }

    public Plane(Vector2 position, Vector2 dimensions)
    {
        this.Position = position;
        this.Dimensions = dimensions;

        this.Mesh = new MeshBuilder<VertexPosition, VertexTexture1>();
        this.Texture = new MaterialBuilder();
        
        this.Initialize();
    }
}