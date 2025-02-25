using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FouledSwitch : MonoBehaviour
{
    private GameObject fouledSwitchesGroup;

    public void FouledSwitchDeath()
    {
        fouledSwitchesGroup = transform.parent.gameObject;
        fouledSwitchesGroup.GetComponent<UnlockDoorPostSwitches>().SubstractFouledSwitch();
        Destroy(gameObject);
    }
}
