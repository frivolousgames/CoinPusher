using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KidStartPanel : MonoBehaviour
{
    Animator anim;
    [SerializeField]
    bool go;
    public static bool isStarted;
    [SerializeField]
    GameObject themePlayer;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        isStarted = false;
    }

    private void Update()
    {
        anim.SetBool("go", go);
    }

    public void StartButton()
    {
        go = true;
        themePlayer.SetActive(true);
    }

    public void StartPlay()
    {
        isStarted = true;
    }
}
