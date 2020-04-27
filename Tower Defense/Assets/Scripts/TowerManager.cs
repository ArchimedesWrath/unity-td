using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TowerManager : MonoBehaviour {
    
    public Dictionary<Node, GameObject> Towers = new Dictionary<Node, GameObject>();
    public Dictionary<GameObject, Node> Nodes = new Dictionary<GameObject, Node>();

    public GameObject currentNode = null;
    public GameObject towerPrefab = null;

    private void Start()
    {
        GameObject[] nodes = GameObject.FindGameObjectsWithTag("Node");
        
        foreach(GameObject node in nodes)
        {
            Nodes.Add(node, node.GetComponent<Node>());
        }
    }
        
    private void Update() 
    {
        if (Input.GetMouseButtonDown(0))
        {
            checkNode();
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            // Place a tower
            PlaceTower(towerPrefab, currentNode);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            // Sell tower
            RemoveTower(currentNode);
        }
    }

    private void checkNode()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

        RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
        if(hit.collider != null)
        {
            if (hit.collider.gameObject.CompareTag("Node"))
            {
                currentNode = hit.collider.gameObject;
            }
        }
    }

    // Function first checks to see if ndoe has a tower on it. 
    // If no tower is on  then it should check the player's money and decide if a tower should be built.
    // TODO: Either rename this function or Node.PlaceTower to prevent ambiguous names.
    private void PlaceTower(GameObject tower, GameObject node)
    {
        if (Nodes[node].HasTower())
        {
            // Then the node already has a tower and cannot place another one.
            Debug.Log("This node already has a tower!");
        } else 
        {
            // If not then place a tower using the Node.PlaceTower() function.
            GameObject newTower = (GameObject)Instantiate(tower, node.transform);
            // Add tower and associated node to Towers Dict
            Node nodeScript = Nodes[node];
            nodeScript.PlaceTower(newTower);
            Towers.Add(nodeScript, newTower);

            // Set new tower as a child of the TowerManager GO for organization.
            newTower.transform.SetParent(transform, true);

            UIManager.Instance.ToggleTowerStatsUI();

        }
    }

    // Takes a noded and removes the tower from that node.
    private void RemoveTower(GameObject node)
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

            UIManager.Instance.ToggleEnemyStatsUI();

        } 
        else 
        {
            Debug.Log("There is no tower on this node!");
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
