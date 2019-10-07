using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FortMain : MonoBehaviour
{
    public GameObject deathEffect;
    public GameObject[] turrets;

    // Start is called before the first frame update
    void Start()
    {
   
    }

    // Update is called once per frame
    void Update()
    {
        bool destroyed = true;

        foreach (GameObject o in turrets)
        {
            if (o != null)
                destroyed = false;
        }

        if (destroyed)
            Die();
    }

    public void Die()
    {
        AudioManager.GeneralPlay("Explosion");
        GameObject.FindGameObjectsWithTag("MainCamera")[0].GetComponent<CameraShake>().Shake(0.35f);
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
