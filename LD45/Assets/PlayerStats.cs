using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    private Player player;
    private Shield shield;
    public GameObject playerStatsContainer;
    public Slider healthSlider;
    public TMP_Text healthReadout;
    public Slider shieldSlider;
    public TMP_Text shieldReadout;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        shield = FindObjectOfType<Shield>();
        healthSlider.value = player.health / Player.MAX_HEALTH;
        healthReadout.text = player.health.ToString("00") + "/" + Player.MAX_HEALTH;
        if(shield != null)
        {
            shieldSlider.value = shield.health / shield.maxHealth;
            shieldReadout.text = shield.health.ToString("00") + "/" + shield.maxHealth;
        }
        else
        {
            shieldSlider.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseMenu.GameIsPaused)
        {
            playerStatsContainer.SetActive(false);
        }
        else
        {
            playerStatsContainer.SetActive(true);
            if (player != null)
            {
                healthSlider.value = player.health / Player.MAX_HEALTH;
                healthReadout.text = player.health.ToString("00") + "/" + Player.MAX_HEALTH;
            }
            else
            {
                healthSlider.value = 0;
            }

            if (shield != null)
            {
                shieldSlider.value = shield.health / shield.maxHealth;
                shieldReadout.text = shield.health.ToString("00") + "/" + shield.maxHealth;
            }
            else
            {
                shieldSlider.value = 0;
            }
        }
    }
}
