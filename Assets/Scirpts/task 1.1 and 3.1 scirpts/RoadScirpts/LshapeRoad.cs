using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UIElements;

public class LshapeRoad : MonoBehaviour
{

    public Vector3 RoadRot = new Vector3(0, 90, 0);
    Vector3 newPosition;


    void Start()
    {

        //GameObject LroadPart = new GameObject("road part1");
        //LroadPart.AddComponent<StrightRoad>();
        //LroadPart.transform.SetParent(this.transform, false);
        //LroadPart.transform.localPosition = new Vector3(0, 0, 30);
        //LroadPart.transform.eulerAngles = RoadRot; // Corrected line

        //LroadPart.transform.localScale = this.gameObject.transform.localScale;



        newPosition = new Vector3(0, 0, 0);

        CreateRoadPart("Road", new Vector3(10, 0.5f, 10), newPosition);

        newPosition = new Vector3(0, 0, 0);


        CreateRoadPart("Pavement", new Vector3(10, 1, 2.5f), new Vector3(0, 0, -12));

        CreateRoadPart("Pavement 2", new Vector3(2.5f, 1, 12.5f), new Vector3(-12, 0, -2));


        GameObject LroadPart2 = new GameObject("road part2");
        LroadPart2.AddComponent<StrightRoad>();
        LroadPart2.transform.SetParent(this.transform, false);
        LroadPart2.transform.localPosition = new Vector3(30,0,0);




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
}