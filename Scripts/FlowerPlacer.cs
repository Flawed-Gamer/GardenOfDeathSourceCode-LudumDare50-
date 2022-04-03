using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class FlowerPlacer : MonoBehaviour
{
    public GameObject soul;
    public static int souls;
    public bool speeded;
    public Sprite sprite1;
    public Sprite sprite2;
    public GameObject speedButton;
    public GameObject[] seedPacks;
    public bool planting;
    public int plantType;
    public int foodType;
    public TextMeshProUGUI soulText;
    public Sprite[] flowerPackets;
    public GameObject flower;
    public GameObject[] flowerPlants;
    public Camera mainCam;
    public GameObject cancelButton;
    public bool emptySpace;
    public bool isPlanting;
    public int food;
    public Tilemap flowerTiles;
    public int plantsGrown;
    public GameObject settingsMenu;
    public Slider soundSlider;
    public Slider musicSlider;
    public AudioSource musicAudio;
    public AudioSource[] soundClips;
    public int waterPrice;
    public int airPrice;
    public int sunPrice;
    public TextMeshProUGUI waterText;
    public TextMeshProUGUI AirText;
    public TextMeshProUGUI SunText;
    public int upgradeLevel;
    public GameObject controlsScreen;
    public GameObject fadeScreen;

    private void Start()
    {
        fadeScreen.SetActive(false);
        fadeScreen.GetComponent<Image>().color = new Color(0, 0, 0, 0);
        controlsScreen.SetActive(false);
        soundSlider.value = StartMenu.sound;
        musicSlider.value = StartMenu.musicSound;
        SoulFall.collectionNum = 2;
        souls = 0;
        food = 0;
        SoulFall.fallSpeed = 4f;
        plantsGrown = 0;
        settingsMenu.SetActive(false);
        Time.timeScale = 1;
        waterPrice = 2;
        airPrice = 1;
        sunPrice = 2;
        upgradeLevel = 0;
        StartCoroutine(SpawnSouls());
        foreach (GameObject obj in seedPacks)
        {
            obj.GetComponent<Button>().interactable = true;
        }
    }

    private void Update()
    {
        if (souls < 0)
        {
            souls = 0;
        }
        soulText.text = souls.ToString();
        Planting();
        if (planting)
        {
            flower.SetActive(true);
            cancelButton.SetActive(true);
        }
        else
        {
            flower.SetActive(false);
            cancelButton.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OpenSettings();
            controlsScreen.SetActive(false);
        }
        musicAudio.volume = musicSlider.value / 100;
        foreach (AudioSource src in soundClips)
        {
            src.volume = soundSlider.value / 100;
        }
        if (waterPrice < 1)
        {
            waterPrice = 1;
        }
        if (airPrice < 0)
        {
            airPrice = 0;
        }
        if (sunPrice < 1)
        {
            sunPrice = 1;
        }
        waterText.text = waterPrice.ToString();
        AirText.text = airPrice.ToString();
        SunText.text = sunPrice.ToString();
        SetPrices();
        HotKeys();
    }

    public void GameOverLose()
    {
        fadeScreen.SetActive(true);
        StartCoroutine(ScreenFade(false));
    }

    public void GameOverWin()
    {
        fadeScreen.SetActive(true);
        StartCoroutine(ScreenFade(true));
    }

    IEnumerator ScreenFade(bool win)
    {
        float num = 0f;
        for (int i = 0; i < 100; i++)
        {
            yield return new WaitForSeconds(0.02f);
            num += 0.01f;
            fadeScreen.GetComponent<Image>().color = new Color(0, 0, 0, num);
        }
        if (win)
        {
            SceneManager.LoadScene("Victory");
        }
        else
        {
            SceneManager.LoadScene("Defeat");
        }
    }

    public void HotKeys()
    {
        if (Input.GetKeyDown(KeyCode.Q) && !planting && souls >= 1)
        {
            soundClips[1].Play();
            Plant(0);
            IsPlanting(true);
        }
        if (Input.GetKeyDown(KeyCode.W) && !planting && souls >= 2)
        {
            soundClips[1].Play();
            Plant(1);
            IsPlanting(true);
        }
        if (Input.GetKeyDown(KeyCode.E) && !planting && souls >= 2)
        {
            soundClips[1].Play();
            Plant(2);
            IsPlanting(true);
        }
        if (Input.GetKeyDown(KeyCode.R) && !planting && souls >= 3)
        {
            soundClips[1].Play();
            Plant(3);
            IsPlanting(true);
        }
        if (Input.GetKeyDown(KeyCode.T) && !planting && souls >= 3)
        {
            soundClips[1].Play();
            Plant(4);
            IsPlanting(true);
        }
        if (Input.GetKeyDown(KeyCode.A) && !planting && souls >= waterPrice)
        {
            Grow(5);
            IsPlanting(false);
        }
        if (Input.GetKeyDown(KeyCode.S) && !planting && souls >= airPrice)
        {
            Grow(7);
            IsPlanting(false);
        }
        if (Input.GetKeyDown(KeyCode.D) && !planting && souls >= sunPrice)
        {
            Grow(6);
            IsPlanting(false);
        }
        if (Input.GetKeyDown(KeyCode.F) && planting)
        {
            soundClips[0].Play();
            CancelPlanting();
        }
    }

    public void OpenSettings()
    {
        if (settingsMenu.activeSelf == true)
        {
            settingsMenu.SetActive(false);
            Time.timeScale = 1;
        }
        else
        {
            settingsMenu.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void OpenControls()
    {
        if (controlsScreen.activeSelf == true)
        {
            controlsScreen.SetActive(false);
        }
        else
        {
            controlsScreen.SetActive(true);
        }
    }

    public void SetPrices()
    {
        if (souls >= 1)
        {
            seedPacks[0].GetComponent<Button>().interactable = true;
        }
        else
        {
            seedPacks[0].GetComponent<Button>().interactable = false;
        }
        if (souls >= 2)
        {
            seedPacks[1].GetComponent<Button>().interactable = true;
            seedPacks[2].GetComponent<Button>().interactable = true;
        }
        else
        {
            seedPacks[1].GetComponent<Button>().interactable = false;
            seedPacks[2].GetComponent<Button>().interactable = false;
        }
        if (souls >= 3)
        {
            seedPacks[3].GetComponent<Button>().interactable = true;
            seedPacks[4].GetComponent<Button>().interactable = true;
        }
        else
        {
            seedPacks[3].GetComponent<Button>().interactable = false;
            seedPacks[4].GetComponent<Button>().interactable = false;
        }
        if (souls >= waterPrice)
        {
            seedPacks[5].GetComponent<Button>().interactable = true;
        }
        else
        {
            seedPacks[5].GetComponent<Button>().interactable = false;
        }
        if (souls >= sunPrice)
        {
            seedPacks[6].GetComponent<Button>().interactable = true;
        }
        else
        {
            seedPacks[6].GetComponent<Button>().interactable = false;
        }
        if (souls >= airPrice)
        {
            seedPacks[7].GetComponent<Button>().interactable = true;
        }
        else
        {
            seedPacks[7].GetComponent<Button>().interactable = false;
        }
    }

    public void Planting()
    {
        if (isPlanting)
        {
            Vector2 mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
            var tilePos = flowerTiles.WorldToCell(mousePos);
            Vector2 centerTile = flowerTiles.GetCellCenterWorld(tilePos);
            flower.GetComponent<SpriteRenderer>().sprite = flowerPackets[plantType];
            //flower.transform.position = new Vector3(Mathf.RoundToInt(mousePos.x) + 0.5f, Mathf.RoundToInt(mousePos.y) + 0.5f, 0);
            flower.transform.position = centerTile;
            if (planting)
            {
                if (Input.GetMouseButtonDown(0) && emptySpace)
                {
                    if (flower.transform.position.x >= -1.5f && flower.transform.position.x <= 3.5f)
                    {
                        if (flower.transform.position.y <= 2.5f && flower.transform.position.y >= -3.5f)
                        {
                            Instantiate(flowerPlants[plantType], flower.transform.position, Quaternion.identity);
                            soundClips[10].Play();
                            foreach (GameObject obj in seedPacks)
                            {
                                obj.GetComponent<Button>().interactable = true;
                            }
                            if (plantType == 0)
                            {
                                souls--;
                            }
                            else if (plantType == 1)
                            {
                                souls -= 2;
                            }
                            else if (plantType == 2)
                            {
                                souls -= 2;
                            }
                            else if (plantType == 3)
                            {
                                souls -= 3;
                            }
                            else if (plantType == 4)
                            {
                                souls -= 3;
                            }
                            planting = false;
                        }
                    }
                }
            }
        }
        else
        {
            Vector2 mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
            var tilePos = flowerTiles.WorldToCell(mousePos);
            Vector2 centerTile = flowerTiles.GetCellCenterWorld(tilePos);
            flower.GetComponent<SpriteRenderer>().sprite = flowerPackets[foodType];
            //flower.transform.position = new Vector3(Mathf.RoundToInt(mousePos.x) + 0.5f, Mathf.RoundToInt(mousePos.y) + 0.5f, 0);
            flower.transform.position = centerTile;
            if (planting)
            {
                if (Input.GetMouseButtonDown(0) && !emptySpace)
                {
                    if (flower.transform.position.x >= -1.5f && flower.transform.position.x <= 3.5f)
                    {
                        if (flower.transform.position.y <= 2.5f && flower.transform.position.y >= -3.5f)
                        {
                            foreach (GameObject obj in seedPacks)
                            {
                                obj.GetComponent<Button>().interactable = true;
                            }
                            planting = false;
                            //food = 0;
                        }
                    }
                }
            }
        }
    }

    public void IsPlanting(bool planting)
    {
        if (planting)
        {
            isPlanting = true;
        }
        else
        {
            isPlanting = false;
        }
    }

    public void SwitchSpeed()
    {
        if (!speeded)
        {
            speeded = true;
            StopAllCoroutines();
            SoulFall.fallSpeed = 6f;
            StartCoroutine(SpawnSoulsFast());
        }
        else if (speeded)
        {
            speeded = false;
            StopAllCoroutines();
            SoulFall.fallSpeed = 4f;
            StartCoroutine(SpawnSouls());
        }
    }

    public void Plant(int num)
    {
        foreach (GameObject obj in seedPacks)
        {
            obj.GetComponent<Button>().interactable = false;
        }
        planting = true;
        plantType = num;
    }

    public void Grow(int num)
    {
        foreach (GameObject obj in seedPacks)
        {
            obj.GetComponent<Button>().interactable = false;
        }
        planting = true;
        foodType = num;
        food = num - 4;
    }

    public void CancelPlanting()
    {
        foreach (GameObject obj in seedPacks)
        {
            obj.GetComponent<Button>().interactable = true;
        }
        planting = false;
        food = 0;
    }

    public void SpriteSwitch()
    {
        if (speedButton.GetComponent<Image>().sprite == sprite1)
        {
            speedButton.GetComponent<Image>().sprite = sprite2;
        }
        else if (speedButton.GetComponent<Image>().sprite == sprite2)
        {
            speedButton.GetComponent<Image>().sprite = sprite1;
        }
    }

    IEnumerator SpawnSouls()
    {
        yield return new WaitForSeconds(Random.Range(2.0f - upgradeLevel, 5.0f - upgradeLevel) + 0.1f);
        Instantiate(soul);
        StartCoroutine(SpawnSouls());
    }

    IEnumerator SpawnSoulsFast()
    {
        yield return new WaitForSeconds((2f - upgradeLevel) + 0.1f);
        Instantiate(soul);
        StartCoroutine(SpawnSoulsFast());
    }
}
