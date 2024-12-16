using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemy;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    private IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(3);

        Instantiate(enemy, transform.position, transform.rotation);

        StartCoroutine(SpawnEnemy());
    }
}

