using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelExpolored : MonoBehaviour
{
    // Start is called before the first frame update

    public List<GameObject> gameObjects;
    void Start()
    {
        gameObjects = new List<GameObject>();

        // Find all GameObjects with the specified tag
        GameObject[] foundObjects = GameObject.FindGameObjectsWithTag("FoundLoc");

        // Add each GameObject in the array to the list
        foreach (GameObject obj in foundObjects)
        {
            gameObjects.Add(obj);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }



    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.tag);

        Debug.Log(other.gameObject.name);


        if (other.gameObject.tag == "FoundLoc")
        {
            int index = gameObjects.IndexOf(other.gameObject);

            gameObjects.RemoveAt(index);


            MeshRenderer meshRenderer = other.GetComponent<MeshRenderer>();


            other.gameObject.tag = "Untagged";
            meshRenderer.material.color = UnityEngine.Color.green;

            Debug.Log("trigger");
            if (gameObjects.Count == 0)
            {
                Debug.Log("aa");
            }

        }

    }



}
