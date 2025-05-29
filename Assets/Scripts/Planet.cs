using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Planet : MonoBehaviour
{
    [Range(1,248)]public int resulation, radius;
    public Color baseColor;
    public MeshGenerator meshGenerator;
    public Material material;
    
    public void Start()
    {
        if (meshGenerator == null) transform.AddComponent<MeshGenerator>();
        
        PlanetMeshData meshData = new PlanetMeshData(Allocator.Persistent);
        PlanetMeshData cubicMeshData = meshGenerator.CubicFace(resulation, radius, Vector3.zero);

        meshData.ClonePlanetMeshData(cubicMeshData, Allocator.Persistent);
        cubicMeshData.Dispose();
        
        Debug.Log(meshData.vertices.Length);

        if (GetComponent<MeshFilter>() == null) transform.AddComponent<MeshFilter>();
        if (GetComponent<MeshRenderer>() == null) transform.AddComponent<MeshRenderer>();

        string matBaseColorId = "_BaseColor";
        material.SetColor(matBaseColorId, baseColor);
        
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material = material;
        
        MeshFilter meshFilter = transform.GetComponent<MeshFilter>();
        Mesh mesh = new Mesh();
        meshFilter.mesh = mesh;
        
        mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
        
        mesh.SetVertices(meshData.vertices.AsArray());
        mesh.SetIndices(meshData.triangles.AsArray(), MeshTopology.Triangles, 0);
        mesh.SetNormals(meshData.normals.AsArray());
    }
}