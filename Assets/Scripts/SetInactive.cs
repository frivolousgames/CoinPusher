using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetInactive : MonoBehaviour
{
    public void SetGameObjectInactive()
    {
        gameObject.SetActive(false);
        Debug.Log("Deactivated: " + this.gameObject.name);
    }
}
