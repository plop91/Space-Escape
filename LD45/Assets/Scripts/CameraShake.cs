using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private float shakeMagnitude;
    private float shakeDecay = 0.2f;

    private float x;
    private float y;

    private Vector3 baseTransform;

    // Start is called before the first frame update
    void Start()
    {
        baseTransform = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (shakeMagnitude > 0)
        {
            transform.localPosition = baseTransform + (new Vector3(Mathf.Sin(x), Mathf.Sin(y), 0) * shakeMagnitude * Random.Range(0.9f, 1.1f));

            x += Time.deltaTime * 100 * shakeMagnitude * Random.Range(1f, 2f);
            y += Time.deltaTime * 100 * shakeMagnitude * Random.Range(1f, 2f);

            shakeMagnitude -= shakeDecay * Time.deltaTime;
            if (shakeMagnitude <= 0)
                transform.localPosition = baseTransform;
        }
    }

    public void Shake()
    {
        Shake(0.3f);
    }

    public void Shake(float magnitude)
    {
        this.shakeMagnitude = magnitude;
    }

    public void StopShake()
    {
        Shake(0f);
    }
}
