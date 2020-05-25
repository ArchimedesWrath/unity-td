using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    private static LevelManager _instance;
    public static LevelManager Instance { get { return _instance; } }
    public float TileSize { get { return tileSize; }  }
    private float tileSize;

    public GameObject[] TilePrefabs;
    public GameObject WayPoint;
    public GameObject StartPortalPrefab;
    public GameObject EndPortalPrefab;
    public GameObject StartPortal;
    public GameObject EndPortal;

    int[,] mapData = new int[,]
        {
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,7,1,1,1,3,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,6,0,0,0,0,0,0,2,1,8,0,0},
            {0,0,0,0,0,0,6,0,0,0,0,2,1,5,0,0,0,0},
            {0,0,0,0,0,0,6,0,0,0,0,6,0,0,0,0,0,0},
            {0,0,0,0,0,0,4,1,1,1,1,5,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        };

    // 0 = Placeable Tile 
    // 1 = Path Tile
    // 2 = Start Portal
    // 3 = End Portal

    public int[,] Tiles;
    public List<GameObject> orderedCheckPoints = new List<GameObject>();
    public Dictionary<Vector2, PlaceableTile> TileDict = new Dictionary<Vector2, PlaceableTile>();
    private Dictionary<Vector2, GameObject> PathDict = new Dictionary<Vector2, GameObject>();

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        } else {
            _instance = this;
        }
    }
    
    private void Start()
    {
        tileSize  = TilePrefabs[0].GetComponent<SpriteRenderer>().sprite.bounds.size.x;

        DrawMap(mapData);

        List<GameObject> checkPoints = SetCheckPoints();
        orderedCheckPoints = OrderCheckPoints(checkPoints, StartPortal);
        StartCoroutine(ToggleCheckPoints());
        
    }

    private void DrawMap(int[,] mapData)
    {

        Tiles = new int[mapData.GetLength(0), mapData.GetLength(1)];

        Vector3 worldStart = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height));
        for (int y = 0; y < mapData.GetLength(0); y++)
        {
            for(int x = 0; x < mapData.GetLength(1); x++)
            {                
                PlaceTile(mapData[y,x], x, y, worldStart);
            }
        }
    }

    private IEnumerator ToggleCheckPoints()
    {
        foreach (GameObject checkPoint in orderedCheckPoints)
        {
            checkPoint.SetActive(false);
            yield return new WaitForSeconds(0.5f);
        }
    }

    private List<GameObject> OrderCheckPoints(List<GameObject> unorderedCheckPoints, GameObject currentCheckPoint)
    {
        if (unorderedCheckPoints.Count == 0)
        {
            // Add the last object to the list and return the ordered list.
            orderedCheckPoints.Add(currentCheckPoint);
            return orderedCheckPoints;
        }
        
        float minDistance = 1000000f;
        GameObject newClosest = null;
        foreach (GameObject checkPoint in unorderedCheckPoints)
        {
            float dist = Mathf.Abs(Vector2.Distance(currentCheckPoint.transform.position, checkPoint.transform.position));
            if (dist < minDistance)
            {
                newClosest = checkPoint;
                minDistance = dist;
            }
        }

        unorderedCheckPoints.Remove(newClosest);
        orderedCheckPoints.Add(newClosest);
        return OrderCheckPoints(unorderedCheckPoints, newClosest);
    }

    private List<GameObject> SetCheckPoints()
    {
        List<GameObject> checkPoints = new List<GameObject>();
        int checkPointNum = 0;

        for (int y = 0; y < Tiles.GetLength(0); y++)
        {
            for (int x = 0; x < Tiles.GetLength(1); x++)
            {
                if (Tiles[y,x] == 1)
                {
                    GameObject checkPoint = Instantiate(WayPoint, PathDict[new Vector2(y, x)].transform);
                    checkPoint.name = "CheckPoint " + checkPointNum;
                    checkPoints.Add(checkPoint);
                    checkPointNum++;
                }  else if (Tiles[y,x] == 2) {
                    StartPortal = Instantiate(StartPortalPrefab, PathDict[new Vector2(y, x)].transform);
                } else if (Tiles[y,x] == 3) {
                    EndPortal = Instantiate(EndPortalPrefab, PathDict[new Vector2(y, x)].transform);
                }
            }
        }

        return checkPoints;
    }

    private void PlaceTile(int tileType, int x, int y, Vector3 worldStart)
    {

        // int tileIndex = (tileType < 9) ? tileType : (Random.Range(0, TilePrefabs.Length) < 9) ? 0 : Random.Range(9, TilePrefabs.Length);
        GameObject newTile;
        
        if (tileType > 0)
        {
            if (tileType == 7) Tiles[y,x] = 2;
            if (tileType == 8) Tiles[y,x] = 3;
            if (tileType > 0 && tileType < 7) Tiles[y,x] = 1;

            // For now we are instantiating all Paths as a generic placeholer asset.
            newTile = Instantiate(TilePrefabs[1]);

            newTile.GetComponent<Tile>().Setup(new Node(x, y), new Vector3(worldStart.x + (TileSize * x + TileSize / 2), worldStart.y - (TileSize * y + TileSize / 2), 0), this.transform);
            PathDict.Add(new Vector2(y,x), newTile);
        } 
        else
        {
            Tiles[y,x] = 0;
            newTile = Instantiate(TilePrefabs[tileType]);
            PlaceableTile newTileScript = newTile.GetComponent<PlaceableTile>();
            newTileScript.Setup(new Node(x, y), new Vector3(worldStart.x + (TileSize * x + TileSize / 2), worldStart.y - (TileSize * y + TileSize / 2), 0), this.transform);
            TileDict.Add(new Vector2(y,x), newTileScript);
        }
    }
}


