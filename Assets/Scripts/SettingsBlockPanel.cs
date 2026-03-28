using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsBlockPanel : MonoBehaviour
{
    Animator anim;
    [SerializeField]
    bool isOpen;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        anim.SetBool("isOpen", isOpen);
    }

    public void Open()
    {
        if(!isOpen)
        {
            isOpen = true;
        } 
    }

    public void Close()
    {
        isOpen = false;
    }
}
