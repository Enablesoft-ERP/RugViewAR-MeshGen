﻿using System.Numerics;
using System.Runtime.CompilerServices;
using SharpGLTF;
using SharpGLTF.Geometry;
using SharpGLTF.Geometry.VertexTypes;
using SharpGLTF.Materials;


namespace RugViewAR.MeshGen
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Scene scene = new Scene();

            Plane plane = new Plane(new Vector2(0, 0), new Vector2(1, 1));
            plane.SetTexture(new Texture("texture.png"));
            
            scene.AddMesh(plane.Mesh);
            
            scene.Export("exported.gltf");
        }
    }
}