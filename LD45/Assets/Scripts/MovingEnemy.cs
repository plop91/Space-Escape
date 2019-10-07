using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingEnemy : MonoBehaviour
{
    public GameObject shot;
    public GameObject deathEffect;
    private GameObject player;
    private Vector2 desiredPosition;
    private bool findingRange = true;
    private float offset = 25f;

    private int yDirection = 1;
    
    private float maxY = 7.5f;

    private float ySpeed = 10f;

    private float speed = 5f;

    private float angle;

    private float maxHealth = 75f;
    private float health;

    private float lastShot = 0.1f;
    private float shotFrequency = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        if (Random.Range(0, 1) > 0.5f)
            yDirection = -yDirection;
        health = maxHealth;
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        float xMove = 0;
        if (!PauseMenu.GameIsPaused && player != null)
        {
            xMove = 0;
            transform.Translate(xMove * Time.deltaTime, yDirection * ySpeed * Time.deltaTime, 0, Space.World);

            RotateToPlayer();

            lastShot -= Time.deltaTime;

            if (Mathf.Abs(transform.position.y - player.transform.position.y) > maxY)
            {
                if (transform.position.y < player.transform.position.y)
                    yDirection = 1;
                else
                    yDirection = -1;
            }

            if (lastShot < 0 && !PauseMenu.GameIsPaused && Vector3.Distance(transform.position, player.transform.position) < 30f)
            {
                lastShot = shotFrequency;

                Shoot();
            }

        }
    }

    private void RotateToPlayer()
    {
        angle = Mathf.Atan2(player.transform.position.y - transform.position.y, player.transform.position.x - transform.position.x) * Mathf.Rad2Deg;
        angle -= 90;

        transform.localEulerAngles = new Vector3(0, 0, angle);
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
        GameObject.FindGameObjectsWithTag("MainCamera")[0].GetComponent<CameraShake>().Shake(0.15f);
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void Shoot()
    {
        Vector3 pos = gameObject.transform.position;
        Instantiate(shot, pos, gameObject.transform.rotation);
    }

}
