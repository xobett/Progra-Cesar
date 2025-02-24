using Profe;
using System.Collections;
using UnityEngine;

// Tipos de puerta: Automatica, Normal, DeLlave, Evento, MultiplesLlaves
public class Door : MonoBehaviour, IInteractable
{
    [Header("TIPO DE PUERTA")]
    [SerializeField] public TipoDePuerta tipoDePuerta;

    [Header("TIPO EVENTO SETTINGS")]
    [SerializeField] public bool eventoActivado;

    [Header("TIPO LLAVE SETTINGS")]
    [SerializeField] public SOItem key;

    [Header("TIPO MULTIPLES LLAVES SETTINGS")]
    [SerializeField] public SOItem[] keys = new SOItem[3];

    [Header("TIPO AUTOMATICA SETTINGS")]
    [SerializeField, Range(0, 5)] public float slideDoorSpeed;

    public bool isAutomatic;
    public bool doorIsUp;

    public Vector3 slideDownPos;
    public Vector3 slideUpPos;

    public InventoryHandler inventoryHandler;

    private void Awake()
    {
        //Busca el inventario del jugador
        inventoryHandler = FindObjectOfType<InventoryHandler>();
    }

    public void Interact()
    {
        switch (tipoDePuerta)
        {
            case TipoDePuerta.Automatica:
                {
                    Automatica();
                    break;
                }

            case TipoDePuerta.Normal:
                {
                    Normal();
                    break;
                }

            case TipoDePuerta.DeLlave:
                {
                    DeLlave();
                    break;
                }

            case TipoDePuerta.Evento:
                {
                    Evento();
                    break;
                }

            case TipoDePuerta.MultiplesLlaves:
                {
                    MultiplesLlaves();
                    break;
                }
        }

    }

    private void Automatica()
    {
        //Guarda la posicion inicial de la puerta.
        slideDownPos = transform.position;
        //Crea la posicion a donde subira la puerta, guardando la posicion actual mas 3 unidades hacia arriba.
        slideUpPos = transform.position + new Vector3(0, 3, 0);

        //Activa el modo automatico de la puerta.
        isAutomatic = true;
    }

    private void Normal()
    {
        //Destruye la puerta.
        Destroy(this.gameObject);
    }

    private void Evento()
    {
        //Comprueba si se ha completado el evento.
        if (eventoActivado)
        {
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("El evento requerido aun no se ha completado!");
        }
    }

    private void MultiplesLlaves()
    {
        //Comprueba que en el inventario se encuentren las llaves requeridas.
        if (VerifyNeededKey(keys[0]) && VerifyNeededKey(keys[1]) && VerifyNeededKey(keys[2]))
        {
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("No tienes las llaves requeridas.");
        }
    }

    private void DeLlave()
    {
        //Comprueba que en el inventario se encuentre la llave requerida.
        if (VerifyNeededKey(key))
        {
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("No tienes la llave requerida");
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        //Al entrar a la zona de trigger, verifica primero si es automatica y la puerta no ha subido.
        //Tras ello, manda a llamar el metodo que mueve la puerta, mandando como parametro la posicion a donde la movera.
        if (isAutomatic && !doorIsUp)
        {
            StartCoroutine(SlideDoor(slideUpPos));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //Al salir de la zona de trigger, verifica primero si es automatica y si la puerta ya ha subido.
        //Tras ello, manda a llamar el metodo que mueve la puerta, para bajar la puerta de nuevo a la posicion inicial.
        if (isAutomatic && doorIsUp)
        {
            StartCoroutine(SlideDoor(slideDownPos));
        }
    }

    private bool VerifyNeededKey(SOItem keyToVerify)
    {
        //Regresa un booleano confirmando si se encuentra la llave requerida en el inventario.
        return inventoryHandler.inventory.Contains(keyToVerify);
    }

    private IEnumerator SlideDoor(Vector3 posToSlide)
    {
        //Este metodo mueve la puerta, necesitando como parametro una posicion a donde la movera.

        //Primero se guarda en una variable la distancia total entre ambos puntos.
        //Segundo se crea otra variable donde se ira restando la distancia total, en ella se guarda primero la distancia de la primera variable hecha.
        float totalDistance = Vector3.Distance(transform.position, posToSlide);
        float remainingDistance = totalDistance;

        while (remainingDistance > 0)
        {
            //Mueve el la posicion del objeto de un punto a otro con velocidad fija. 
            transform.position = Vector3.Lerp(transform.position, posToSlide, 1 - (remainingDistance / totalDistance));
            remainingDistance -= Time.deltaTime * slideDoorSpeed;
            yield return null;
        }

        //Invierte el valor del booleano que indica si la puerta ha sido movida o no.
        doorIsUp = !doorIsUp;
        StopAllCoroutines();
    }

}

public enum TipoDePuerta
{
    Automatica, Normal, DeLlave, Evento, MultiplesLlaves
}
