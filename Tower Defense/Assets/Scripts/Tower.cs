
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{

    // Variables for drawing circle to act as range indicator:
    public int Segments = 50;
    public float lineWidth = 0.04f;
    public LineRenderer lineRenderer;

    // Base Tower Variables
    public GameObject projectilePrefab = null; // This does not need to exist for every tower (think AoE towers). 
    public GameObject CurrentTarget = null;
    
    [SerializeField]
    private float AttackSpeed = 1f;
    private float AttackCountdown = 0f;
    [SerializeField]
    private float AttackRange = 4f;
    [SerializeField]
    private int AttackDamage = 5;

    private void Awake() 
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetVertexCount(Segments + 1);
        lineRenderer.widthMultiplier = lineWidth;
        lineRenderer.useWorldSpace = false;
        //SetupRange(); 
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        CurrentTarget = FindTarget();

        if (AttackCountdown <= 0f)
        {
            if (CurrentTarget) Attack();
        }

        AttackCountdown -= Time.deltaTime;
    }

    // When a basic tower attacks an enemy it needs to instantiate a projectile. 
    // The projectile will then handle find ing the enemy and applying damage.
    private void Attack()
    {
        float dist = Vector2.Distance(transform.position, CurrentTarget.transform.position);

        if (dist < AttackRange)
        {
            // Spawn a projectile prefab and set values using SetupProjectile.
            SpawnProjectile();
        }
        else 
        {
            CurrentTarget = null;
        }

        AttackCountdown = 1f / AttackSpeed;

    }

    private GameObject FindTarget()
    {
        // Loop through all enenimes and find closest.
        foreach(KeyValuePair<GameObject, Enemy> enemy in WaveManager.Instance.enemies)
        {
            float dist = Vector2.Distance(transform.position, enemy.Key.transform.position);
            if (dist < AttackRange) 
            {
                return enemy.Key;
            }
        }

        return null;
    }

    private void SpawnProjectile()
    {
        GameObject projectile = Instantiate(projectilePrefab, transform);
        projectile.GetComponent<Projectile>().SetupProjectile(CurrentTarget, AttackDamage);
    }

    // TODO: Have each tower draw it's own range indicator? Or have that done by some helper class?

    // Here's what that would look like in the base Tower Class.
    private void SetupRange()
    {
        
        float x;
        float y;
        float z = 2f;

        float angle = 20f;

        for (int i = 0; i < (Segments+ 1); i++)
        {
            x = Mathf.Sin(Mathf.Deg2Rad * angle) * AttackRange;
            y = Mathf.Cos(Mathf.Deg2Rad * angle) * AttackRange;

            lineRenderer.SetPosition(i, new Vector3(x, y, z));
            angle += (360f / Segments);
        }
    }

    // private void OnDrawGizmos() 
    // {
    //     Gizmos.color = new Color(0.1f, 0.1f, 0.3f, 0.1f);
    //     Gizmos.DrawSphere(transform.position, AttackRange);    
    // }
}

