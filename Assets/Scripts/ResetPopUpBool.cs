using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPopUpBool : MonoBehaviour
{
    private void OnDisable()
    {
        RewardController.isPopUp = false;
    }
}
