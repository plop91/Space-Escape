using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathEffectScript : MonoBehaviour
{
    //resets level after resetTime seconds
    public int resetTime;
    void Start()
    {
        StartCoroutine(restartLevel());
    }
    IEnumerator restartLevel()
    {
        yield return new WaitForSeconds(resetTime);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

