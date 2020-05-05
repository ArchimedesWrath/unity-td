using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The WaveManager class probably has too much functionality as it stands.
// It is responsible for starting the wave, spawning enemies, Updating UI, removing lives,
// and ending the game. 
// I will need to decouple this in the future. 
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

    [SerializeField]
    private int enemyCount = 15;
    private int enemiesLeft = 0;
    [SerializeField]
    private int lives = 20; // THIS WILL EVENTUALLY CHANGE I CAN WRITE GOOD CODE I SWEAR

    // Spawning Variables
    public bool isSpawning = false;
    private int wave = 0;

    private static WaveManager _instance;

    public static WaveManager Instance { get { return _instance; } }

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
        wayPoints.AddRange(GameObject.FindGameObjectsWithTag("Waypoint"));
        wayPoints.Add(EndPortal);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
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
        wave++;
        enemyCount = 15;
        enemiesLeft = enemyCount;
        isSpawning = true;

        // Should set the UI here?
        UIManager.Instance.UpdateGameStatsUI(enemiesLeft, wave, lives);

        for (int i = 0; i < enemyCount; i ++)
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
        enemiesLeft--; // I think not a great way to solve this problem...
        UIManager.Instance.UpdateGameStatsUI(enemiesLeft, wave, lives);
    }

    public void TakeLife()
    {
        lives--;
        UIManager.Instance.UpdateGameStatsUI(enemiesLeft, wave, lives);
    }

}
