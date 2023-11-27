using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LRoad : MonoBehaviour
{

    public float width;

    public float length;



    // Start is called before the first frame update
    void Start()
    {
        createLRoad(width, length, this.gameObject.transform.rotation);

    }




    public void createLRoad(float width, float length, Quaternion rotation)
    {
        MeshFilter meshFilter = this.GetComponent<MeshFilter>();
        MeshBuilder meshBuilder = new MeshBuilder(6); // Initialize MeshBuilder with 6 submeshes (if necessary)

        // Define vertices for L-shaped road
        // The road is assumed to be flat on the ground (y = 0) and consists of two segments

        // First segment vertices
        Vector3[] vertices = new Vector3[8];
        vertices[0] = new Vector3(-width / 2, 0, 0);                  // Bottom Left of the first segment
        vertices[1] = new Vector3(width / 2, 0, 0);                   // Bottom Right of the first segment
        vertices[2] = new Vector3(-width / 2, 0, length);             // Top Left of the first segment
        vertices[3] = new Vector3(width / 2, 0, length);              // Top Right of the first segment

        // Second segment vertices
        // It starts where the first segment ends and extends perpendicular to it
        vertices[4] = new Vector3(-width / 2, 0, length);             // Bottom Left of the second segment (same as top left of first)
        vertices[5] = new Vector3(-width / 2 + length, 0, length);    // Bottom Right of the second segment
        vertices[6] = new Vector3(-width / 2, 0, length + width);     // Top Left of the second segment
        vertices[7] = new Vector3(-width / 2 + length, 0, length + width); // Top Right of the second segment

        // Define triangles for the top face of the road
        // Each rectangle (segment) is made up of two triangles
        int[] roadTriangles = {
        0, 2, 3, 0, 3, 1, // Triangles for the first segment
        4, 6, 7, 4, 7, 5  // Triangles for the second segment
    };

        // Build triangles for the mesh
        for (int i = 0; i < roadTriangles.Length; i += 3)
        {
            meshBuilder.BuildTriangle(vertices[roadTriangles[i]], vertices[roadTriangles[i + 1]], vertices[roadTriangles[i + 2]], 0);
        }

        meshFilter.mesh = meshBuilder.CreateMesh(); // Assign the mesh to the mesh filter

        // Set the rotation of the GameObject to the provided rotation
        this.gameObject.transform.rotation = rotation;

        // If you have a material to apply, set it here
        // MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        // meshRenderer.material = [Your Road Material];
    }
}
