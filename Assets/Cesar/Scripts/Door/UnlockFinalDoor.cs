using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockFinalDoor : MonoBehaviour
{
    [SerializeField] private int lockedDoors; 

    void Start()
    {
        GetTotalLockedDoors();
    }

    private void GetTotalLockedDoors()
    {
        GameObject[] lockedDoorsInScene = GameObject.FindGameObjectsWithTag("Locked Door");

        foreach (GameObject lockedDoor in lockedDoorsInScene)
        {
            lockedDoors++;
        }
    }

    public void DoorOpened()
    {
        lockedDoors--;

        if (lockedDoors <= 1)
        {
            gameObject.GetComponent<Door>().eventoActivado = true;
            Debug.Log("Final door will open");
        }
    }
}
