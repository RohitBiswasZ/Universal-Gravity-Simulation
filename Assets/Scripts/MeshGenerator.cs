using Unity.Collections;
using UnityEngine;

public class MeshGenerator : MonoBehaviour
{
    public CelestialBodyMeshData TopFace(int resolution, int radius, Vector3 offset)
    {
        CelestialBodyMeshData meshData = new CelestialBodyMeshData(Allocator.Temp);
        float stepPos = (float)radius / resolution;
        
        for (int z = 0; z <= resolution; z++)
        for (int x = 0; x <= resolution; x++)
        {
            Vector3 position = new Vector3(x * stepPos, radius, z * stepPos) - offset;
            Vector3 normalize = NormalizeCordinate(position, Vector3.one * radius / 2f - offset);
            meshData.vertices.Add(normalize * radius);
            meshData.normals.Add(normalize);
            
            if (z < resolution && x < resolution)
            {
                int i = z + x * (resolution + 1);
                
                meshData.triangles.Add(i);
                meshData.triangles.Add(i + resolution + 1);
                meshData.triangles.Add(i + 1);

                meshData.triangles.Add(i + 1);
                meshData.triangles.Add(i + resolution + 1);
                meshData.triangles.Add(i + resolution + 2);
            }
        }

        return meshData;
    }
    
    public CelestialBodyMeshData DownFace(int resolution, int radius, Vector3 offset)
    {
        CelestialBodyMeshData meshData = new CelestialBodyMeshData(Allocator.Temp);
        float stepPos = (float)radius / resolution;
        
        for (int z = 0; z <= resolution; z++)
        for (int x = 0; x <= resolution; x++)
        {
            Vector3 position = new Vector3(x * stepPos, 0, z * stepPos) - offset;
            Vector3 normalize = NormalizeCordinate(position, Vector3.one * radius / 2f - offset);
            meshData.vertices.Add(normalize * radius);
            meshData.normals.Add(normalize);
            
            if (z < resolution && x < resolution)
            {
                int i = z + x * (resolution + 1);
                
                meshData.triangles.Add(i + 1);
                meshData.triangles.Add(i + resolution + 1);
                meshData.triangles.Add(i);

                meshData.triangles.Add(i + resolution + 2);
                meshData.triangles.Add(i + resolution + 1);
                meshData.triangles.Add(i + 1);
            }
        }

        return meshData;
    }

    public CelestialBodyMeshData FrontFace(int resolution, int radius, Vector3 offset)
    {
        CelestialBodyMeshData meshData = new CelestialBodyMeshData(Allocator.Temp);
        float stepPos = (float)radius / resolution;
        
        for (int y = 0; y <= resolution; y++)
        for (int x = 0; x <= resolution; x++)
        {
            Vector3 position = new Vector3(x * stepPos, y * stepPos, radius) - offset;
            Vector3 normalize = NormalizeCordinate(position, Vector3.one * radius / 2f - offset);
            meshData.vertices.Add(normalize * radius);
            meshData.normals.Add(normalize);
            
            if (y < resolution && x < resolution)
            {
                int i = y + x * (resolution + 1);
                
                meshData.triangles.Add(i + 1);
                meshData.triangles.Add(i + resolution + 1);
                meshData.triangles.Add(i);

                meshData.triangles.Add(i + resolution + 2);
                meshData.triangles.Add(i + resolution + 1);
                meshData.triangles.Add(i + 1);
            }
        }

        return meshData;
    }
    
    public CelestialBodyMeshData BackFace(int resolution, int radius, Vector3 offset)
    {
        CelestialBodyMeshData meshData = new CelestialBodyMeshData(Allocator.Temp);
        float stepPos = (float)radius / resolution;
        
        for (int y = 0; y <= resolution; y++)
        for (int x = 0; x <= resolution; x++)
        {
            Vector3 position = new Vector3(x * stepPos, y * stepPos, 0) - offset;
            Vector3 normalize = NormalizeCordinate(position, Vector3.one * radius / 2f - offset);
            meshData.vertices.Add(normalize * radius);
            meshData.normals.Add(normalize);
            
            if (y < resolution && x < resolution)
            {
                int i = y + x * (resolution + 1);
                
                meshData.triangles.Add(i);
                meshData.triangles.Add(i + resolution + 1);
                meshData.triangles.Add(i + 1);

                meshData.triangles.Add(i + 1);
                meshData.triangles.Add(i + resolution + 1);
                meshData.triangles.Add(i + resolution + 2);
            }
        }

        return meshData;
    }
    
