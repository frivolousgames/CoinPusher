using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleButton : MonoBehaviour
{
    [SerializeField]
    Animator anim;
    //[SerializeField]
    //float offset;
    [SerializeField, HideInInspector]
    bool hovering;
    [SerializeField]
    Color hoverColor;
    [SerializeField]
    Color pressedColor;
    [SerializeField]
    Color idleColor;
    [SerializeField]
    Image buttonImage;

    public static bool isClicked;
    public static string selectedButton;
    private void Awake()
    {
        isClicked = false;
        selectedButton = null;
    }
    private void Start()
    {
        buttonImage.color = idleColor;
    }

    private void Update()
    {
        //anim.SetFloat("Offset", offset);
        anim.SetBool("Hovering", hovering);
    }

    public void PointerEnter()
    {
        hovering = true;
        buttonImage.color = hoverColor;
    }
    public void PointerExit()
    {
        hovering = false;
        buttonImage.color = idleColor;
    }

    public void PointerClick()
    {
        if(!isClicked)
        {
            
            isClicked = true;
            selectedButton = transform.parent.name;
            Debug.Log("Clicked: " + selectedButton);
        }
    }
    public void PointerDown()
    {
        buttonImage.color = pressedColor;
        hovering = false;
    }
}
