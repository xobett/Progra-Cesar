using UnityEngine;



public class TakeObject : MonoBehaviour, IInteractable
{
    public Transform parent;
    public bool hasParent;

    public void Interact()
    {
        Destroy(gameObject);
        Debug.Log("Interaction works!");
    }

}