using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GoldShotObject", menuName = "Gold Shot Object")]
public class GoldShotObject : ScriptableObject
{
    public string objectName;
    public Sprite objectPic;
    public GameObject[] objectGO;
}
