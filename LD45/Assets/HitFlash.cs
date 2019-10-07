using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitFlash : MonoBehaviour
{
    private SpriteRenderer renderer;

    private float currentFlash = 0f;
    private float flashDecay = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, currentFlash);
        currentFlash -= flashDecay * Time.deltaTime;
    }

    public void Flash(Color color)
    {
        currentFlash = 1f;
        renderer.color = new Color(color.r, color.g, color.b, currentFlash);
    }
}
