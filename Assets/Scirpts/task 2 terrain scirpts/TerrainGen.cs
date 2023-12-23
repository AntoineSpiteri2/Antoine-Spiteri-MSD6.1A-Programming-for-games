using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public int NumberoFTrees = 100;

    public float slopthreshold = 10f;


    private void Start()
    {

        offsetx = Random.Range(0f, 999f);
        offsety = Random.Range(0f, 999f);

        Terrain terrain =  GetComponent<Terrain>();

        terrain.terrainData = GenerateTerrain(terrain.terrainData);


        PlaceTrees(terrain, tree, NumberoFTrees);
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
