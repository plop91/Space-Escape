using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathLaser : MonoBehaviour
{
    public bool canMove = true;
    public float moveSpeed;
    GameObject playerGO;
    // Start is called before the first frame update
    void Start()
    {
        playerGO = GameObject.FindGameObjectWithTag("Player");
        GetComponent<Animator>().speed = 0.4f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (canMove) { 
        if (playerGO != null)
        {
            if (!PauseMenu.GameIsPaused)
            {
                transform.position = new Vector3(transform.position.x + (moveSpeed * Time.fixedDeltaTime), playerGO.transform.position.y, transform.position.z);
            }
        }
    }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if(player != null)
        {
            player.Die();
        }
    }
}
