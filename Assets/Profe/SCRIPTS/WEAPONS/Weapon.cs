using UnityEngine;

namespace Profe.Weapons
{
    /// <summary>
    /// 
    /// METODOS ABSTRACTOS SI O SI LOS TENGO QUE IMPLEMENTAR
    /// 
    /// METODOS VIRTUALES NO ESTOY OBLIGADO A IMPLEMENTARLO
    /// 
    /// </summary>
    public abstract class Weapon : MonoBehaviour
    {
        [SerializeField] protected int actualAmmo; // Municion actual de el arma
        [SerializeField] protected int magazineSize; //  tama�o de el cargador
        [SerializeField] protected int maxAmmo; // capacidad maxima de almacenamiento de municion
        [SerializeField] protected float reloadTime; //  tiempo de recarga

        [SerializeField] protected float fireRate; // cadencia
        [SerializeField] protected internal float range; // alcance de el arma

        [SerializeField] protected int damage; // da�o

        [SerializeField] protected LayerMask detection; // a que se le puede disparar

        protected RaycastHit target;

        // Instruccion 1, es una relga hecha por el maestro
        protected internal virtual void Shoot()
        {
            // bajarte puntos
            if (Physics.Raycast(transform.position, transform.forward * range, out target, range, detection))
            {
                target.collider.GetComponent<IDamageable>().TakeDamage(damage);
            }
        }

        // Instruccion 2, es una regla hecha por la escuela
        protected internal abstract void Reload();

        protected void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, transform.forward * range);
        }

    }
}