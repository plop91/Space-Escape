using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMissile : MonoBehaviour
{
    public GameObject deathEffect;
    private GameObject missileTarget;
    private float angle;
    private Vector2 movement = new Vector2();
    private float speed = 40f;

    private bool active = true;

    private float age = 0;
    private float lifetime = 2.5f;

    private float lingerTime = 0;
    private float lingerTimer = 0;//3f;

    private float swivelNum = 0;

    private float range = 35f;


    // Start is called before the first frame update
    void Start()
    {
        FindTarget();
        
    }

    private void FindTarget()
    {
        GameObject[] possibleTargets = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject[] targetsInRange = new GameObject[possibleTargets.Length];
        int numTargets = 0;
        float lastDist = 10000;

        foreach (GameObject t in possibleTargets)
        {
            float dist = Vector2.Distance(transform.position, t.transform.position);
            if (dist < range)
            {
                if (dist < lastDist)
                {
                    missileTarget = t;
                }
            }

        }

        /*
        if (numTargets > 0)
        {
            this.missileTarget = targetsInRange[Random.Range(0, numTargets)];
            Debug.Log(missileTarget.name);
        }
        else
            this.missileTarget = null;*/
    }

    // Update is called once per frame
    void Update()
    {
        if (missileTarget != null && active)
        {
            swivelNum += Time.deltaTime * 400;

            Vector2 target = missileTarget.transform.position;

            float y = target.y - transform.position.y;
            float x = target.x - transform.position.x;

            angle = Mathf.Atan2(y, x) * Mathf.Rad2Deg;
            //angle -= 90;

            movement.x = Mathf.Cos(angle * Mathf.Deg2Rad) * speed;
            movement.y = Mathf.Sin(angle * Mathf.Deg2Rad) * speed;

            transform.Translate(movement * Time.deltaTime);
        }

        else if (missileTarget == null)
            FindTarget();

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
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.name.StartsWith("Enemy") && active && !collision.gameObject.name.StartsWith("EnemyShot"))
        {
            collision.collider.gameObject.GetComponent<MovingEnemy>().Die();
            Die();
        }
        else if (collision.gameObject.name.StartsWith("FortTurret") && active)
        {
            collision.collider.gameObject.GetComponent<FortTurret>().Die();
            Die();
        }
    }
}
