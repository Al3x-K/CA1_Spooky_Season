using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestOpener : MonoBehaviour
{
    public GameObject ChestClosed, ChestOpen;
    public int numOfCoins;
    void Start()
    {
        ChestClosed.SetActive(true);
        ChestOpen.SetActive(false);
        Debug.Log(numOfCoins);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        ChestClosed.SetActive(false);
        ChestOpen.SetActive(true);
        numOfCoins++; 
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        ChestClosed.SetActive(true);
        ChestOpen.SetActive(false);
    }
}
