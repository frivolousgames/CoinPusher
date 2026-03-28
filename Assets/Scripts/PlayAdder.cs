using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayAdder : MonoBehaviour
{
    [SerializeField]
    Text playAddText;

    [SerializeField]
    float fee;

    ///AUDIO////
    [SerializeField]
    AudioSource playAdd;
    [SerializeField]
    AudioSource playAddDeny;
    
    public void AddPlays()
    {
        if(PlaySceneManager.credits >= fee)
        {
            if (PlaySceneManager.plays <= 979)
            {
                PlaySceneManager.plays += 20;
                PlaySceneManager.credits -= 1.00f;
                playAddText.text = "+20 Plays";
                playAdd.Play();
            }
            else if (PlaySceneManager.plays > 979 && PlaySceneManager.plays < 999)
            {
                playAddText.text = "+20 Plays";
                PlaySceneManager.plays = 999;
                PlaySceneManager.credits -= 1.00f;
                playAdd.Play();
            }
            else
            {
                playAddText.text = "Error";
                playAddDeny.Play();
            }
        }
        else
        {
            playAddText.text = "Not Enough Credits";
            playAddDeny.Play();
        }
    }
    public void ResetPlayAddText()
    {
        playAddText.text = "Swipe Card To Play\r\n$1.00 For 20 Plays";
    }
}
