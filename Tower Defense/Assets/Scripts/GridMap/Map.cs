using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Map : MonoBehaviour
{
    private int width;
    private int height;
    public float TileSize { get { return tileSize; }  }
    private float tileSize;
    private int[,] map;

    public GameObject[] Tiles;

    public Tile testTile;

    
    void Start()
    {
        width = 18;
        height = 10;
        tileSize = 32;
        map = new int[width, height];
        tileSize  = Tiles[0].GetComponent<SpriteRenderer>().sprite.bounds.size.x;

        DrawMap();
    }

    private void DrawMap()
    {
        string[] mapData = new string[]
        {
            "0,0,0,0,0,0,0,0,9,0,0,0,0,0,0,0,0,0",
            "0,9,0,9,0,0,0,9,0,0,9,0,0,0,0,0,9,0",
            "0,0,7,1,1,1,3,0,0,0,0,0,0,0,0,0,9,0",
            "0,0,0,0,0,0,6,0,0,9,0,0,0,2,1,8,0,0",
            "9,0,0,0,0,0,6,0,0,0,0,2,1,5,0,0,0,0",
            "0,0,9,0,0,0,6,9,0,9,0,6,0,0,0,0,9,0",
            "0,0,0,0,0,0,4,1,1,1,1,5,0,0,0,0,0,0",
            "0,0,9,9,0,0,0,0,0,0,0,0,0,9,0,0,0,0",
            "0,0,0,0,0,0,0,0,9,0,0,0,0,0,0,0,0,0",
            "0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,9,0",
        };

        int mapY = mapData.Length;

        Vector3 worldStart = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height));
        for (int y = 0; y < mapY; y++)
        {
            string [] newTiles = mapData[y].Split(',');
            for(int x = 0; x < newTiles.Length; x++)
            {
                PlaceTile(newTiles[x], x, y, worldStart);
            }
        }
    }

    private void PlaceTile(string tileType, int x, int y, Vector3 worldStart)
    {

        int tileIndex = (int.Parse(tileType) < 9) ? int.Parse(tileType) : (Random.Range(0, Tiles.Length) < 9) ? 0 : Random.Range(9, Tiles.Length);

        GameObject newTile = Instantiate(Tiles[tileIndex]);

        newTile.GetComponent<Tile>().Setup(new Node(x, y), new Vector3(worldStart.x + (TileSize * x + TileSize / 2), worldStart.y - (TileSize * y + TileSize / 2), 0), this.transform);

    }

}


