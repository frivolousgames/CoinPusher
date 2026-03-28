using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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

    [SerializeField]
    AudioSource endSound;
    [SerializeField]
    AudioClip winClip;
    [SerializeField]
    AudioClip loseClip;

    bool isCollected;
    [SerializeField]
    GameObject collectAdder;
    [SerializeField]
    Transform canvas;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        isLose = KidSceneManager.isLost;
        isWin = KidController.isDead;
        if (isWin)
        {
            text.text = "SUCCESS!";
            playsText.text = "BALANCE: $" + KidSceneManager.selectedCredits + ".00";
            endSound.clip = winClip;
            endSound.Play(); 
        }
        else
        {
            text.text = "YOU BLEW IT!";
            endSound.clip = loseClip;
            endSound.Play();
        }
    }

    private void Update()
    {
        isLose = KidSceneManager.isLost;
        isWin = KidController.isDead;
        anim.SetBool("isWin", isWin);
        anim.SetBool("isLose", isLose);

    }
    public void ClickYes()
    {
        SceneManager.LoadSceneAsync("Kid Attack");
    }

    public void ClickNo()
    {
        Application.Quit();
    }

    public void ClickCollect()
    {
        if(!isCollected)
        {
            isCollected = true;
            var adder = Instantiate(collectAdder, canvas);
            adder.GetComponent<Text>().text = "+$" + KidSceneManager.selectedCredits + ".00";
            StartCoroutine(ExitScene());
        }
    }

    IEnumerator ExitScene()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Play Screen");
    }
}
