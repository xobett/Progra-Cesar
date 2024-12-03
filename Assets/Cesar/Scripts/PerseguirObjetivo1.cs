using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// /// EJERCICIO/TAREA
/// 
/// Hacer que deje de perseguir al objetivo
/// </summary>
/// 

namespace Cesar
{
    public class PerseguirObjetivo : MonoBehaviour
    {
        public Transform objetivo;
        public float velocidad;

        private NavMeshAgent agent;

        private void Start()
        {
            agent = GetComponent<NavMeshAgent>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void Perseguir()
        {
            agent.speed = velocidad;
            agent.destination = objetivo.position;
        }

        public void DejarDePerseguir()
        {
            agent.speed = 3.5f;
        }

    }

}
