using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassExplosion : MonoBehaviour
{
    [SerializeField]
    AudioSource audioPlayer;

    [SerializeField]
    AudioClip[] clips;

    private void Awake()
    {
        audioPlayer.clip = clips[Random.Range(0, clips.Length)];
        audioPlayer.Play();
    }
}
