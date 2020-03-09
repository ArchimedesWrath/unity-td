using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{

    [SerializeField]
    private GameObject EnemyPrefab = null;

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            // Spawn a sheep at the current mouse point
            EnemyManager.Instance.SpawnEnemy(EnemyPrefab);
        }
    }

    

}
