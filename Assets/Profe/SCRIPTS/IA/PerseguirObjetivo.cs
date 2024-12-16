
using UnityEngine;
using UnityEngine.AI;


namespace Profe
{
    /// <summary>
    /// /// EJERCICIO/TAREA
    /// 
    /// Hacer que deje de perseguir al objetivo
    /// </summary>
    public class PerseguirObjetivo : MonoBehaviour
    {
        [SerializeField] private GameObject explosionParticles;

        public Transform objetivo;
        public float velocidad;

        [SerializeField] private int damage = 10; 

        private NavMeshAgent agent;

        private void Start()
        {
            objetivo = GameObject.FindGameObjectWithTag("Player").transform;

            agent = GetComponent<NavMeshAgent>();

            agent.destination = objetivo.position;
        }

        // Update is called once per frame
        void Update()
        {
            Perseguir();
            Explosion();
        }

        public void Perseguir()
        {
            agent.speed = velocidad;
            agent.destination = objetivo.position;
        }

        public void Explosion()
        {
            if (Vector3.Distance(transform.position, objetivo.position) < 1f)
            {
                AudioManager.instance.Play("EnemyExplosion");

                Instantiate(explosionParticles, transform.position, Quaternion.identity);
                objetivo.GetComponent<PlayerHealth>().TakeDamage(damage);

                Destroy(this.gameObject);
            }
        }

    }

}