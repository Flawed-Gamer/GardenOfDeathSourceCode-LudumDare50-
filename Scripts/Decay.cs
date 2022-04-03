using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decay : MonoBehaviour
{
    public float decayTime;

    private void Start()
    {
        StartCoroutine(DecayWait());
    }

    IEnumerator DecayWait()
    {
        yield return new WaitForSeconds(decayTime);
        Destroy(gameObject);
    }
}
