using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** Makes a laser (not necessarily only the player's) throb - Alex */

public class LaserFluctuation : MonoBehaviour
{
    private GameObject player;
    private LineRenderer lineRenderer;
    public const float SIZE = 0.5f;
    private float lifespan = 0.5f;
    private float fluctuations = 15f;
    private float age = 0f;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        age += Time.deltaTime;
        if (age > lifespan)
        {
            Destroy(gameObject);
        }
        else
        {
            lineRenderer.startWidth = Mathf.Abs(Mathf.Sin(age * fluctuations / (lifespan))) * SIZE;
            lineRenderer.endWidth = Mathf.Abs(Mathf.Sin(age * fluctuations / (lifespan))) * SIZE;
        }
    }

}
