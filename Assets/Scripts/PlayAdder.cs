using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayAdder : MonoBehaviour
{
    [SerializeField]
    Text playAddText;

    ///AUDIO////
    [SerializeField]
    AudioSource playAdd;
    [SerializeField]
    AudioSource playAddDeny;
    
    public void AddPlays()
    {
        if (SceneManager.plays <= 979)
        {
            SceneManager.plays += 20;
            playAddText.text = "+20 Plays";
            playAdd.Play();
        }
        else if (SceneManager.plays > 979 && SceneManager.plays < 999)
        {
            playAddText.text = "+20 Plays";
            SceneManager.plays = 999;
            playAdd.Play();
        }
        else
        {
            playAddText.text = "Error";
            playAddDeny.Play();
        }
    }
    public void ResetPlayAddText()
    {
        playAddText.text = "Swipe Card To Play\r\n$1.00 For 20 Plays";
    }
}
