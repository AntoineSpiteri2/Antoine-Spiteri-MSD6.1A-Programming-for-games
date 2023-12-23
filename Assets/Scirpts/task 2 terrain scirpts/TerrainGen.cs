using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGen : MonoBehaviour
{
    public int depth = 20;

    public int width = 256;
    public int height = 256;

    public float scale = 20f;

    public float offsetx = 100f;

    public float offsety = 100f;




    private void Start()
    {

        offsetx = Random.Range(0f, 999f);
        offsety = Random.Range(0f, 999f);

        Terrain terrain =  GetComponent<Terrain>();

        terrain.terrainData = GenerateTerrain(terrain.terrainData);
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

}
