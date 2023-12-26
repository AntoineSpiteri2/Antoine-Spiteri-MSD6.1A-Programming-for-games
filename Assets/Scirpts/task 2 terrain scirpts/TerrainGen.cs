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



    public bool enableGrass = false;

    public bool enablerocks = false;

    public bool enableClopuds = false;

    public bool enablePath = false;

    private int[,] originalGrassMap;

    public GameObject stonePrefab; // Assign your stone prefab
    public int pathLength = 10; // How many stones in the path
    public float stoneSpacing = 1.0f; // Space between each stone




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


        GeneratePath(terrain);

        // Assuming you're using the first detail layer for grass
    }

    private void GeneratePath( Terrain terrain)
    {

        if (enablePath == true)
        {


            bool isStartOnXAxis = Random.value > 0.5f;
            float randomValue = Random.Range(0f, 255f);

            Vector3 startPosition;
            if (isStartOnXAxis)
            {
                startPosition = new Vector3(randomValue, 0, 0); // Random X, start at one edge of Z
            }
            else
            {
                startPosition = new Vector3(0, 0, randomValue); // Random Z, start at one edge of X
            }
            startPosition.y = terrain.SampleHeight(startPosition);

            float undergroundDepth = 0.9f; // Depth to lower the stones into the ground

            for (int i = 0; i < pathLength; i++)
            {
                // Calculate the position for each stone with a straight path
                Vector3 stonePosition = startPosition + (isStartOnXAxis ? new Vector3(0, 0, stoneSpacing * i) : new Vector3(stoneSpacing * i, 0, 0));

                // Introduce randomness to the stone placement
                Vector3 randomOffset = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)) * stoneSpacing * 0.3f; // Adjust the multiplier to change the variance

                // Apply the random offset to the stone position
                stonePosition += randomOffset;

                // Ensure the stone is placed at the correct height on the terrain
                stonePosition.y = terrain.SampleHeight(stonePosition) - undergroundDepth;

                // Instantiate the stone prefab
                Instantiate(stonePrefab, stonePosition, Quaternion.identity);
            }
        }
    }





    private void PlaceGrass(Terrain terrain)
    {
        if (enableGrass == true)
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
            terrainData.SetDetailLayer(0, 0, detailLayer + 1, detailMap);
            terrain.Flush();
            terrainData.SetDetailLayer(0, 0, detailLayer + 2, detailMap);
            terrain.Flush();
            terrainData.SetDetailLayer(0, 0, detailLayer + 3, detailMap);
            terrain.Flush();
        }
    }

    private void PlaceClouds(Terrain terrain, GameObject clouds, int numberoFclouds)
    {
        if (enableClopuds == true)
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
    }

    private void PlaceRocks(Terrain terrain, GameObject rock, int numberofrocks)
    {
        if (enablerocks == true)
        {




            for (int i = 0; i < numberofrocks; i++)
            {
                float x = Random.Range(0, terrain.terrainData.size.x);
                float z = Random.Range(0, terrain.terrainData.size.z);
                float y = terrain.SampleHeight(new Vector3(x, 0, z));

                Vector3 rockPosition = new Vector3(x, y , z) + terrain.transform.position;
                Instantiate(rock, rockPosition, Quaternion.identity);

            }
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

    void OnDisable()
    {
        Terrain terrain = GetComponent<Terrain>();
        TerrainData terrainData = terrain.terrainData;

        int numberOfDetailLayers = terrainData.detailPrototypes.Length;

        for (int layer = 0; layer < numberOfDetailLayers; layer++)
        {
            int[,] zeroDetailLayer = new int[terrainData.detailWidth, terrainData.detailHeight];
            terrainData.SetDetailLayer(0, 0, layer, zeroDetailLayer);
        }

    }


}
