using UnityEngine;

/**Changes made - Alex
 * Allowed the player to fire a laser
 * Made the player slower in reverse to make asteroids more threatening
 * */

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    [SerializeField]
    private GameObject manager;
    private GameObject shield;
    private GameObject hitFlash;
    public GameObject shot; //Shots the player can fire - Alex
    private Transform shotTransform;
    public GameObject deathEffect;
    public GameObject missile;
    public int shipSpeed = 45;
    public int scrollSpeed = 5;
    [HideInInspector]
    public float speed;
    private int verticalMoveSpeed;
    public bool canMoveUp = true;
    public bool canMoveDown = true;
    public bool canMoveLeft = true;
    public bool canMoveRight = true;
    public bool canMove = true;
    public bool canMissile = true;
    public bool canBoost = false;
    public bool canShoot = false;
    [HideInInspector]
    public Vector2 movement;
    private Rigidbody2D rb;
    private Animator animator;

    private float lastShot; //how long since last shot - Alex
    private float shotFrequency = 1f;

    private float lastMissile;
    private float missileFrequency = 3f;

    [HideInInspector]
    public float health;
    public const int MAX_HEALTH = 10;

    private Color shieldHitCol = new Color(255, 255, 255);
    private Color playerHitCol = new Color(255, 0, 0);

    private bool firingMissiles = false;
    private float lastFiredMissile = 0f;
    private float missileSalvoRate = 0.4f;
    private int salvoSize = 4;
    private int missilesFired = 0;

    void Start()
    {
        //am = FindObjectOfType<AudioManager>();
        //am.Play("MainLoop");
        hitFlash = GameObject.Find("HitFlash");
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        lastShot = shotFrequency; //Alex
        health = MAX_HEALTH;
        lastMissile = missileFrequency;
        manager = GameObject.Find("GameManager");
    }

    void Update()
    {
        if (canMove)
        {
            if (shield != null)
            {
                gameObject.GetComponent<PolygonCollider2D>().enabled = false;
                gameObject.GetComponent<CircleCollider2D>().enabled = true;
            }
            else
            {
                gameObject.GetComponent<PolygonCollider2D>().enabled = true;
                gameObject.GetComponent<CircleCollider2D>().enabled = false;
            }

            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
            if ((Input.GetAxisRaw("Horizontal") > 0.01 || Input.GetAxisRaw("Horizontal") < 0.01) && (Input.GetAxisRaw("Vertical") > 0.01 || Input.GetAxisRaw("Vertical") < 0.01))
            {
                movement.x = movement.x / 4;

                movement.y = movement.y / 2;
                if (!canMoveUp && movement.y > 0)
                {
                    movement.y = 0;
                }
                if (!canMoveDown && movement.y < 0)
                {
                    movement.y = 0;
                }
                if (!canMoveRight && movement.x > 0)
                {
                    movement.x = 0;
                }
                if (!canMoveLeft && movement.x < 0)
                {
                    movement.x = 0;
                }
            }
            
                animator.SetFloat("VerticalSpeed", movement.y);
            
        }

        if (movement.x < 0)
            movement.x /= .5f; //.5 value is intentional to allow ship to better maneuver - Alex

        if (Input.GetKey(KeyCode.LeftShift) && canBoost)
        {
            speed = shipSpeed * 2;
        }
        else
        {
            speed = shipSpeed;
        }

        if (canShoot)
        {
            //Shooting code - Alex
            lastShot += Time.deltaTime;

            if (lastShot > shotFrequency && Input.GetKey(KeyCode.Space))
            {
                AudioManager.GeneralPlay("LaserFire");
                lastShot = 0f;
                Vector3 position = transform.position;
                GameObject newShot = Instantiate(shot, gameObject.transform) as GameObject;
                newShot.GetComponent<LaserFluctuation>().transform.localPosition = new Vector3(1.35f, 0f, -1f);
                newShot.GetComponent<LaserFluctuation>().transform.SetParent(gameObject.transform, false);
            }//End shooting code
        }

        if (firingMissiles)
        {
            lastFiredMissile -= Time.deltaTime;
            if (lastFiredMissile <= 0)
            {
                lastFiredMissile = missileSalvoRate;

                Vector3 position = transform.position;

                AudioManager.GeneralPlay("MissileFire");
                GameObject newShot = Instantiate(missile, gameObject.transform) as GameObject;
                newShot.gameObject.transform.parent = null;
                newShot.transform.Translate(missilesFired + 2, 1, 0);

                missilesFired++;

                if (missilesFired >= salvoSize)
                {
                    firingMissiles = false;
                    lastFiredMissile = 0;
                    missilesFired = 0;
                }
            }
        }

        if (canMissile)
        {
            lastMissile += Time.deltaTime;

            if (lastMissile > missileFrequency && Input.GetKey(KeyCode.F))
            {
                lastMissile = 0f;
                Vector3 position = transform.position;


                firingMissiles = true;
            }
        }
    }
    private void FixedUpdate()
    {
        if (canMove)
        {
            if (rb != null)
            {
                rb.MovePosition(rb.position + (movement * speed + new Vector2(scrollSpeed, 0)) * Time.fixedDeltaTime);
            }
        }
    }
    public void Die()
    {
        if (shield != null)
            shield.GetComponent<Shield>().Die();
        else
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
            GameObject.FindGameObjectsWithTag("MainCamera")[0].GetComponent<CameraShake>().StopShake();
            GameObject.FindGameObjectWithTag("MainCamera").transform.parent = null;
            Destroy(gameObject);
        }
    }

    public void SetShield(GameObject shield)
    {
        this.shield = shield;
    }

    public void TakeDamage(float damage)
    {
        if (shield != null)
        {
            hitFlash.GetComponent<HitFlash>().Flash(shieldHitCol);
            shield.GetComponent<Shield>().TakeDamage(damage);
        }
        else
        {
            hitFlash.GetComponent<HitFlash>().Flash(playerHitCol);
            AudioManager.GeneralPlay("Explosion");
            health -= damage;
            
            if (health < 0)
            {
                Die();
            }
            else
            {
                GameObject.FindGameObjectsWithTag("MainCamera")[0].GetComponent<CameraShake>().Shake(0.25f);
            }
        }
    }

    public Vector2 GetSpeed()
    {
        return movement * speed;
    }
}
