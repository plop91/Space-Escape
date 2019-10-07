using UnityEngine;

/** Makes the starfield respond to the player's movement - Alex */

public class Starscape : MonoBehaviour
{
    private Vector2 movement;
    private ParticleSystem system;
    private Player player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        system = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
            if (player.movement.x > 0)
            {
                var main = system.main;
                main.simulationSpeed = 3.0f;
            }
            else if (player.movement.x < 0)
            {
                var main = system.main;
                main.simulationSpeed = 0.5f;
            }
            else
            {
                var main = system.main;
                main.simulationSpeed = 1.0f;
            }
        
    }
}
