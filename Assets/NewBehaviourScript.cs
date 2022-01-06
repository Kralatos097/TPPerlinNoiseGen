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

    [Range(0.01f,1)]
    public float zoom;

    public float xOffset;
    public float yOffset;

    [Range(0,1)]
    public float floorValue = 0.6f;

    public Tilemap Tilemap;
    public Tilemap OverTilemap;

    public Tile blackTile;
    public Tile whiteTile;
    public Tile GreenTile;
    public Tile LianeTile;

    private float xSeed;
    private float ySeed;

    // Start is called before the first frame update
    void Update()
    {
        System.Random rnd = new System.Random(seed);

        xSeed = (float)rnd.NextDouble();
        ySeed = (float)rnd.NextDouble();

        for (int y = 0; y < ySize; y++)
        {
            for (int x = 0; x < xSize; x++)
            {
                float xCoord = (xSeed + x) * zoom + xOffset;
                float yCoord = (ySeed + y) * zoom + yOffset;
                
                float pNValue = Mathf.PerlinNoise(xCoord, yCoord);

                //Debug.Log(pNValue + " / " + xSeed+x + " / " + ySeed+y);
                
                Vector3Int pos = new Vector3Int(x, y,0);
                if(pNValue > floorValue)
                {
                    if (pNValue > 0.85f)
                    {
                        OverTilemap.SetTile(pos, LianeTile);
                    }
                    else
                    {
                        OverTilemap.SetTile(pos, null);
                    }
                    Tilemap.SetTile(pos, blackTile);
                }
                else
                {
                    if(pNValue < 0.1f)
                    {
                        OverTilemap.SetTile(pos, GreenTile);
                    }
                    else
                    {
                        OverTilemap.SetTile(pos, null);
                    }
                    Tilemap.SetTile(pos, whiteTile);
                }
                
            }
        }
    }
}
