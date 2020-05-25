using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public Node GridPosition { get; set; }

    public string Type;

    public void Setup(Node gridPosition, Vector3 worldPos, Transform parent)
    {
        GridPosition = gridPosition;
        transform.position = worldPos;
        transform.SetParent(parent);
    }
}
