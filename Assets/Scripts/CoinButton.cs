using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinButton : MonoBehaviour
{
    Image button;
    [SerializeField]
    Sprite buttonUp;
    [SerializeField]
    Sprite buttonDown;
    Color startColor;
    [SerializeField]
    Color endColor;
    [SerializeField]
    float blinkSpeed;

    private void Awake()
    {
        button = GetComponent<Image>();
        startColor = Color.white;
        StartCoroutine(ButtonGlow());
    }
    private void Update()
    {
        
    }

    IEnumerator ButtonGlow()
    {
        while (true)
        {
            button.color = Color.Lerp(startColor, endColor, Mathf.PingPong(blinkSpeed * Time.deltaTime, 1));
            yield return null;
        }
    }

    public void Clicked()
    {
        //Debug.Log("Clicked");
        if(button.sprite == buttonUp)
        {
            button.sprite = buttonDown;
        }
        else
        {
            button.sprite = buttonUp;
        }
    }
}
