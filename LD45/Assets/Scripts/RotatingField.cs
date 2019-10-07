using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingField : MonoBehaviour
{
    public float rotationSpeed = 10f;
    public int direction = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, rotationSpeed * direction * Time.deltaTime));
    }
}
