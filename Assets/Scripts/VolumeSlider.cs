using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeSlider : MonoBehaviour
{
    public static VolumeSlider selectedSlider;

    private void Awake()
    {
        selectedSlider = null;
    }

    public void PointerDown()
    {
        selectedSlider = this;
    }
    public void PointerUp()
    {
        selectedSlider = null;
    }
}
