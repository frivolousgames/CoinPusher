using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaberRoutine : MonoBehaviour
{
    [SerializeField]
    AudioClip[] saberClips;
    [SerializeField]
    AudioSource saberSource;

    [SerializeField]
    GameObject saberPanel;

    public void OpenSaber()
    {
        saberSource.clip = saberClips[0];
        saberSource.Play();
    }
    public void MoveSaber()
    {
        saberSource.clip = saberClips[1];
        saberSource.Play();
    }

    public void SliceSaber()
    {
        saberSource.clip = saberClips[2];
        saberSource.Play();
    }
    public void CloseSaber()
    {
        saberSource.clip = saberClips[3];
        saberSource.Play();
    }

    private void OnDisable()
    {
        Sabercontroller.saberEnd = true;
    }
}
