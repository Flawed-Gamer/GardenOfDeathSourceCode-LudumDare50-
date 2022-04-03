using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerGrowth : MonoBehaviour
{
    public GameObject dust;
    public GameObject[] neededFood;
    public Sprite[] stage;
    public int plantStage;
    public float lifeForce;
    public FlowerPlacer placer;
    public bool waterReady;
    public AudioSource grow;
    public AudioSource feed;
    public int plantID;
    public GameObject spotLight;
    public Coroutine waterCoroutine = null;
    public Coroutine airCoroutine = null;
    public Coroutine sunCoroutine = null;

    private void Start()
    {
        lifeForce = 100f;
        placer = GameObject.FindWithTag("GameController").GetComponent<FlowerPlacer>();
        plantStage = 0;
        gameObject.GetComponent<SpriteRenderer>().sprite = stage[0];
        grow = GameObject.FindWithTag("BigPlant").GetComponent<AudioSource>();
        waterReady = false;
        foreach (GameObject obj in neededFood)
        {
            obj.SetActive(false);
        }
        airCoroutine = StartCoroutine(NeedsAir());
        sunCoroutine = StartCoroutine(NeedsSun());
        waterCoroutine = StartCoroutine(NeedsWater());
    }

    private void Update()
    {
        ClickOn();
        if (lifeForce <= 0)
        {
            Instantiate(dust, transform.position, transform.rotation);
            if (plantID == 5 && plantStage >= 3)
            {
                placer.upgradeLevel--;
            }
            if (plantID == 4 && plantStage >= 3)
            {
                SoulFall.collectionNum--;
            }
            Destroy(gameObject);
        }
    }

    IEnumerator NeedsAir()
    {
        yield return new WaitForSeconds(Random.Range(12 + (plantStage * 4), 16 + (plantStage * 4)));
        neededFood[1].SetActive(true);
        for (int i = 0; i < 100; i++)
        {
            yield return new WaitForSeconds(0.08f);
            lifeForce--;
        }
    }

    IEnumerator NeedsSun()
    {
        yield return new WaitForSeconds(Random.Range(17 + (plantStage * 4), 20 + (plantStage * 4)));
        neededFood[2].SetActive(true);
        for (int i = 0; i < 100; i++)
        {
            yield return new WaitForSeconds(0.08f);
            lifeForce--;
        }
    }

    IEnumerator NeedsWater()
    {
        yield return new WaitForSeconds(Random.Range(5 + (plantStage * 3), 9 + (plantStage * 3)));
        neededFood[0].SetActive(true);
        waterReady = true;
        for (int i = 0; i < 100; i++)
        {
            yield return new WaitForSeconds(0.08f);
            lifeForce--;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (plantID == 2 && plantStage >= 3)
        {
            if (collision.CompareTag("Soul"))
            {
                collision.GetComponent<SoulFall>().Collect();
            }
        }
    }

    public void ClickOn()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
            Collider2D collider = GetComponent<Collider2D>();

            if (hit.collider == collider)
            {
                if (placer.food == 3)
                {
                    StopCoroutine(airCoroutine);
                    lifeForce = 100;
                    neededFood[1].SetActive(false);
                    airCoroutine = StartCoroutine(NeedsAir());
                    placer.food = 0;
                    placer.airPrice++;
                    FlowerPlacer.souls -= placer.airPrice;
                    feed.Play();
                }
                if (placer.food == 2)
                {
                    StopCoroutine(sunCoroutine);
                    lifeForce = 100;
                    neededFood[2].SetActive(false);
                    sunCoroutine = StartCoroutine(NeedsSun());
                    placer.food = 0;
                    placer.sunPrice++;
                    FlowerPlacer.souls -= placer.sunPrice;
                    feed.Play();
                }
                if (placer.food == 1 && waterReady)
                {
                    StopCoroutine(waterCoroutine);
                    lifeForce = 100;
                    neededFood[0].SetActive(false);
                    plantStage++;
                    waterReady = false;
                    waterCoroutine = StartCoroutine(NeedsWater());
                    placer.food = 0;
                    placer.waterPrice++;
                    FlowerPlacer.souls -= placer.waterPrice;
                    if (plantStage < 4)
                    {
                        gameObject.GetComponent<SpriteRenderer>().sprite = stage[plantStage];
                        grow.Play();
                    }
                    if (plantStage == 3)
                    {
                        placer.plantsGrown++;
                        if (plantID == 1)
                        {
                            placer.waterPrice -= 6;
                            placer.airPrice -= 5;
                            placer.sunPrice -= 5;
                        }
                        else
                        {
                            placer.waterPrice -= 2;
                            placer.airPrice -= 2;
                            placer.sunPrice -= 2;
                        }
                        if (plantID == 5)
                        {
                            placer.upgradeLevel++;
                        }
                        if (plantID == 4)
                        {
                            SoulFall.collectionNum++;
                        }
                        if (plantID == 3)
                        {
                            spotLight.SetActive(true);
                        }
                    }
                }
            }
        }
    }
}
