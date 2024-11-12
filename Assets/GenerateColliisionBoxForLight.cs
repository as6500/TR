using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(MeshCollider))]
public class GenerateColliisionBoxForLight : MonoBehaviour
{
    [SerializeField] private Light2D light;
    
    private void Start()
    {
        GenerateCollision();
    }

    private void GenerateCollision()
    {
        List<Vector3> Verticies = new List<Vector3>();
        List<int> Triangles = new List<int>();

        // Get the spotlight angle in degrees
        float angle = light.pointLightOuterAngle; 

        // Calculate the positions of the two points based on the light direction
        // ALl this is based off the origin (0,0,0) as it lives in a child therefore should always be relative to the parent
        Vector3 pointA = new Vector3(0,0,0);
        Vector3 pointB = Quaternion.Euler(0, -angle / 2, 0) * Vector3.forward * (light.pointLightOuterRadius * 2);
        Vector3 pointC = Quaternion.Euler(0, angle / 2, 0) * Vector3.forward * (light.pointLightOuterRadius * 2);

        Vector3 YOffset = new Vector3(0, 20, 0);

        // Top 3
        Verticies.Add(pointA + YOffset);
        Verticies.Add(pointB + YOffset);
        Verticies.Add(pointC + YOffset);

        // Bottom 3
        Verticies.Add(pointA - YOffset);
        Verticies.Add(pointB - YOffset);
        Verticies.Add(pointC - YOffset);

        // Top Triangle
        Triangles.Add(0); // A+
        Triangles.Add(1); // B+
        Triangles.Add(2); // C+

        // Bottom Triangle
        Triangles.Add(3); // A-
        Triangles.Add(5); // B-
        Triangles.Add(4); // C-

        // Front Face
        Triangles.Add(1); // B+
        Triangles.Add(4); // B-
        Triangles.Add(5); // C-
        
        Triangles.Add(1); // B+
        Triangles.Add(5); // C-
        Triangles.Add(2); // C+

        // Left Face
        Triangles.Add(0); // A+
        Triangles.Add(3); // A-
        Triangles.Add(4); // B-
        
        Triangles.Add(0); // A+
        Triangles.Add(4); // B-
        Triangles.Add(1); // B+

        // Right Face
        Triangles.Add(0); // A+
        Triangles.Add(2); // C+
        Triangles.Add(5); // C-

        Triangles.Add(0); // A+ 
        Triangles.Add(5); // C-
        Triangles.Add(3); // A-


        Mesh mesh = new Mesh();
        mesh.vertices = Verticies.ToArray();
        mesh.triangles = Triangles.ToArray();

        GetComponent<MeshCollider>().sharedMesh = mesh;
        GetComponent<MeshCollider>().convex = true;

        // If one exists we can try put our mesh on it
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        if (meshFilter && meshFilter.sharedMesh == null)
        {
            meshFilter.sharedMesh = mesh;
        }
    }
}
