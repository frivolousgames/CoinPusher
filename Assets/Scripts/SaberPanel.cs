using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaberPanel : MonoBehaviour
{
    private void OnDisable()
    {
        Sabercontroller.saberEnd = false;
    }
}
