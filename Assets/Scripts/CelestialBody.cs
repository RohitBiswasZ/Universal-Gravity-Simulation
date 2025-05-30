using NaughtyAttributes;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class CelestialBody : MonoBehaviour
{
    [Min(0)] public float mass;
    
    [Range(1,248)]public int resulation, radius;
    public Color baseColor;
    public MeshGenerator meshGenerator;
    public Rigidbody rigidBody;
    public Shader shader;
    
    public void Start()
    {
        InitComponents();
        
        currentVelocity = initialVelocity;
        
        rigidBody = GetComponent<Rigidbody>();
        rigidBody.useGravity = false;
        rigidBody.isKinematic = true;
        
        UpdateMesh();
    }

    public void InitComponents()
    {
        if (meshGenerator == null) transform.AddComponent<MeshGenerator>();
        if (rigidBody != null) transform.AddComponent<Rigidbody>();
        if (GetComponent<MeshFilter>() == null) transform.AddComponent<MeshFilter>();
        if (GetComponent<MeshRenderer>() == null) transform.AddComponent<MeshRenderer>();
    }

    [Button("Generate Mesh")]
    public void UpdateMesh()
    {
        InitComponents();
        
        CelestialBodyMeshData meshData = new CelestialBodyMeshData(Allocator.Persistent);
        CelestialBodyMeshData cubicMeshData = meshGenerator.CubicFace(resulation, radius, Vector3.zero);

        meshData.ClonePlanetMeshData(cubicMeshData, Allocator.Persistent);
        cubicMeshData.Dispose();
        
        Debug.Log(meshData.vertices.Length);
        
        MeshFilter meshFilter = transform.GetComponent<MeshFilter>();
        Mesh mesh = new Mesh();
        meshFilter.mesh = mesh;
        
        mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
        
        mesh.SetVertices(meshData.vertices.AsArray());
        mesh.SetIndices(meshData.triangles.AsArray(), MeshTopology.Triangles, 0);
        mesh.SetNormals(meshData.normals.AsArray());
        
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        
        Material material = new Material(shader);
        string matBaseColorId = "_BaseColor";
        material.SetColor(matBaseColorId, baseColor);

        meshRenderer.material = material;
    }

    public void FixedUpdate()
    {
        CelestialBody[] celestialBodies = new CelestialBody[transform.parent.childCount];
        for (int i = 0; i < transform.parent.childCount; i++) celestialBodies[i] = transform.parent.GetChild(i).GetComponent<CelestialBody>();
        UpdateVelocity(celestialBodies, Universal.timeStep);
        UpdatePosition(Universal.timeStep);
    }

    public Vector3 initialVelocity;
    private Vector3 currentVelocity;
    
    public void UpdateVelocity(CelestialBody[] allBodies, float timeSteps)
    {
        for (int i = 0; i < allBodies.Length; i++)
        {
            if (allBodies[i] != this)
            {
                float dist = Vector3.Distance(transform.position, allBodies[i].transform.position);
                Vector3 forceDir = (allBodies[i].transform.position - transform.position).normalized;
                Vector3 force = forceDir * Universal.gravity * mass * allBodies[i].mass * Mathf.Pow(dist, 2);
                Vector3 acceleration = force / mass;
                currentVelocity += acceleration * timeSteps;
            }
        }
    }
    
    public void UpdatePosition(float timeSteps)
    {
        rigidBody.position += currentVelocity * timeSteps;
    }
}