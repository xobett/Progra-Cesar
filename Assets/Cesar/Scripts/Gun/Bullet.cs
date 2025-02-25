using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().Death();
        }
        else if (collision.gameObject.CompareTag("Fouled Switch"))
        {
            collision.gameObject.GetComponent<FouledSwitch>().FouledSwitchDeath();
        }
        Destroy(gameObject, 3);
    }
}
