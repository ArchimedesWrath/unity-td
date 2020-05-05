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
    private Tower tower;
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
            if (target) DamageTarget();
            else Destroy(gameObject);
        }
    }

    private void DamageTarget()
    {
        // Do we want to check that the projectile damage is a non null value?
        // I also believe that this is causing an error to occur where a projectile tries to damage an enemy that doesn't exist.
        // Here is the fix for that:
        if (WaveManager.Instance.enemies.ContainsKey(target))
        {
            WaveManager.Instance.enemies[target].DamageEnemy(projectileDamage, tower);
        }
        Destroy(gameObject);
        // TODO: Add destruction particles here.
    }    

    public void SetupProjectile(GameObject Target, int damage, Tower originTower)
    {
        target = Target;
        projectileDamage = damage;
        tower = originTower;
    }

}
