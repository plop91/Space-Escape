using UnityEngine;

public class ColorShiftReciever : MonoBehaviour
{
    public bool deadly;
    public bool StartDeadly;
    private SpriteRenderer sr;

   void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        Debug.Log(deadly);
        if (StartDeadly)
            deadly = true;
        if(deadly)
        {
            sr.color = new Color(255, 255, 0,255);
        }
        else
        {
            sr.color = new Color(125, 0, 255,255);
        }
    }
    void Update()
    {
        //Debug.Log(deadly);
        if (deadly)
        {
            sr.color = new Color(255, 255, 0, 255);
        }else
        {
            sr.color = new Color(125, 0, 255, 255);
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player != null)
        {
            if (deadly)
            {
                player.Die();
            }
        }
    }
    public void Shift()
    {
        deadly = !deadly;
    }
}
