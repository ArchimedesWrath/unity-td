using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager: MonoBehaviour
{
    [SerializeField]
    private List<GameObject> wayPoints = null;

    [SerializeField]
    private List<GameObject> enemies = null;

    [SerializeField]
    private GameObject StartPortal = null;

    [SerializeField]
    private GameObject EndPortal = null;

    private static EnemyManager _instance;

    public static EnemyManager Instance { get { return _instance; } }

    public void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        } else {
            _instance = this;
        }
    }

    void Start()
    {
        wayPoints.AddRange(GameObject.FindGameObjectsWithTag("Waypoint"));
        wayPoints.Add(EndPortal);
    }

    public void SpawnEnemy(GameObject enemyPrefab)
    {
        GameObject enemy = Instantiate(enemyPrefab, StartPortal.transform);
        enemy.GetComponent<Enemy>().SetWayPoint(wayPoints);

        // Add enemy to enemy list
        AddEnemy(enemy);
    }

    private void AddEnemy(GameObject enemy)
    {
        enemies.Add(enemy);
    }

    public void RemoveEnemey(GameObject enemy)
    {
        enemies.Remove(enemy);
    }

}
