using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckSpace : MonoBehaviour
{
    public FlowerPlacer placer;
    private void Start()
    {
        placer.emptySpace = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Soul"))
        {
            placer.emptySpace = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Soul"))
        {
            placer.emptySpace = true;
        }
    }
}
