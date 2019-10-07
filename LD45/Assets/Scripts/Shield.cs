using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [HideInInspector]
    public float maxHealth = 12f;
    [HideInInspector]
    public float health = 12f;
    private float regen = 0.75f;
    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.Find("Player");
        transform.localPosition = player.transform.localPosition;
        transform.parent = player.transform;

        player.GetComponent<Player>().SetShield(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        health += regen * Time.deltaTime;
        if (health > maxHealth)
            health = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health < 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
