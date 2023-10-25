using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    public PlayerController playerController;
    public int health;
    public int maxHealth;
    public Sprite heart;
    public Sprite emptyHeart;
    public Image[] healthBar;
   
    void Update()
    {
        health = playerController.health;
        maxHealth = playerController.maxHealth;
        for (int i = 0; i < healthBar.Length; i++)
        {
            if(i < health)
            {
                healthBar[i].sprite = heart;
            }
            else
            {
                healthBar[i].sprite = emptyHeart;
            }
            if(i < maxHealth)
            {
                healthBar[i].enabled = true;
            }
            else
            {
                healthBar[i].enabled = false;
            }

        }
    }
}
