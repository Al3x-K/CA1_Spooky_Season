using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this script allows player to open chest by using colliders
public class ChestOpener : MonoBehaviour
{
    public GameObject ChestClosed, ChestOpen; //there are 2 sprites for each chest
    public int numOfCoins;
    void Start()
    {
        ChestClosed.SetActive(true); //the sprite with closed chest appears
        ChestOpen.SetActive(false); //the sprite with open chest disappears
        Debug.Log(numOfCoins);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        ChestClosed.SetActive(false); //the sprite with closed chest disappears when player "touches" the chest
        ChestOpen.SetActive(true); //the sprite with open chest appears
        numOfCoins++; 
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        ChestClosed.SetActive(true); //when player no longer "touches" the chest the sprite with closed chest reappears
        ChestOpen.SetActive(false); //the sprite with open chest disappears
    }
}
