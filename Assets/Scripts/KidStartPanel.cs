using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KidStartPanel : MonoBehaviour
{
    Animator anim;
    public static bool isStarted;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        isStarted = false;
    }

    private void Update()
    {
        anim.SetBool("isStarted", isStarted);
    }

    public void StartButton()
    {
        isStarted = true;
    }
}
