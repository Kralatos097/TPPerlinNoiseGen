using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class NewBehaviourScript : MonoBehaviour
{
    public int seed;

    public int xSize;
    public int ySize;

    [Range(0.01f,50)]
    public float zoom;

    public float xOffset;
    public float yOffset;

    [Range(0,1)]
    public float floorValue = 0.6f;
    
    [Range(0,1)]
    public float floorValueB = 0.6f;

    public Tilemap Tilemap;
    public Tilemap OverTilemap;

    public Tile blackTile;
    public Tile whiteTile;
    public Tile GreenTile;
    public Tile LianeTile;

    private float xSeed;
    private float ySeed;
    private float xSeedB;
    private float ySeedB;

    private int[,] grid;

    // Start is called before the first frame update
    void Update()
    {
        Tilemap.ClearAllTiles();
        OverTilemap.ClearAllTiles();
        grid = new int[xSize,ySize];
        System.Random rnd = new System.Random(seed);

        xSeed = rnd.Next(50)+(float)rnd.NextDouble();
        ySeed = rnd.Next(50)+(float)rnd.NextDouble();
        
        xSeedB = rnd.Next(50)+(float)rnd.NextDouble();
        ySeedB = rnd.Next(50)+(float)rnd.NextDouble();

        for (int y = 0; y < ySize; y++)
        {
            for (int x = 0; x < xSize; x++)
            {
                float xCoord = (xSeed + x) / zoom + xOffset;
                float yCoord = (ySeed + y) / zoom + yOffset;
                
                float pNValue = Mathf.PerlinNoise(xCoord, yCoord);
                
                float xCoordB = (xSeedB + x) / zoom + xOffset;
                float yCoordB = (ySeedB + y) / zoom + yOffset;
                
                float pNValueBis = Mathf.PerlinNoise(xCoordB, yCoordB);

                //Debug.Log(pNValue + " / " + pNValueBis);
                
                Vector3Int pos = new Vector3Int(x, y,0);
                if(pNValue > floorValue)
                {
                    Tilemap.SetTile(pos, blackTile);
                    grid[x, y] = 1;
                }
                else
                {
                    Tilemap.SetTile(pos, whiteTile);
                    grid[x, y] = 0;
                }

                if (pNValueBis < 1-floorValueB && grid[x,y] == 1)
                {
                    OverTilemap.SetTile(pos, LianeTile);
                }
                else if (pNValueBis > floorValueB && grid[x,y] == 0)
                {
                    OverTilemap.SetTile(pos, GreenTile);
                }
                else
                {
                    OverTilemap.SetTile(pos, null);
                }
                
            }
        }
    }
}
