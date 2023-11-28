using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StrightRoad : MonoBehaviour
{
    // Default values as seen in the screenshot
    public Vector3 RoadSize = new Vector3(20, 0.5f, 10);
    public Vector3 PavementSize = new Vector3(20.05f, 1, 2.5f);
    public Vector3 MarkingSize = new Vector3(3, 0.75f, 0.25f);






    void Start()
    {
        // Create the main road
        CreateRoadPart("Road", RoadSize, this.transform.position);

        // Create pavements on both sides of the road
        CreateRoadPart("Pavement_Left", PavementSize,  new Vector3 (0,0,-12));
        CreateRoadPart("Pavement_Right", PavementSize,  new Vector3(0, 0, 12));

        CreateRoadPart("Marking", MarkingSize, new Vector3(0, 0, 0));
        CreateRoadPart("Marking", MarkingSize, new Vector3(15, 0, 0));
        CreateRoadPart("Marking", MarkingSize, new Vector3(-15, 0, 0));


    }

    void CreateRoadPart(string name, Vector3 size, Vector3 position)
    {
        GameObject roadPart = new GameObject(name);
        roadPart.transform.SetParent(this.transform, false);
        roadPart.transform.localPosition = position;
        roadPart.transform.localScale = size;

        // Add the Cube component, which will automatically create the cube mesh
        Cube cubeScript = roadPart.AddComponent<Cube>();

        // Create and assign a material with a color based on the road part type
        Material material = new Material(Shader.Find("Standard"));
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
            material.color = Color.yellow; // Marking color
        }

        // Add a MeshRenderer component if not present and assign the material
        MeshRenderer meshRenderer = roadPart.GetComponent<MeshRenderer>();
        if (meshRenderer == null)
        {
            meshRenderer = roadPart.AddComponent<MeshRenderer>();
        }
        meshRenderer.material = material;
    }

}



