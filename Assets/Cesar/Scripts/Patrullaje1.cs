using System.Collections;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// EJERCICIO/TAREA
/// 
/// Al regresar a patrullar debe de ir al ultimo punto a donde se dirigia antes de detectar al objetivo. Y regresar a su velocidad de patrullaje.
/// Y continuar patrullando
/// 
/// </summary>
/// 

namespace Cesar
{
    public class Patrullaje : MonoBehaviour
    {
        public Transform[] puntosDePatrullaje;
        public float tiempoDeVigilancia;

        public int lastDestination;

        private NavMeshAgent agent;

        private void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            StartCoroutine(Patrullar());
        }

        private IEnumerator Patrullar()
        {
            Transform randomPos = RandomPos();

            agent.destination = randomPos.position;

            yield return new WaitUntil(() => Vector3.Distance(transform.position, randomPos.position) < 2);

            Debug.Log("Ya lleg� al punto");

            yield return new WaitForSeconds(tiempoDeVigilancia);

            StartCoroutine(Patrullar());
        }

        private IEnumerator RegresarAlUltimoDestino()
        {
            agent.destination = puntosDePatrullaje[lastDestination].position;

            yield return null;
        }

        public void DejarDePatrullar()
        {
            StopAllCoroutines();
        }

        public void RegresarAPatrullaje()
        {
            StartCoroutine(RegresarAlUltimoDestino());
            StopAllCoroutines();
            StartCoroutine(Patrullar());
        }

        private Transform RandomPos()
        {
            int randomPoint = Random.Range(0, puntosDePatrullaje.Length);
            lastDestination = randomPoint;
            return puntosDePatrullaje[randomPoint];
        }

    }
}
