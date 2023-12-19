using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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


        if (other.gameObject.tag == "FoundLoc")
        {
            int index = gameObjects.IndexOf(other.gameObject);

            gameObjects.RemoveAt(index);


            MeshRenderer meshRenderer = other.GetComponent<MeshRenderer>();


            other.gameObject.tag = "Untagged";
            meshRenderer.material.color = UnityEngine.Color.green;

            if (gameObjects.Count == 0)
            {
                if (SceneManager.GetActiveScene().name != "task3.2 level 2") {
                    LoadNewScene("task3.2 level 2");
                }
            }

        }

    }

    public void LoadNewScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }



}
