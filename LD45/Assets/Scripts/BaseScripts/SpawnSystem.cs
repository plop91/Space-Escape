using System.Collections.Generic;
using UnityEngine;

public class SpawnSystem : MonoBehaviour
{
    public bool SpawningEnabled;
    [Range(0f, 1f)]
    public int players = 1;
    public GameObject player;
    [Range(0f, 5f)]
    public int enemys;
    public GameObject enemy;
    public bool testCopTracking;
    private SpawnPoint[] spawnpoints;
    void Start()
    {
        spawnpoints = GetComponentsInChildren<SpawnPoint>();
        for (int i = 0; i < players; i++)
        {
            SpawnPlayerRandomly();
        }
        for (int i = 0; i < enemys; i++)
        {
            SpawnCopRandomly();
        }
    }
    private void Update()
    {

    }
    private void SpawnPlayerRandomly()
    {
        if (SpawningEnabled)
        {
            SpawnPoint spawnpoint = GetCharcterSpawnpoint();
            if (spawnpoint != null)
            {
                Instantiate(player, spawnpoint.transform.position, Quaternion.identity);
            }
        }
    }
    private void SpawnPlayerAt(Transform t)
    {
        Instantiate(player, t.position, Quaternion.identity);
    }
    private SpawnPoint GetCharcterSpawnpoint()
    {
        List<SpawnPoint> spawnPointchoices = new List<SpawnPoint>();
        foreach (SpawnPoint s in spawnpoints)
        {
            if (s.playerCanSpawn && s.GetUsedTimer() <= 0)
            {
                spawnPointchoices.Add(s);
            }
        }
        if (spawnPointchoices.Count == 0)
        {
            return null;
        }
        int random = Random.Range(0, spawnPointchoices.Count);
        spawnPointchoices[random].SetUsedTimer();

        return spawnPointchoices[random];
    }
    private void SpawnCopRandomly()
    {
        if (SpawningEnabled)
        {
            SpawnPoint spawnpoint = GetCopSpawnpoint();
            if (spawnpoint != null)
            {
                GameObject temp = Instantiate(enemy, spawnpoint.transform.position, Quaternion.identity);
                if (testCopTracking)
                {
                    //PoliceAI policeAI = temp.GetComponent<PoliceAI>();
                    //policeAI.target = FindObjectOfType<Player>().transform;
                }
            }
        }
    }
    private SpawnPoint GetCopSpawnpoint()
    {
        List<SpawnPoint> spawnPointchoices = new List<SpawnPoint>();
        foreach (SpawnPoint s in spawnpoints)
        {
            // if (s.copCanSpawn && s.GetUsedTimer() <= 0)
            //{
            //    spawnPointchoices.Add(s);
            // }
        }
        if (spawnPointchoices.Count == 0)
        {
            return null;
        }
        int random = Random.Range(0, spawnPointchoices.Count);
        spawnPointchoices[random].SetUsedTimer();

        return spawnPointchoices[random];
    }
}
