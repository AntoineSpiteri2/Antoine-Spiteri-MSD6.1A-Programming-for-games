using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Road : MonoBehaviour
{

    public enum RoadType
    {
        Straight,
        Curved,
        Intersection,
        T_Junction,
        Cross
        // Add more types as needed
    }

    public RoadType roadType;




    // Start is called before the first frame update
    void Start()
    {
        CreateRoad();
    }


    void CreateRoad()
    {
        switch (roadType)
        {
            case RoadType.Straight:
                createRoad(1,0.5f,2, this.gameObject.transform.position, this.gameObject.transform.rotation);
                break;
            case RoadType.Curved:
                createRoad(1, 0.5f, 2, this.gameObject.transform.position, this.gameObject.transform.rotation);
                createRoad(1, 0.5f, 2, this.gameObject.transform.position, Quaternion.Euler(0, 90, 0));


                break;
            case RoadType.Intersection:
                // Logic for creating an intersection
                break;
                // Implement other cases as needed
        }

    }



    public void createRoad( float width, float height, float length, Vector3 position, Quaternion rotation)
    {
        MeshFilter meshFilter = this.GetComponent<MeshFilter>();

        MeshBuilder meshBuilder = new MeshBuilder(6); // sub meshes is 6


        // Define vertices
        Vector3[] vertices = new Vector3[8];

        vertices[0] = new Vector3(-width / 2, 0, 0); // Bottom Left
        vertices[1] = new Vector3(width / 2, 0, 0); // Bottom Right
        vertices[2] = new Vector3(-width / 2, 0, length); // Top Left
        vertices[3] = new Vector3(width / 2, 0, length); // Top Right


        // Define triangles for the top face of the road
        int[] roadTriangles = { 0, 2, 3, 0, 3, 1 }; // Top face




        meshBuilder.BuildTriangle(vertices[roadTriangles[0]], vertices[roadTriangles[1]], vertices[roadTriangles[2]], 0);
        meshBuilder.BuildTriangle(vertices[roadTriangles[3]], vertices[roadTriangles[4]], vertices[roadTriangles[5]], 0);

        // Flip the first triangle
        meshBuilder.BuildTriangle(vertices[roadTriangles[0]], vertices[roadTriangles[2]], vertices[roadTriangles[1]], 0);

        // Flip the second triangle
        meshBuilder.BuildTriangle(vertices[roadTriangles[3]], vertices[roadTriangles[5]], vertices[roadTriangles[4]], 0);




        meshFilter.mesh = meshBuilder.CreateMesh();

        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();

        this.gameObject.transform.position = position;

        this.gameObject.transform.rotation = rotation;

        //Material buildingMaterial = new Material(Shader.Find("Standard"));
        //buildingMaterial.color = new UnityEngine.Color(150f / 255f, 75f / 255f, 0f / 255f, 1f); // Set the color or other properties

        //this.gameObject.GetComponent<MeshRenderer>().material = buildingMaterial;
    }



}
