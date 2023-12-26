using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class TerrainGen : MonoBehaviour
{

    // reference  https://www.youtube.com/watch?v=vFvwyu_ZKfU 
    public int depth = 20;

    public int width = 256;
    public int height = 256;

    public float scale = 20f;

    public float offsetx = 100f;

    public float offsety = 100f;


    public GameObject tree;

    public GameObject rock;

    public GameObject cloud;


    public int NumberoFTrees = 100;
    public int NumberoFRocks = 100;
    public int NumberoFClouds = 100;



    public float slopthreshold = 10f;

    public float threshold = 0.1f; // Define how low a point must be to be considered a "low point"



    private void Start()
    {

        offsetx = Random.Range(0f, 999f);
        offsety = Random.Range(0f, 999f);

        Terrain terrain =  GetComponent<Terrain>();

        terrain.terrainData = GenerateTerrain(terrain.terrainData);


        PlaceTrees(terrain, tree, NumberoFTrees);
        PlaceRocks(terrain, rock, NumberoFRocks);
        PlaceClouds(terrain, cloud, NumberoFClouds);
        PlaceGrass(terrain);
    }

    private void PlaceGrass(Terrain terrain)
    {
        TerrainData terrainData = terrain.terrainData;

        int detailLayer = 0;



        // Get the width and height of the detail layer
        int detailWidth = terrainData.detailWidth;
        int detailHeight = terrainData.detailHeight;

        // Create a new detail map with the desired density
        int[,] detailMap = new int[detailWidth, detailHeight];

        for (int y = 0; y < detailHeight; y++)
        {
            for (int x = 0; x < detailWidth; x++)
            {
                // Set the density of the detail object (e.g., 100 for maximum density)
                detailMap[x, y] = 100;
            }
        }


            terrainData.SetDetailLayer(0, 0, detailLayer, detailMap);
            terrain.Flush();
            terrainData.SetDetailLayer(0, 0, detailLayer+ 1, detailMap);
            terrain.Flush();
            terrainData.SetDetailLayer(0, 0, detailLayer + 2, detailMap);
            terrain.Flush();
            terrainData.SetDetailLayer(0, 0, detailLayer + 3, detailMap);
            terrain.Flush();

    }

    private void PlaceClouds(Terrain terrain, GameObject clouds, int numberoFclouds)
    {
        for (int i = 0; i < numberoFclouds; i++)
        {
            float x = Random.Range(0, terrain.terrainData.size.x);
            float z = Random.Range(0, terrain.terrainData.size.z);
            float y = terrain.SampleHeight(new Vector3(x, 0, z));

            Vector3 rockPosition = new Vector3(x, y + 50f, z) + terrain.transform.position;
            Instantiate(clouds, rockPosition, Quaternion.identity);

        }
    }

    private void PlaceRocks(Terrain terrain, GameObject rock, int numberofrocks)
    {


        for (int i = 0; i < numberofrocks; i++)
        {
            float x = Random.Range(0, terrain.terrainData.size.x);
            float z = Random.Range(0, terrain.terrainData.size.z);
            float y = terrain.SampleHeight(new Vector3(x, 0, z));

                Vector3 rockPosition = new Vector3(x, y, z) + terrain.transform.position;
                Instantiate(rock, rockPosition, Quaternion.identity);
            
        }
    }



  

    TerrainData GenerateTerrain (TerrainData TerrainData)
    {
        TerrainData.heightmapResolution = width + 1;
        TerrainData.size = new Vector3(width, depth, height);

        TerrainData.SetHeights(0, 0, generateHeights());

        return TerrainData;
    }

    float[,] generateHeights ()
    {
        float[,] heights = new float[width, height];

        for (int x =0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                heights[x, y] = CalucateHeight(x, y);
            }
        }

        return heights;
    }



    float CalucateHeight( int x, int y)
    {
        float xcord = (float)x/width * scale + offsetx;
        float ycord = (float)y /height * scale + offsety;

        return Mathf.PerlinNoise(xcord, ycord);

    }

    void PlaceTrees(Terrain terrain, GameObject treePrefab, int numberOfTrees)
    {
        for (int i = 0; i < numberOfTrees; i++)
        {
            float x = Random.Range(0, terrain.terrainData.size.x);
            float z = Random.Range(0, terrain.terrainData.size.z);
            float y = terrain.SampleHeight(new Vector3(x, 0, z));

            float normY = y / terrain.terrainData.size.y;
            if (normY < slopthreshold)
            {
                Vector3 treePosition = new Vector3(x, y, z) + terrain.transform.position;
                Instantiate(treePrefab, treePosition, Quaternion.identity);
            }
        }
    }


}
