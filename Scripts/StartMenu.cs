using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public static int sound;
    public static int musicSound;
    public Slider soundSlider;
    public Slider musicSlider;
    public GameObject settingsMenu;
    public GameObject controlsMenu;

    private void Start()
    {
        sound = 100;
        musicSound = 100;
    }

    private void Update()
    {
        sound = Mathf.RoundToInt(soundSlider.value);
        musicSound = Mathf.RoundToInt(musicSlider.value);
    }

    public void OpenSettings()
    {
        if (settingsMenu.activeSelf == true)
        {
            settingsMenu.SetActive(false);
        }
        else
        {
            settingsMenu.SetActive(true);
        }
    }

    public void OpenControls()
    {
        if (controlsMenu.activeSelf == true)
        {
            controlsMenu.SetActive(false);
        }
        else
        {
            controlsMenu.SetActive(true);
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("IntroScene");
    }
}
