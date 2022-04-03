using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigFlower : MonoBehaviour
{
    public GameObject dust;
    public GameObject[] neededFood;
    public Sprite[] stage;
    public int plantStage;
    public float lifeForce;
    public FlowerPlacer placer;
    public AudioSource feed;
    public Coroutine waterCoroutine = null;
    public Coroutine airCoroutine = null;
    public Coroutine sunCoroutine = null;
    public bool won;

    private void Start()
    {
        won = false;
        lifeForce = 100;
        placer = GameObject.FindWithTag("GameController").GetComponent<FlowerPlacer>();
        plantStage = 0;
        gameObject.GetComponent<SpriteRenderer>().sprite = stage[0];
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
            placer.GameOverLose();
            Destroy(gameObject);
            //Start Game Over Bad Ending
        }
        if (placer.plantsGrown > 23)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = stage[3];
            if (!won)
            {
                placer.GameOverWin();
                won = true;
            }
        }
        else if (placer.plantsGrown > 11)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = stage[2];
        }
        else if (placer.plantsGrown > 5)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = stage[1];
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = stage[0];
        }
    }

    IEnumerator NeedsAir()
    {
        yield return new WaitForSeconds(Random.Range(35, 41));
        neededFood[1].SetActive(true);
        for (int i = 0; i < 100; i++)
        {
            yield return new WaitForSeconds(0.4f);
            lifeForce--;
        }
    }

    IEnumerator NeedsSun()
    {
        yield return new WaitForSeconds(Random.Range(41, 46));
        neededFood[2].SetActive(true);
        for (int i = 0; i < 100; i++)
        {
            yield return new WaitForSeconds(0.4f);
            lifeForce--;
        }
    }

    IEnumerator NeedsWater()
    {
        yield return new WaitForSeconds(Random.Range(45, 51));
        neededFood[0].SetActive(true);
        for (int i = 0; i < 100; i++)
        {
            yield return new WaitForSeconds(0.4f);
            lifeForce--;
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
                    feed.Play();
                }
                if (placer.food == 1)
                {
                    StopCoroutine(waterCoroutine);
                    lifeForce = 100;
                    neededFood[0].SetActive(false);
                    plantStage++;
                    waterCoroutine = StartCoroutine(NeedsWater());
                    placer.food = 0;
                    placer.waterPrice++;
                    feed.Play();
                }
            }
        }
    }
}
