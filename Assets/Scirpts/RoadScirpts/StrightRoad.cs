using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StrightRoad : MonoBehaviour
{
    public Vector3 RoadSize;
    public Vector3 PavementSize;
    public Vector3 MarkingSize;
    public float MarkingSpacing;

    void Start()
    {
        // Create the main road
        CreateRoadPart("Road", RoadSize, this.transform.position, Quaternion.identity);

        // Create pavements on both sides of the road
        CreateRoadPart("Pavement_Left", PavementSize, new Vector3(-RoadSize.x / 2 - PavementSize.x / 2, 0, 0), Quaternion.identity);
        CreateRoadPart("Pavement_Right", PavementSize, new Vector3(RoadSize.x / 2 + PavementSize.x / 2, 0, 0), Quaternion.identity);

        // Create road markings
        for (float i = -RoadSize.z / 2; i < RoadSize.z / 2; i += MarkingSize.z + MarkingSpacing)
        {
            CreateRoadPart("Marking", MarkingSize, new Vector3(0, 0.1f, i), Quaternion.identity);
        }
    }

    void CreateRoadPart(string name, Vector3 size, Vector3 position, Quaternion rotation)
    {
        GameObject roadPart = new GameObject(name);
        roadPart.transform.SetParent(this.transform);
        roadPart.transform.localPosition = position;
        roadPart.transform.localRotation = rotation;
        roadPart.transform.localScale = new Vector3(1, 1, 1); // Scale set to 1, 1, 1 by default

        // Generate the cube mesh for this road part
        Cube.CreateCube(roadPart, size.z, size.y, size.x); // Assuming CreateCube is a static method in Cube.cs

        // Create a new material and assign a color based on the road part type
        Material material = new Material(Shader.Find("Standard")); // Using the Standard shader
        if (name.Contains("Road"))
        {
            material.color = Color.black; // Road color
        }
        else if (name.Contains("Pavement"))
        {
            material.color = Color.gray; // Pavement color
        }
        else if (name.Contains("Marking"))
        {
            material.color = Color.yellow; // Road marking color
        }

        // Assign the material to the MeshRenderer
        MeshRenderer meshRenderer = roadPart.GetComponent<MeshRenderer>();
        if (meshRenderer != null)
        {
            meshRenderer.material = material;
        }
    }
}



