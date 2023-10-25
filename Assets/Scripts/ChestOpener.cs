using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestOpener : MonoBehaviour
{
    public GameObject ChestClosed, ChestOpen;
    void Start()
    {
        ChestClosed.SetActive(true);
        ChestOpen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        ChestClosed.SetActive(false);
        ChestOpen.SetActive(true);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        ChestClosed.SetActive(true);
        ChestOpen.SetActive(false);
    }
}
