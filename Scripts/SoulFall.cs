using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulFall : MonoBehaviour
{
    public static float fallSpeed;
    public GameObject collectSoul;
    public AudioSource getSoul;
    public static int collectionNum;

    private void Start()
    {
        transform.position = new Vector3(Random.Range(-2, 4), 6, 0);
        getSoul = GameObject.FindWithTag("Player").GetComponent<AudioSource>();
    }

    private void Update()
    {
        transform.position += Vector3.down * Time.deltaTime * fallSpeed;
        if (transform.position.y < -6)
        {
            FlowerPlacer.souls -= collectionNum;
            Destroy(gameObject);
        }
    }

    public void Collect()
    {
        FlowerPlacer.souls += collectionNum;
        Instantiate(collectSoul, transform.position, transform.rotation);
        getSoul.Play();
        Destroy(gameObject);
    }

    private void OnMouseDown()
    {
        FlowerPlacer.souls += collectionNum;
        Instantiate(collectSoul, transform.position, transform.rotation);
        getSoul.Play();
        Destroy(gameObject);
    }
}
