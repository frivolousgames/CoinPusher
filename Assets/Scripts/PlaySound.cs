using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    [SerializeField]
    AudioSource sound;

    public void PlayASound()
    {
        sound.Play();
    }
}
