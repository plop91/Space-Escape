using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingField : MonoBehaviour
{
    public float movement = 15;

    private float currentMovement;
    public float movementSpeed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, movementSpeed*Time.deltaTime, 0);
        currentMovement += movementSpeed*Time.deltaTime;

        if (Mathf.Abs(currentMovement) > movement)
        {
            currentMovement = 0;
            movementSpeed = -movementSpeed;
        }
    }
}
