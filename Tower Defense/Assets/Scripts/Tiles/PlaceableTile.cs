using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceableTile : Tile
{
    // Grass tiles can have towers placed on them.
    public GameObject tower = null;

    public bool PlaceTower(GameObject Tower)
    {
        if (tower) 
        {
            return false;
        }
        else {
            tower = Tower;
            return true;
        } 
    }
    
    public bool HasTower()
    {
        if (tower) return true;
        else return false;
    }

    public bool RemoveTower()
    {
        if (tower)
        {
            tower = null;
            return true;
        } else {
            return false;
        }
    }
}
