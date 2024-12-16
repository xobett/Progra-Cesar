using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadVictory : MonoBehaviour
{
    [SerializeField] private string victoryScene;

    private void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadScene(victoryScene);
    }
}
