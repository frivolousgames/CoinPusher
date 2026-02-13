using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaberButton : MonoBehaviour
{
    Image button;
    [SerializeField]
    Text multiText;
    [SerializeField]
    GameObject useImage;
    [SerializeField]
    Text useText;

    private void Awake()
    {
        button = GetComponent<Image>();
    }

    private void Start()
    {
        SetButton();
    }

    void SetButton()
    {
        if (PlayerPrefs.GetInt("Saber", 0) < 1)
        {
            button.enabled = false;
            multiText.text = string.Empty;
        }
        else
        {
            button.enabled = true;
            multiText.text = "x" + PlayerPrefs.GetInt("Saber", 0).ToString();
        }
    }

    public void PointerEnter()
    {
        if(button.enabled)
        {
            useImage.SetActive(true);
        }
    }

    public void PointerExit()
    {
        if (button.enabled)
        {
            useImage.SetActive(false);
        }
    }

    public void PointerClick()
    {
        if (button.enabled)
        {
            int amount = PlayerPrefs.GetInt("Saber", 0);
            PlayerPrefs.SetInt("Saber", amount--);
            useImage.SetActive(false);
            RewardController.popUpList.Add("Saber");
            if (amount == 1)
            {
                button.enabled = false;
                multiText.text = string.Empty;
            }
        }
    }
}
