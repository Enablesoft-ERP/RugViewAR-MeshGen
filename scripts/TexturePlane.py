import os
import sys
import bpy
import addon_utils

from enum import Enum

class ObjectType(Enum):
    Plane = 0

class BlenderContext:
    def CreateObject(type: ObjectType):
        if (type == ObjectType.Plane):
            bpy.ops.mesh.primitive_plane_add()
            return bpy.data.objects[len(bpy.data.objects) - 1]
            

    def DeselectObjects():
        for object in bpy.data.objects:
            object.select.select_set(bool(0))
            
    def SelectObject(id: str):
        for object in bpy.data.objects:
            if (object.name_full == id):
                object.select_set(bool(True))
    
    def EnableAddOn(addOn: str):
        addon_utils.enable(addOn)
        

class Vector2D:
    X = int(0)
    Y = int(0)

#    def __iadd__(self, right: Vector2D):
#        self.X += right.X
#        self.Y += right.Y
    
    def __eq__(self, right):
        return (self.X == right.X and self.Y == right.Y)
    
    def Add(self, vector):   
        self.X += right.X
        self.Y += right.Y
         
    def __init__(self, x: int = 0, y: int = 0):
        self.X = x
        self.Y = y


class Texture:
    ID = str()
    Material = None
    
    def LoadImage(filePath: str):
        bpy.data.images.new(filePath)
    
    def BuildMaterial():
        return
    
    def __init__(self, filePath):
        self.Material = str()

class Plane:
    ID = str()
    
    BpyObject = None
    
    Dimensions = Vector2D()
    
    def __init__(self, dimensions):
        self.Dimensions = Vector2D()
        self.BpyObject = BlenderContext.CreateObject(ObjectType.Plane)
        self.ID = self.BpyObject.name_full
        self.Scale(dimensions)
    
    def SetTexture(self, texturePath: str):
        return
    
    def Scale(self, dimensions: Vector2D):
        if (dimensions == self.Dimensions):
            return
        
        #self,Dimensions.Add(dimensions)
        self.BpyObject.scale[0] += dimensions.X
        self.BpyObject.scale[1] += dimensions.Y 


#Plane(Vector2D(2, 1))
class Tools:
    def CombineStrings(arr: list[str], delimiter: str, start: int, end: int):
        combinedString = str()
        
        for x in range(start, end - 1):
            combinedString += f"{arr[x]}{delimiter}"
        
        combinedString += arr[end - 1]
        
        return combinedString
    
    def ReplaceFileExtension(path: str, extension: str):
        splitArray = path.split('.')
        
        return (Tools.CombineStrings(splitArray, '.', 0, len(splitArray) - 1) + "." + extension)

    def GetExtension(path: str):
        split = path.split('.')
        return split[len(split) - 1]

class TexturePlane:
    Path = str()
    
    Name = str()
    
    def Export(self, fileName: str):
        bpy.ops.export_scene.gltf(filepath=(fileName))
    
    def Initialize(self):
        splitArray = self.Path.split('/')
        
        self.Name = splitArray[len(splitArray) - 1]
        
        bpy.ops.import_image.to_plane(files=list([dict({"name": self.Path})]))     
    
    def __init__(self, path: str):
        self.Path = path     
        self.Initialize()



#BlenderContext.EnableAddOn("io_import_images_as_planes")
def GenerateModels(imageDirPath: str, outputPath: str):
    dirList: list[str] = os.listdir(imageDirPath)
    
    split: list[str] = None

    for path in dirList:
        path = f"{imageDirPath}/{path}"
        if (Tools.GetExtension(path) == "jpg"):
            split = path.split('/')
    
            for x in range(2):
                bpy.ops.object.select_all()
            bpy.ops.object.delete()
    

            modelName: str = Tools.ReplaceFileExtension(split[len(split) - 1], "glb")

            if (os.path.exists(f"{outputPath}/{modelName}")):
                print(f"Model for {path} exists.")
                continue
            TexturePlane(path).Export(f"{outputPath}/{modelName}")

def Main(argv: list[str]) -> None:
    BlenderContext.EnableAddOn("io_import_images_as_planes")

    GenerateModels(argv[0], argv[1])

Main(sys.argv[sys.argv.index("--") + 1:])



