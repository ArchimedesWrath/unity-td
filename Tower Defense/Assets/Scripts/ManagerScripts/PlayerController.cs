using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject MouseHover;
    void Update()
    {
        Vector3 mousePosScreen = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector3 screenConv = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));

        MouseHover.transform.position = new Vector2(Mathf.RoundToInt(mousePosScreen.x) + 0.5f, Mathf.RoundToInt(mousePosScreen.y) - 0.5f);

        Vector2 mouseGridPos = new Vector2(Mathf.RoundToInt(mousePosScreen.x + screenConv.x), Mathf.Abs(Mathf.RoundToInt(mousePosScreen.y - screenConv.y)));

        // Check the GridPosition the player clicked, if path then return 1 else return 0

        if (Input.GetMouseButtonDown(0)) Debug.Log(LevelManager.Instance.Tiles[(int)Mathf.Round(mouseGridPos.y), (int)Mathf.Round(mouseGridPos.x)]);

    }
}
