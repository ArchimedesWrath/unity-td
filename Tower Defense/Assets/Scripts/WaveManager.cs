using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{

    [SerializeField]
    private GameObject EnemyPrefab = null;
    
    [SerializeField]
    private List<GameObject> wayPoints = null;

    [SerializeField]
    public Dictionary<GameObject, Enemy> enemies = new Dictionary<GameObject, Enemy>();

    [SerializeField]
    private GameObject StartPortal = null;

    [SerializeField]
    private GameObject EndPortal = null;


    // Spawning Variables
    public bool isSpawning = false;

    private static WaveManager _instance;

    public static WaveManager Instance { get { return _instance; } }

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        } else {
            _instance = this;
        }
    }

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    private void Start()
    {
        wayPoints.AddRange(GameObject.FindGameObjectsWithTag("Waypoint"));
        wayPoints.Add(EndPortal);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {

            if (!isSpawning)
            {
                // Start Spawn
                StartCoroutine(SpawnWave());
            }
        }
    }

    private GameObject SpawnEnemy(GameObject enemyPrefab, Transform spawnTransfom)
    {
        GameObject enemy = Instantiate(enemyPrefab, spawnTransfom);
        enemy.GetComponent<Enemy>().SetWayPoint(wayPoints);
        return enemy;
    }

    private IEnumerator SpawnWave()
    {
        isSpawning = true;
        for (int i = 0; i < 15; i ++)
        {
            GameObject enemy =  SpawnEnemy(EnemyPrefab, StartPortal.transform);
            enemy.transform.SetParent(transform, true);
            AddEnemy(enemy);

            yield return new WaitForSeconds(1f / 2f);
        }

        isSpawning = false;
    }

    private void AddEnemy(GameObject enemy)
    {
        enemies.Add(enemy, enemy.GetComponent<Enemy>());
    }

    public void RemoveEnemey(GameObject enemy)
    {
        enemies.Remove(enemy);
    }

}
