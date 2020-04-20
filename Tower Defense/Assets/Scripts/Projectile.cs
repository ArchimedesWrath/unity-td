using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <notes>
/// I need to think more about future types of projectiles:
/// - Missles, AoE Projectiles, Projectiles that seek towards a static location and not a moving target, etc.
///
/// </notes>

public class Projectile : MonoBehaviour
{
    private GameObject target = null;
    private float speed = 10f;
    private int projectileDamage = 0;

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    private void FixedUpdate()
    {
        if (target)
        {
            // Move towards the target 
            SeekTarget();
        } else 
        {
            // If the target no longer exists then we need to destroy the game object.
            Destroy(gameObject);
        }
    }
    
    // The projectile will do a simple distance calc if it gets within range of the target it will detonate 
    // and apply damage to the target via the WaveManage Instance.
    private void SeekTarget()
    {
        Vector3 dir = target.transform.position - transform.position; // I don't think this is nessiary.
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        float dist = Vector2.Distance(transform.position, target.transform.position);

        if (dist > 0.1f)
        {
            transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
        }
        else 
        {
            // Destroy game object and apply damage.
            DamageTarget();
        }
    }

    private void DamageTarget()
    {
        // Do we want to check that the projectile damage is a non null value?
        WaveManager.Instance.enemies[target].DamageEnemy(projectileDamage);
        Destroy(gameObject);
        // TODO: Add destruction particles here.
    }    

    public void SetupProjectile(GameObject Target, int damage)
    {
        target = Target;
        projectileDamage = damage;
    }

}
