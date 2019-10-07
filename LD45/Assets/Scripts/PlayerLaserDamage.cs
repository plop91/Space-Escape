using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** Handles collisions between player lasers and destructible objects (Currently just asteroids) - Alex */

public class PlayerLaserDamage : MonoBehaviour
{
    private float range = 30f; //Range of laser
    private float damage = 155f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject != null)
        {
            if (damage > 0)
            {
                RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, Vector2.right);

                foreach (RaycastHit2D hit in hits)
                {
                    if (hit.collider != null)
                    {
                        float dist = Vector3.Distance(hit.transform.position, transform.position);
                        if (hit.collider.gameObject.name.StartsWith("Asteroid") && dist < range)
                            this.damage = hit.collider.gameObject.GetComponent<Asteroid>().TakeHit(damage);
                        else if (hit.collider.gameObject.name.StartsWith("FortTurret") && dist < range)
                            this.damage = hit.collider.gameObject.GetComponent<FortTurret>().TakeHit(damage);
                        else if (hit.collider.gameObject.name.StartsWith("EnemyShot") && dist < range)
                        {
                            hit.collider.gameObject.GetComponent<EnemyShot>().Die();
                            damage -= 10;
                        }
                        else if (hit.collider.gameObject.name.StartsWith("Enemy") && dist < range)
                        {
                            this.damage = hit.collider.gameObject.GetComponent<MovingEnemy>().TakeHit(damage);
                        }

                        if (damage <= 0)
                        {
                            break;
                        }
                    }
                }
            }
        }
    }
}
