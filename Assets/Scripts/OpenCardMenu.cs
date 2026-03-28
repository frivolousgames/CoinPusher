using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCardMenu : MonoBehaviour
{
    [SerializeField]
    GameObject cardMenu;
    [SerializeField]
    Animator anim;
    [SerializeField]
    bool isOut;

    [SerializeField]
    GameObject buttonBlock;

    private void Awake()
    {
        isOut = false;
    }

    private void Update()
    {
        anim.SetBool("isOut", isOut);
        //Debug.Log("isOut: " + isOut);
    }

    public void SlideOut()
    {
        //Debug.Log("Clicked");
        if(isOut)
        {
            isOut = false;
        }
        else
        {
            isOut = true;
        }
    }
    public void BlockButtonOn()
    {
        if(buttonBlock != null)
        {
            buttonBlock.SetActive(true);
        }
    }
    public void BlockButtonOff()
    {
        if (buttonBlock != null)
        {
            buttonBlock.SetActive(false);
        }
    }

}
