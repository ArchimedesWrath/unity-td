using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CheckClick();
        }   
    }

    private void CheckClick()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

        RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
        if(hit.collider != null)
        {
            if (hit.collider.gameObject.CompareTag("Node"))
            {
                // Then we hit a node and need to display either:
                // The tower buying UI
                // OR
                // The tower stats UI
            }
            else if (hit.collider.gameObject.CompareTag("Enemy"))
            {
                // Then we need to display the enemy stats UI.
            }
        }
    }
}
