using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class House : MonoBehaviour
{

    public int CubeSize;

    public Vector3 CubeLocation;



    // Start is called before the first frame update
    void Start()
    {

        createcube();

    }


    public void createcube()
    {
        MeshFilter meshFilter = this.GetComponent<MeshFilter>();

        MeshBuilder meshBuilder = new MeshBuilder(6); // sub meshes is 6

        Vector3 halfSize = Vector3.one * CubeSize * 0.5f;

        // Define vertices
        Vector3[] vertices = new Vector3[8];
        for (int i = 0; i < 8; i++)
        {
            vertices[i] = new Vector3(
                CubeLocation.x + halfSize.x * ((i & 1) == 0 ? -1 : 1),
                CubeLocation.y + halfSize.y * ((i & 2) == 0 ? -1 : 1),
                CubeLocation.z + halfSize.z * ((i & 4) == 0 ? -1 : 1));
        }

        // Define triangles for each face (6 faces, 2 triangles per face)
        int[][] cubeTriangles = new int[][]
        {
            new int[] { 0, 2, 3, 0, 3, 1 }, // Front face
            new int[] { 6, 4, 5, 6, 5, 7 }, // Back face
            new int[] { 0, 1, 5, 0, 5, 4 }, // Bottom face
            new int[] { 2, 6, 7, 2, 7, 3 }, // Top face
            new int[] { 0, 4, 6, 0, 6, 2 }, // Left face
            new int[] { 1, 3, 7, 1, 7, 5 }, // Right face
        };

        // Add triangles to the mesh
        foreach (int[] triangle in cubeTriangles)
        {
            meshBuilder.BuildTriangle(vertices[triangle[0]], vertices[triangle[1]], vertices[triangle[2]], 0);
            meshBuilder.BuildTriangle(vertices[triangle[3]], vertices[triangle[4]], vertices[triangle[5]], 0);
        }

        meshFilter.mesh = meshBuilder.CreateMesh();

        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();

        Material buildingMaterial = new Material(Shader.Find("Standard"));
        buildingMaterial.color = new UnityEngine.Color(150f / 255f, 75f / 255f, 0f / 255f, 1f); // Set the color or other properties

        this.gameObject.GetComponent<MeshRenderer>().material = buildingMaterial;
    }

}
