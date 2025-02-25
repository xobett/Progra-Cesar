using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject keySpawner;

    public void Death()
    {
        keySpawner.GetComponent<UnlockDoorKey>().SubstractEnemyCount();
    }
}
