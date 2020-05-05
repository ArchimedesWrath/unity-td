using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerBuilderUI : MonoBehaviour
{
    public Text NodeType;

    public void BuildTower()
    {
        if (TowerManager.Instance.PlaceTower(TowerManager.Instance.towerPrefab, UIManager.Instance.CurrentNode))
        {
            UIManager.Instance.UpdateGameControlsUI();
        } 
        else
        {
            // The tower wasn't built maybe we should do something here!
        }
    }
}
