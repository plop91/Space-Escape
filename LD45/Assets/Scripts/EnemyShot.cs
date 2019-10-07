using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShot : MonoBehaviour
{
    public GameObject deathEffect;
    private GameObject player;
    private float angle;
    private Vector2 movement = new Vector2();
    private float speed = 17f;

    private bool active = true;

    private float age = 0;
    private float lifetime = 4.5f;

    private float lingerTime = 0;
    private float lingerTimer = 0;//3f;

    private float swivelNum = 0;

    public float damage = 3;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null && active)
        {
            swivelNum += Time.deltaTime*40;

            Vector2 target = player.transform.position;

            float y = target.y - transform.position.y;
            float x = target.x - transform.position.x;

            angle = Mathf.Atan2(y, x) * Mathf.Rad2Deg;
            angle -= 90;

            if (Vector2.Distance(transform.position, player.transform.position) > 80)
                angle += Mathf.Sin(swivelNum * Mathf.Deg2Rad) * 10f;

            movement.x = Mathf.Cos(angle * Mathf.Deg2Rad) * speed;
            movement.y = Mathf.Sin(angle * Mathf.Deg2Rad) * speed;

            transform.Translate(movement * Time.deltaTime);
        }

        if (!active)
        {
            Destroy(gameObject.GetComponent<Rigidbody2D>());
        }

        age += Time.deltaTime;
        if (age >= lifetime)
        {
            Die();
        }
    }

    public void SetAngle(float angle)
    {
        this.angle = angle;
        movement = new Vector2();
        movement.x = -Mathf.Cos(angle * Mathf.Deg2Rad) * speed;
        movement.x = Mathf.Sin(angle * Mathf.Deg2Rad) * speed;;
    }

    public void Die()
    {
        if (active)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
            active = false;
        }
        else
        {
            lingerTime += Time.deltaTime;

            if (lingerTime > lingerTimer)
            {
                Destroy(gameObject);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player" && active)
        {
            FindObjectOfType<Player>().TakeDamage(damage);
            Die();
        }
    }
}
