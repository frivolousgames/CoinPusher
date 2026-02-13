using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningCardAdder : MonoBehaviour
{
    [SerializeField]
    AudioSource swooshSound;

    public void PlaySwoosh()
    {
        swooshSound.Play();
    }

    public void DestroyObject()
    {
        Destroy(gameObject);
    }
}
