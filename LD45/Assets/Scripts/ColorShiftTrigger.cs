using UnityEngine;

public class ColorShiftTrigger : MonoBehaviour
{
    private ColorShiftReciever[] recievers;

    private void Start()
    {
        recievers = FindObjectsOfType<ColorShiftReciever>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player != null)
        {
            foreach (ColorShiftReciever r in recievers)
            {
                r.Shift();
            }
        }
    }
}
