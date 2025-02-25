using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockDoorKey : MonoBehaviour
{
    [Header("SPAWN KEY SETTINGS")]
    [SerializeField] private Transform spawnKeyPos;
    [SerializeField] private GameObject keyPrefab;

    private int totalEnemies;

    private GameObject enemiesGroup;
 
    void Start()
    {
        enemiesGroup = transform.parent.GetChild(4).gameObject;
        totalEnemies = enemiesGroup.transform.childCount;
    }

    public void SubstractEnemyCount()
    {
        totalEnemies--;

        if (totalEnemies <= 0)
        {
            Debug.Log("Key should appear");
            SpawnKey();
        }
    }

    private void SpawnKey()
    {
        Instantiate(keyPrefab, spawnKeyPos.position, Quaternion.identity);
    }
}
