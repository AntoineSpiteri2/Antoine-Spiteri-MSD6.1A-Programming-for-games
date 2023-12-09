using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class Cube : MonoBehaviour
{

    public Vector3 size = Vector3.one;


    public Quaternion Crot;
    private void Start()
    {
        CreateCube();
    }

    // Method to create a cube mesh with given dimensions and apply it to the GameObject
    void CreateCube()
    {
        MeshFilter meshFilter = gameObject.GetComponent<MeshFilter>();
        if (meshFilter == null)
        {
            meshFilter = gameObject.AddComponent<MeshFilter>();
        }

        MeshRenderer meshRenderer = gameObject.GetComponent<MeshRenderer>();
        if (meshRenderer == null)
        {
            meshRenderer = gameObject.AddComponent<MeshRenderer>();
        }

        MeshBuilder meshBuilder = new MeshBuilder(6); // Assuming 6 submeshes

        transform.rotation = Crot;

        // Define vertices
        Vector3[] vertices = new Vector3[8];
        for (int i = 0; i < 8; i++)
        {
            vertices[i] = new Vector3(
                size.x * ((i & 1) == 0 ? -1 : 1),
                size.y * ((i & 2) == 0 ? -1 : 1),
                size.z * ((i & 4) == 0 ? -1 : 1));
        }

        // Define triangles for each face (6 faces, 2 triangles per face)
        int[][] cubeTriangles = {
            new int[] { 0, 2, 3, 0, 3, 1 }, // Front face
            new int[] { 6, 4, 5, 6, 5, 7 }, // Back face
            new int[] { 0, 1, 5, 0, 5, 4 }, // Bottom face
            new int[] { 2, 6, 7, 2, 7, 3 }, // Top face
            new int[] { 0, 4, 6, 0, 6, 2 }, // Left face
            new int[] { 1, 3, 7, 1, 7, 5 }  // Right face
        };

        // Add triangles to the mesh
        foreach (int[] triangle in cubeTriangles)
        {
            meshBuilder.BuildTriangle(vertices[triangle[0]], vertices[triangle[1]], vertices[triangle[2]], 0);
            meshBuilder.BuildTriangle(vertices[triangle[3]], vertices[triangle[4]], vertices[triangle[5]], 0);
        }

        meshFilter.mesh = meshBuilder.CreateMesh();

        // Assign material here if necessary, or handle it externally in the calling script
    }

}
