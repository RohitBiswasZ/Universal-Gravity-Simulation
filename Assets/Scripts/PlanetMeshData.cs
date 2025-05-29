using System;
using Unity.Collections;
using UnityEngine;

public struct PlanetMeshData : IDisposable
{
    public NativeList<Vector3> vertices;
    public NativeList<Vector3> normals;
    public NativeList<int> triangles;

    public PlanetMeshData(Allocator allocator)
    {
        vertices = new NativeList<Vector3>(0, allocator);
        normals = new NativeList<Vector3>(0, allocator);
        triangles = new NativeList<int>(0, allocator);
    }

    public void AddPlanetMeshData(PlanetMeshData meshData)
    {
        for (int i = 0; i < meshData.triangles.Length; i++) triangles.Add(meshData.triangles[i] + vertices.Length); 
        vertices.AddRange(meshData.vertices.AsArray());
        normals.AddRange(meshData.normals.AsArray());
    }

    public void ClonePlanetMeshData(PlanetMeshData meshData, Allocator allocator)
    {
        vertices = new NativeList<Vector3>(0, allocator);
        normals = new NativeList<Vector3>(0, allocator);
        triangles = new NativeList<int>(0, allocator);
        
        vertices.AddRange(meshData.vertices.AsArray());
        normals.AddRange(meshData.normals.AsArray());
        triangles.AddRange(meshData.triangles.AsArray());
    }
    
    public void Dispose()
    {
        vertices.Dispose();
        normals.Dispose();
        triangles.Dispose();
    }
}