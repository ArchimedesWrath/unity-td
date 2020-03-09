﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> wayPoints;

    [SerializeField]
    private Transform currentWaypoint = null;

    [SerializeField]
    private int currentWaypointIndex;

    [SerializeField]
    private int health;

    [SerializeField]
    private bool isAlive = false;


    private Rigidbody2D rb = null;

    private float speed = 3.0f;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        isAlive = true;
    }

    void FixedUpdate()
    {
        Vector3 dir = currentWaypoint.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        float dist = Vector2.Distance(transform.position, currentWaypoint.position);

        if (dist > 0.1f) transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
        
        if (dist <= 0.1f) SetNextWayPoint();
    }

    void SetNextWayPoint()
    {
        if (currentWaypoint == null)
        {
            currentWaypointIndex = 0;
        } 
        else 
        {
            if (currentWaypointIndex < wayPoints.Count - 1)
            {
                currentWaypointIndex += 1;
            } 
            else 
            {
                // Go find the end!
                // CHANGE THIS
                currentWaypointIndex = 0;
            }
        }

        currentWaypoint = wayPoints[currentWaypointIndex].transform;
    }

    public void SetWayPoint(List<GameObject> wayPointList)
    {
        wayPoints = wayPointList;
        SetNextWayPoint();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log(col);
    }
}