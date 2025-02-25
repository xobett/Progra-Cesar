using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockDoorPostSwitches : MonoBehaviour
{
    [SerializeField] private int totalFouledSwitches;

    [SerializeField] private GameObject doorLocked;
    void Start()
    {
        totalFouledSwitches = transform.childCount;
    }

    public void SubstractFouledSwitch()
    {
        totalFouledSwitches--;

        if (totalFouledSwitches <= 0)
        {
            doorLocked.GetComponent<Door>().eventoActivado = true;
        }
    }
}
