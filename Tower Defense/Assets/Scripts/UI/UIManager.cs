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

    public TowerStatsUI TowerStatsUI;
    public EnemyStatsUI EnemyStatsUI;
    public TowerBuilderUI TowerBuilderUI;

    public GameObject CurrentNode { 
        get { return currentNode;} 
        set { currentNode = value;} 
        }
    [SerializeField]
    private GameObject currentNode; 
    public GameObject CurrentEnemy { 
        get { return currentEnemy; } 
        set { currentEnemy = value; } 
        }
    [SerializeField]
    private GameObject currentEnemy;

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
    
    public void UpdateGameStatsUI(int enemyCount, int waveNumber, int lives)
    {
        livesText.text = lives.ToString();
        waveText.text = waveNumber.ToString();
        enemyCountText.text = enemyCount.ToString();
    }

    public void UpdateGameControlsUI()
    {
        if (currentNode)
        {
            // if current node has tower then display tower
            // else display tower builder
            if (TowerManager.Instance.Nodes[currentNode].tower)
            {
                ToggleTowerStatsUI();
            } else 
            {
                TogggleTowerBuilderUI();
            }
        }
        else if (!currentNode && currentEnemy)
        {
            // display the enemy stats
            ToggleEnemyStatsUI();
        }
        else 
        {
            // Turn off all GameControlUIs.
            ToggleStatsUIOff();
        }
    }

    private void TogggleTowerBuilderUI()
    {
        TowerBuilderUI.gameObject.SetActive(true);
        EnemyStatsUI.gameObject.SetActive(false);
        TowerStatsUI.gameObject.SetActive(false);
    }

    private void ToggleEnemyStatsUI()
    {
        TowerBuilderUI.gameObject.SetActive(false);
        EnemyStatsUI.gameObject.SetActive(true);
        TowerStatsUI.gameObject.SetActive(false);

        Enemy enemy = WaveManager.Instance.enemies[currentEnemy];
        EnemyStatsUI.EnemyIcon.sprite = enemy.gameObject.GetComponent<SpriteRenderer>().sprite;
        EnemyStatsUI.Name.text = enemy.Name;
        EnemyStatsUI.Type.text = enemy.Type;
        EnemyStatsUI.HPSlider.value = (float)enemy.Health / (float)enemy.MaxHealth;
        EnemyStatsUI.HPNumber.text = $"{enemy.Health.ToString()}/{enemy.MaxHealth.ToString()}";
    }

    public void UpdateEnemyHealthUI(Enemy enemy)
    {
        EnemyStatsUI.HPSlider.value = (float)enemy.Health / (float)enemy.MaxHealth;
    }

    public void UpdateTowerExpUI(Tower tower)
    {
        TowerStatsUI.LevelSlider.value = (float)tower.exp / (float)tower.expNextLevel;
        TowerStatsUI.Level.text = "Lvl " + tower.Level.ToString();
    }

    private void ToggleTowerStatsUI()
    {
        TowerBuilderUI.gameObject.SetActive(false);
        EnemyStatsUI.gameObject.SetActive(false);
        TowerStatsUI.gameObject.SetActive(true);

        Tower tower = TowerManager.Instance.Towers[TowerManager.Instance.Nodes[currentNode]].GetComponent<Tower>();
        TowerStatsUI.TowerIcon.sprite = tower.gameObject.GetComponent<SpriteRenderer>().sprite;
        TowerStatsUI.TowerName.text = tower.Name;
        TowerStatsUI.Level.text = "Lvl " + tower.Level.ToString();
        TowerStatsUI.LevelSlider.value = (float)tower.exp / (float)tower.expNextLevel;
        TowerStatsUI.Attack.text = "Atk " + tower.AttackDamage.ToString();
        TowerStatsUI.AttackSpeed.text = "Atk Spd " + tower.AttackSpeed.ToString();
    }

    public void ToggleStatsUIOff()
    {
        TowerBuilderUI.gameObject.SetActive(false);
        EnemyStatsUI.gameObject.SetActive(false);
        TowerStatsUI.gameObject.SetActive(false);
    }
}



