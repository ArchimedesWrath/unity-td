using System.Collections;
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

    public int Level = 1;
    
    public int Health;
    public int MaxHealth;
    public string Name = "Sheep";
    public string Type = "Animal";

    [SerializeField]
    public bool isAlive = false;


    private Rigidbody2D rb = null;

    private float speed = 3.0f;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        isAlive = true;

        // TODO: Change this eventually
        Health = 10;
        MaxHealth = Health;
    }

    void FixedUpdate()
    {
        // TODO: This should be coupled into a MoveTowardsWaypoint function.
        Vector3 dir = currentWaypoint.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        float dist = Vector2.Distance(transform.position, currentWaypoint.position);

        if (dist > 0.1f) transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
        
        if (dist <= 0.1f) SetNextWayPoint();

        if (!isAlive) KillEnemy(null);
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

    // TODO: This can be modified to take data during damage event to add status to enemy.
    public void DamageEnemy(int damage, Tower tower)
    {
        Health -= damage;
        if (UIManager.Instance.CurrentEnemy == this.gameObject) UIManager.Instance.UpdateEnemyHealthUI(this);

        if (Health <= 0) KillEnemy(tower);
    }

    public void KillEnemy(Tower tower)
    {
        if (isAlive)
        {
            isAlive = false;
            
            if (UIManager.Instance.CurrentEnemy == this.gameObject) UIManager.Instance.ToggleStatsUIOff();

            // If the enemy was killed by a tower award xp to that tower.
            // Need to account for when a projectile is still in the air but we delete the tower before collision. MIGHT want to handle this on projecile side...
            if (tower)
            {
                tower.AddExp( (Level * 2) + 1 );
            }

            WaveManager.Instance.RemoveEnemey(gameObject);
            Destroy(gameObject);

        } else if (!isAlive)
        {
            // Something went wrong...
            Debug.Log($"{this.gameObject.name} has to be destroyed.");
            WaveManager.Instance.RemoveEnemey(gameObject);
            Destroy(gameObject);
        }
        
    }
}
