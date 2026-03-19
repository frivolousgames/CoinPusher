using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KidEndPanel : MonoBehaviour
{
    Animator anim;
    [SerializeField]
    bool isWin;
    [SerializeField]
    bool isLose;
    [SerializeField]
    Text text;
    [SerializeField]
    Text playsText;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        isLose = KidSceneManager.isLost;
        isWin = KidController.isDead;
        if (isWin)
        {
            text.text = "SUCCESS!";
            playsText.text = "+" + KidSceneManager.selectedCredits + " PLAYS";
        }
        else
        {
            text.text = "YOU BLEW IT!";

        }
    }

    private void Update()
    {
        isLose = KidSceneManager.isLost;
        isWin = KidController.isDead;
        anim.SetBool("isWin", isWin);
        anim.SetBool("isLose", isLose);

    }
}
