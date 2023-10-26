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
    public Image[] healthBar; //creates an array with heart images
   
    void Update()
    {
        health = playerController.health; //gets player's health
        maxHealth = playerController.maxHealth;//gets player's maxhealth
        for (int i = 0; i < healthBar.Length; i++) //goes through an array of heart images
        {
            if(i < health) //checks if i is less than current health
            {
                healthBar[i].sprite = heart; //shows the full heart
            }
            else //if the i is bigger than health
            {
                healthBar[i].sprite = emptyHeart; //shows empty heart
            }
            if(i < maxHealth) //checks if the max health wasn't exceeded
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
