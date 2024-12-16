using Profe;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoor : MonoBehaviour, IInteractable
{
    [SerializeField] private SOItem requiredKey;
    public void Interact()
    {
        InventoryHandler playerInventory = FindObjectOfType<InventoryHandler>();

        if (playerInventory.inventory.Contains(requiredKey))
        {
            AudioManager.instance.Play("Access Granted");
            Destroy(this.gameObject);
        }
        else
        {
            AudioManager.instance.Play("Access Denied");
            Debug.Log("You dont have the key!");
        }
    }
}
