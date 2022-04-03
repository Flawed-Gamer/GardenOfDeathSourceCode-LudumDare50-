using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ToolTips : MonoBehaviour
{
    public Vector3 offset;
    public TextMeshProUGUI description;
    public bool isActive;
    public GameObject button;
    public Sprite onSprite;
    public Sprite offSprite;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    private void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mousePos.x, mousePos.y, 0) + offset;
        if (mousePos.x < 0)
        {
            offset = new Vector3(3, -1.3f, 0);
        }
        else
        {
            offset = new Vector3(-3, -1.3f, 0);
        }
    }

    public void CheckActive()
    {
        if (!isActive)
        {
            isActive = true;
            button.GetComponent<Image>().sprite = onSprite;
        }
        else
        {
            isActive = false;
            button.GetComponent<Image>().sprite = offSprite;
        }
    }

    public void TextChange(int text)
    {
        if (isActive)
        {
            gameObject.SetActive(true);
        }
        if (text == 0)
        {
            description.text = "Sunflower: Once fully grown greatly reduces prices for water, sun, and air. No effects after fully grown";
        }
        if (text == 1)
        {
            description.text = "Hydraflower: While fully grown collects any souls that falls on it";
        }
        if (text == 2)
        {
            description.text = "Glowflower: While fully grown, increases brightness of the farm";
        }
        if (text == 3)
        {
            description.text = "ScytheFlower: While fully grown, all souls give one more if collected and take one more if missed";
        }
        if (text == 4)
        {
            description.text = "BushFlower: While fully grown, souls appear faster";
        }
        if (text == 5)
        {
            description.text = "Changes speed at which the souls spawn and fall";
        }
        if (text == 6)
        {
            description.text = "Turns on or off the description box";
        }
    }

    public void HideObject()
    {
        gameObject.SetActive(false);
    }
}
