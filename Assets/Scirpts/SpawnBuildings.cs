using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnBuildings : MonoBehaviour
{

    public List<GameObject> childObjects = new List<GameObject>();
    private int amountOfChildGameObjects = 0;
    private int pickedIndex = -1; // Initialize to an invalid index
    public int maxAmountOfBuildings = 10;
    public GenerateBuilding GenerateBuilding;
    private List<int> availableNumbers;


    void Start()
    {
        availableNumbers = new List<int>();
        GameObject parentObject = gameObject; // If the script is always attached to the parent

        foreach (Transform child in parentObject.transform)
        {
            childObjects.Add(child.gameObject);
        }

        amountOfChildGameObjects = childObjects.Count;

        // Check if there are enough child objects
        if (amountOfChildGameObjects == 0)
        {
            Debug.Log("No child objects found.");
            return;
        }

        int count = 0;
        while (count < Mathf.Min(maxAmountOfBuildings, amountOfChildGameObjects))
        {
            pickedIndex = Random.Range(0, amountOfChildGameObjects);

           while (availableNumbers.Contains(pickedIndex))
            {
                pickedIndex = Random.Range(0, amountOfChildGameObjects);

            }
            availableNumbers.Add(pickedIndex);

            Debug.Log("Picked Index: " + pickedIndex);
            count++;
            childObjects[pickedIndex].AddComponent<GenerateBuilding>();



        }

    }

}
