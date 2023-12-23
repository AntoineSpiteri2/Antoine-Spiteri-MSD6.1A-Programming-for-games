using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class GenerateBuilding : MonoBehaviour
{

    public Vector3 BuildingSize = new Vector3(25, 25, 25);


    // Start is called before the first frame update
    void Start()
    {
        CreateRoadPart("House", BuildingSize, new Vector3(0,0,0));
    }


     public void CreateRoadPart(string name, Vector3 size, Vector3 position)
    {
        GameObject roadPart = new GameObject(name);
        roadPart.transform.SetParent(this.transform, false);
        roadPart.transform.localPosition = position;
        roadPart.transform.localScale = size;

        // Add the Cube component, which will automatically create the cube mesh
        Cube cubeScript = roadPart.AddComponent<Cube>();

        // Create and assign a material with a color based on the road part type
        Material material = new Material(Shader.Find("Standard"));
        if (name.Contains("House"))
        {
            material.color = Color.black; // Road color
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
