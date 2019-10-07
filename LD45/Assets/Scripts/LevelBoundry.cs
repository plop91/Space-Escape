using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBoundry : MonoBehaviour
{
    public float distanceFrom0;
    private GameObject player;
    public GameObject top;
    public GameObject top2;
    public GameObject bottom;
    public GameObject bottom2;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        top.transform.position = new Vector3(player.transform.position.x+4,distanceFrom0,0);
        top2.transform.position = new Vector3(player.transform.position.x+24, distanceFrom0, 0);
        bottom.transform.position = new Vector3(player.transform.position.x+4, -distanceFrom0, 0);
        bottom2.transform.position = new Vector3(player.transform.position.x+24, -distanceFrom0, 0);
    
}
    private void FixedUpdate()
    {
        if (player != null) { 
        top.transform.position = new Vector3(player.transform.position.x + 4, distanceFrom0, 0);
        top2.transform.position = new Vector3(player.transform.position.x + 24, distanceFrom0, 0);
        bottom.transform.position = new Vector3(player.transform.position.x + 4, -distanceFrom0, 0);
        bottom2.transform.position = new Vector3(player.transform.position.x + 24, -distanceFrom0, 0);
    }
    }
    void OnDrawGizmosSelected()
    {
        // Draw a semitransparent blue cube at the transforms position
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(new Vector3(0,distanceFrom0,0), new Vector3(10000, 1, 1));
        Gizmos.DrawCube(new Vector3(0, -distanceFrom0, 0), new Vector3(10000, 1, 1));
    }
}
