using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerStatsUI : MonoBehaviour
{
    public Image TowerIcon;
    public Text TowerName;
    public Text Level;
    public Slider LevelSlider;
    public Text Attack;
    public Text AttackSpeed;

    public void SellTower()
    {
        if(TowerManager.Instance.RemoveTower(UIManager.Instance.CurrentNode))
        {
            UIManager.Instance.UpdateGameControlsUI();
        }
        else
        {
            // Then there was no tower on the selected node... There is an issue!
        }
    }
}
