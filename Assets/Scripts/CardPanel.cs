using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardPanel : MonoBehaviour
{
    [SerializeField]
    Image[] cardImages;

    [SerializeField]
    Color nullColor;

    [SerializeField]
    Text[] multiText;

    private void Start()
    {
        SetCardAmounts();
    }

    public void SetCardAmounts()
    {
        for (int i = 0; i < SceneManager.cardArray.Length;  i++)
        {
            if (SceneManager.cardArray[i] == 0)
            {
                cardImages[i].color = nullColor;
                multiText[i].gameObject.SetActive(false);
            }
            else if (SceneManager.cardArray[i] == 1)
            {
                cardImages[i].color = Color.white;
                multiText[i].gameObject.SetActive(false);
            }
            else
            {
                cardImages[i].color = Color.white;
                multiText[i].text = "x" + SceneManager.cardArray[i].ToString();
                multiText[i].gameObject.SetActive(true);
            }
        }
    }
}
