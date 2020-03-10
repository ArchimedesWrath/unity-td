using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TowerManager : MonoBehaviour {
    
    public Dictionary<Node, GameObject> Towers = new Dictionary<Node, GameObject>();
    public Dictionary<GameObject, Node> Nodes = new Dictionary<GameObject, Node>();

    public GameObject currentNode = null;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
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
    }

}