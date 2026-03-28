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

    [SerializeField]
    Animator anim;
    [SerializeField]
    bool enter;

    private void Awake()
    {
        button = GetComponent<Image>();
    }

    private void Start()
    {
        SetButton();
        //Debug.Log("Amount: " + PlayerPrefs.GetInt("Saber", 0));
    }

    private void Update()
    {
        anim.SetBool("enter", enter);
    }

    public void SetButton()
    {
        if (PlayerPrefs.GetInt("Sabers", 0) < 1)
        {
            button.enabled = false;
            multiText.text = string.Empty;
        }
        else
        {
            button.enabled = true;
            multiText.text = "x" + PlayerPrefs.GetInt("Sabers", 0).ToString();
        }
    }

    public void PointerEnter()
    {
        if(button.enabled)
        {
            enter = true;
        }
    }

    public void PointerExit()
    {
        if (button.enabled)
        {
            enter = false;
        }
    }

    public void PointerClick()
    {
        if (button.enabled)
        {
            int amount = PlayerPrefs.GetInt("Sabers", 0);
            //Debug.Log("Amount: " + amount);
            PlayerPrefs.SetInt("Sabers", amount - 1);
            enter = false;
            //Debug.Log("Amount: " + PlayerPrefs.GetInt("Saber", 0));
            RewardController.popUpList.Add("Sabers");
            SetButton();
        }
    }
}
