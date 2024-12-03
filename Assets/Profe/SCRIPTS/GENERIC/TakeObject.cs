using UnityEngine;



public class TakeObject : MonoBehaviour, IInteractable
{
    public Transform parent;
    public bool hasParent;

    public void Interact()
    {
        if (!hasParent)
        {


            transform.SetParent(parent, false);

            transform.position = Vector3.zero;
        }

    }

}