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
    Animator panelAnim;

    private void Awake()
    {
        isOut = false;
    }

    private void Update()
    {
        anim.SetBool("isOut", isOut);
        if(panelAnim != null )
        {
            panelAnim.SetBool("isOut", isOut);
        }
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
}
