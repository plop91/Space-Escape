using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** Asteroid to smash player. This should probably inherit from a generic Enemy class, but it's a bit pointless until we have more enemies - Alex */

public class Asteroid : MonoBehaviour
{
    public GameObject deathEffect;

    private float size; //Size of asteroid
    private Vector2 speed; //Movement speed
    private Vector3 rotationSpeed; //Speed of rotation
    private float health = 150f;

    // Start is called before the first frame update
    void Start()
    {
        size = Random.Range(0.2f, 0.45f); //Random size
        health *= size;
        transform.localScale = transform.localScale * size;
        speed.x = Random.Range(-6, 0); //Random speed
        speed.y = Random.Range(-6, 6);
        rotationSpeed = new Vector3(0, 0, Random.Range(-500, 500));

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += (Vector3)(speed * Time.deltaTime); //Update position
        transform.Rotate(rotationSpeed * Time.deltaTime); //Update speed
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            FindObjectOfType<Player>().TakeDamage(10*size);
            Die(true);
        }
    }

    /**TODO: Make large asteroids split into smaller ones when destroyed. Also add particle effect.*/
    public void Die()
    {
        GameObject.FindGameObjectsWithTag("MainCamera")[0].GetComponent<CameraShake>().Shake(size/2);
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void Die(bool noshake)
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
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
}
