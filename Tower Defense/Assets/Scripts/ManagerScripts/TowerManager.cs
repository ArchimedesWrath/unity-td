using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TowerManager : MonoBehaviour {
    
    public Dictionary<PlaceableTile, GameObject> Towers = new Dictionary<PlaceableTile, GameObject>();
    public Dictionary<GameObject, PlaceableTile> Nodes = new Dictionary<GameObject, PlaceableTile>();
    public GameObject towerPrefab = null;

    private static TowerManager _instnace;
    public static TowerManager Instance { get { return _instnace; } }

    private void Awake() 
    {
        if (_instnace != null && _instnace != this)
        {
            Destroy(gameObject);
        } else {
            _instnace = this;
        }
    }

    private void Start()
    {
        GameObject[] nodes = GameObject.FindGameObjectsWithTag("Node");
        
        foreach(GameObject node in nodes)
        {
            Nodes.Add(node, node.GetComponent<PlaceableTile>());
        }
    }

    public PlaceableTile GetNode(GameObject node)
    {
        return Nodes[node];
    }

    public GameObject GetTower(GameObject node)
    {
        return Towers[GetNode(node)];
    }

    // Function first checks to see if ndoe has a tower on it. 
    // If no tower is on  then it should check the player's money and decide if a tower should be built.
    // TODO: Either rename this function or Node.PlaceTower to prevent ambiguous names.
    public bool PlaceTower(GameObject tower, GameObject node)
    {
        if (Nodes[node].HasTower())
        {
            // Then the node already has a tower and cannot place another one.
            Debug.Log("This node already has a tower!");
            return false;
        } else 
        {
            // If not then place a tower using the Node.PlaceTower() function.
            GameObject newTower = (GameObject)Instantiate(tower, node.transform);
            // Add tower and associated node to Towers Dict
            PlaceableTile nodeScript = Nodes[node];
            nodeScript.PlaceTower(newTower);
            Towers.Add(nodeScript, newTower);

            // Set new tower as a child of the TowerManager GO for organization.
            newTower.transform.SetParent(transform, true);

            return true;

        }
    }

    // Takes a noded and removes the tower from that node.
    public bool RemoveTower(GameObject node)
    {
        if (Nodes[node].HasTower())
        {
            // Remove the tower
            // Remove tower from node
            Nodes[node].RemoveTower();
            // Delete tower from game || In the future this will be a different mechanic.
            // Do I have the node delete this tower??
            Destroy(Towers[Nodes[node]]);
            // Remove tower from dict
            Towers.Remove(Nodes[node]);

            return true;

        } 
        else 
        {
            Debug.Log("There is no tower on this node!");
            return false;
        }
    }

    /*
        IF a node is selected 
            - Check if Node has a Tower
                - If it DOES HAVE a tower set UI elements = the Tower
                - If it DOES NOT HAVE a tower set UI elements = Build Menu
        
        FOR NOW:::
            - Check if Node has a Tower
                - If it DOES HAVE a tower log 'Tower is already in place:' TOWER ... Make sure it is not adding another tower
                - If it DOES NOT HAVE a tower - Add a tower to that node and populate data. 
    */

}
