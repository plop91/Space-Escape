using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FortTurret : MonoBehaviour
{
    public GameObject deathEffect;

    private GameObject player;
    public GameObject shot;

    private float angle;
    private float shotSpeed = 25f;

    private float lastShot = 1f;
    private float shotFrequency = 2f;
    private int shotsLeft = 8;

    private float health = 75f;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }


    void Update()
    {
        lastShot -= Time.deltaTime;
        if (player != null && !PauseMenu.GameIsPaused)
        {
            float dist = Vector2.Distance(player.transform.position, transform.position);

            Vector2 playerSpeed = player.GetComponent<Player>().GetSpeed();
            Vector2 target = player.transform.position;

            float y = target.y - transform.position.y;
            float x = target.x - transform.position.x;

            angle = Mathf.Atan2(y, x) * Mathf.Rad2Deg;

            if (float.IsNaN(angle))
                angle = 0;

            transform.localEulerAngles = new Vector3(0, 0, angle - 90);

            if (dist < 55f && shotsLeft > 0)
            {
                if (lastShot <= 0)
                {
                    lastShot = shotFrequency;
                    shotsLeft--;
                    Shoot();
                }
            }
        }
        
    }

    private void Shoot()
    {
        Vector3 pos = gameObject.transform.position;
        Instantiate(shot, pos, gameObject.transform.rotation);
    }

    public float TakeHit(float damage)
    {
        float oldHealth = health;
        health -= damage;
        damage -= oldHealth;

        if (health < 0)
            Die();

        return damage;
    }

    public void Die()
    {
        AudioManager.GeneralPlay("Explosion");
        GameObject.FindGameObjectsWithTag("MainCamera")[0].GetComponent<CameraShake>().Shake(0.1f);
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
