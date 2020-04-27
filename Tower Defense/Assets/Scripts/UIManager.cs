using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager Instance { get { return _instance; } }
    private Text livesText;
    private Text waveText;
    private Text enemyCountText;

    public GameObject LivesUI;
    public GameObject WaveUI;
    public GameObject EnemyCountUI;

    public GameObject TowerStatsUI;
    public GameObject EnemyStatsUI;

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
        livesText = LivesUI.GetComponent<Text>();
        waveText = WaveUI.GetComponent<Text>();
        enemyCountText = EnemyCountUI.GetComponent<Text>();
    }
    
    public void UpdateGameUI(int enemyCount, int waveNumber, int lives)
    {
        livesText.text = lives.ToString();
        waveText.text = waveNumber.ToString();
        enemyCountText.text = enemyCount.ToString();
    }

    public void ToggleTowerStatsUI()
    {
        EnemyStatsUI.SetActive(false);
        TowerStatsUI.SetActive(true);
    }

    public void ToggleEnemyStatsUI()
    {
        EnemyStatsUI.SetActive(true);
        TowerStatsUI.SetActive(false);
    }
}



