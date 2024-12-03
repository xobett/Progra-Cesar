using UnityEngine;

/// <summary>
/// EJERCICIO/TAREA
/// 
/// Tienen que hacer que cuando se deje de detectar al objetivo después de X cantidad de tiempo, este regrese a el ultimo punto de patrullaje al que fue
/// 
/// </summary>
/// 

namespace Cesar
{
    public class Deteccion : MonoBehaviour
    {

        public float radioDeDetección;
        public LayerMask layer;

        private Patrullaje patrullador;
        private PerseguirObjetivo perseguir;

        private bool playerDetectado;

        private void Start()
        {
            patrullador = GetComponent<Patrullaje>();
            perseguir = GetComponent<PerseguirObjetivo>();
        }

        private void Update()
        {
            Detectar();
        }

        private void Detectar()
        {
            if (Physics.CheckSphere(transform.position, radioDeDetección, layer))
            {
                patrullador.DejarDePatrullar();
                perseguir.Perseguir();
                playerDetectado = true;
            }
            else if (playerDetectado)
            {
                patrullador.RegresarAPatrullaje();
                perseguir.DejarDePerseguir();
                playerDetectado = false;
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, radioDeDetección);
        }
    }

}
