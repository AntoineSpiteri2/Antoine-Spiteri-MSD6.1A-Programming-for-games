using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using UnityEngine;

public class Plusroad : MonoBehaviour
{



    private void Start()
    {
        CreateRoadPart("Road", new Vector3(10, 0.5f, 10), new Vector3(0,0,0));

        CreateRoadPart("Road", new Vector3(10, 0.5f, 10), new Vector3(20, 0, 0));

        CreateRoadPart("Road", new Vector3(10, 0.5f, 10), new Vector3(-20, 0, 0));

        CreateRoadPart("Road", new Vector3(10, 0.5f, 10), new Vector3(0, 0, 20));

        CreateRoadPart("Road", new Vector3(10, 0.5f, 10), new Vector3(0, 0, -20));


        CreateRoadPart("Pavement 1", new Vector3(10, 1, 2.5f), new Vector3(20, 0, -12));

        CreateRoadPart("Pavement 2", new Vector3(10, 1, 2.5f), new Vector3(20, 0, 10));


        CreateRoadPart("Pavement 3", new Vector3(10, 1, 2.5f), new Vector3(-20, 0, -12));

        CreateRoadPart("Pavement 4", new Vector3(10, 1, 2.5f), new Vector3(-20, 0, 10));


        CreateRoadPart("Pavement 5", new Vector3(2.5f, 1, 11f), new Vector3(-12, 0, 19));

        CreateRoadPart("Pavement 6", new Vector3(2.5f, 1, 10f), new Vector3(-12, 0, -20));


        CreateRoadPart("Pavement 7", new Vector3(2.5f, 1, 11f), new Vector3(10, 0, 19));
        CreateRoadPart("Pavement 8", new Vector3(2.5f, 1, 10f), new Vector3(10, 0, -20));


        CreateRoadPart("Marking", new Vector3(0.25f, 0.75f, 3), new Vector3(0, 0, 20));

        CreateRoadPart("Marking", new Vector3(0.25f, 0.75f, 3), new Vector3(0, 0, -20));

        CreateRoadPart("Marking", new Vector3(3, 0.75f, 0.25f), new Vector3(20, 0, 0));

        CreateRoadPart("Marking", new Vector3(3, 0.75f, 0.25f), new Vector3(-20, 0, 0));





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
            material.color = UnityEngine.Color.black; // Road color
            roadPart.tag = "FoundLoc";

        }
        else if (name.Contains("Pavement"))
        {
            material.color = UnityEngine.Color.gray; // Pavement color
        }
        else if (name.Contains("Marking"))
        {
            material.color = UnityEngine.Color.yellow; // Marking color
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
