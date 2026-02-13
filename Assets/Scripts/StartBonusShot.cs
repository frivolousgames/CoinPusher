using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StartBonusShot : MonoBehaviour
{
    [SerializeField]
    UnityEvent startShootRoutine;

    public void StartRoutine()
    {
        startShootRoutine.Invoke();
    }
}
