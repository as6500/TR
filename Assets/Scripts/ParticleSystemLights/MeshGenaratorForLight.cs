using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshGenaratorForLight : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        CreateCube();
    }

    private void CreateCube()
    {
        Vector3[] vertices = {
            new Vector3 (0, 0, 20),
            new Vector3 (15, -5, 20),
            new Vector3 (15, 5, 20),
            new Vector3 (0, 0, 20),
            new Vector3 (0, 0, -20),
            new Vector3 (15, -5, -20),
            new Vector3 (15, 5, -20),
            new Vector3 (0, 0, -20),
        };

        int[] triangles = {
            0, 2, 1, //face front
			0, 0, 0,
            2, 3, 4, //face top
			2, 4, 6,
            1, 2, 5, //face right
			2, 5, 6,
            0, 0, 0, //face left
			0, 0, 0,
            0, 0, 0, //face back
			5, 7, 6,
            0, 5, 7, //face bottom
			0, 1, 5
        };

/*        int[] triangles = {
            0, 2, 1, //face front
			0, 3, 2,
            2, 3, 4, //face top
			2, 4, 5,
            1, 2, 5, //face right
			1, 5, 6,
            0, 7, 4, //face left
			0, 4, 3,
            5, 4, 7, //face back
			5, 7, 6,
            0, 6, 7, //face bottom
			0, 1, 6
        };*/

        Mesh mesh = GetComponent<MeshCollider>().sharedMesh;
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.Optimize();
        mesh.RecalculateNormals();
    }
}
