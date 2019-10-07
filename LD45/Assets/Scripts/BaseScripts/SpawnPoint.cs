using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public int cooldown = 500;
    private int usedTimer = 0;
    public bool playerCanSpawn;
    public bool enemyCanSpawn;

    public void FixedUpdate()
    {
        usedTimer--;
    }
    public int GetUsedTimer()
    {
        return usedTimer;
    }
    public void SetUsedTimer()
    {
        usedTimer = cooldown;
    }
}
