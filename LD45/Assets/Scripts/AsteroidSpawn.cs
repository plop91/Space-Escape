using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** Spawn asteroids to smash player. It could be extended to spawn other hazards, but it might make more sense to make a separate class for that. - Alex */

public class AsteroidSpawn : MonoBehaviour
{
    public bool astroidsEnabled;
    public GameObject asteroidPrefab;
    private GameObject player;

    private float timeSinceSpawn; //Every time timeSinceSpawn reaches spawnFrequency there is a chance of an asteroid spawn
    public float spawnFrequency = 0.75f;
    private float spawnChance = 0.45f;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        timeSinceSpawn = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (astroidsEnabled && !PauseMenu.GameIsPaused)
        {
            if (player != null)
            {
                timeSinceSpawn += Time.deltaTime;

                if (timeSinceSpawn >= spawnFrequency)
                {
                    if (Random.Range(0f, 1f) < spawnChance)
                    {
                        timeSinceSpawn = 0f;
                        Vector3 position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
                        position.x += 35;
                        Instantiate(asteroidPrefab, position, Quaternion.identity);
                    }
                    else
                    {
                        timeSinceSpawn = spawnFrequency / 2f;
                    }
                }
            }
        }
    }
}