    public CelestialBodyMeshData RightFace(int resolution, int radius, Vector3 offset)
    {
        CelestialBodyMeshData meshData = new CelestialBodyMeshData(Allocator.Temp);
        float stepPos = (float)radius / resolution;
        
        for (int y = 0; y <= resolution; y++)
        for (int z = 0; z <= resolution; z++)
        {
            Vector3 position = new Vector3(radius, y * stepPos, z * stepPos) - offset;
            Vector3 normalize = NormalizeCordinate(position, Vector3.one * radius / 2f - offset);
            meshData.vertices.Add(normalize * radius);
            meshData.normals.Add(normalize);
            
            if (y < resolution && z < resolution)
            {
                int i = y + z * (resolution + 1);
                
                meshData.triangles.Add(i);
                meshData.triangles.Add(i + resolution + 1);
                meshData.triangles.Add(i + 1);

                meshData.triangles.Add(i + 1);
                meshData.triangles.Add(i + resolution + 1);
                meshData.triangles.Add(i + resolution + 2);
            }
        }

        return meshData;
    }
    
    public CelestialBodyMeshData LeftFace(int resolution, int radius, Vector3 offset)
    {
        CelestialBodyMeshData meshData = new CelestialBodyMeshData(Allocator.Temp);
        float stepPos = (float)radius / resolution;
        
        for (int y = 0; y <= resolution; y++)
        for (int z = 0; z <= resolution; z++)
        {
            Vector3 position = new Vector3(0, y * stepPos, z * stepPos) - offset;
            Vector3 normalize = NormalizeCordinate(position, Vector3.one * radius / 2f - offset);
            meshData.vertices.Add(normalize * radius);
            meshData.normals.Add(normalize);
            
            if (y < resolution && z < resolution)
            {
                int i = y + z * (resolution + 1);
                
                meshData.triangles.Add(i + 1);
                meshData.triangles.Add(i + resolution + 1);
                meshData.triangles.Add(i);

                meshData.triangles.Add(i + resolution + 2);
                meshData.triangles.Add(i + resolution + 1);
                meshData.triangles.Add(i + 1);
            }
        }

        return meshData;
    }

    public CelestialBodyMeshData CubicFace(int resulation, int radius, Vector3 offset)
    {
        CelestialBodyMeshData topMeshData = TopFace(resulation, radius, offset);
        CelestialBodyMeshData downMeshData = DownFace(resulation, radius, offset);
        CelestialBodyMeshData frontMeshData = FrontFace(resulation, radius, offset);
        CelestialBodyMeshData backMeshData = BackFace(resulation, radius, offset);
        CelestialBodyMeshData rightMeshData = RightFace(resulation, radius, offset);
        CelestialBodyMeshData leftMeshData = LeftFace(resulation, radius, offset);

        CelestialBodyMeshData meshData = new CelestialBodyMeshData(Allocator.Temp);
        
        meshData.AddPlanetMeshData(topMeshData);
        meshData.AddPlanetMeshData(downMeshData);
        meshData.AddPlanetMeshData(frontMeshData);
        meshData.AddPlanetMeshData(backMeshData);
        meshData.AddPlanetMeshData(rightMeshData);
        meshData.AddPlanetMeshData(leftMeshData);
        
        topMeshData.Dispose();
        downMeshData.Dispose();
        frontMeshData.Dispose();
        backMeshData.Dispose();
        rightMeshData.Dispose();
        leftMeshData.Dispose();

        return meshData;
    }

    Vector3 NormalizeCordinate(Vector3 cordinate, Vector3 center)
    {
        float distance = Vector3.Distance(center, cordinate);
        Vector3 direction = cordinate - center;
        Vector3 normal = direction / distance;
        return normal;
    }
}
