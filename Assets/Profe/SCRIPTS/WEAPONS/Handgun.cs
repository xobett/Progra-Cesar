using System.Collections;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace Profe.Weapons
{
    /// <summary>
    /// EJERCICIO
    /// 
    /// 
    /// Realizar la lógica detras de la recarga de la pistola como si esta fuera un revolver
    /// 
    /// </summary>
    public class Handgun : Weapon
    {
        protected internal override void Shoot()
        {
            if (actualAmmo > 0)
            {
                actualAmmo--;
                base.Shoot();
            }
        }

        protected internal override void Reload()
        {
            StartCoroutine(ReloadHandgun());
        }

        private IEnumerator ReloadHandgun()
        {      
            if (actualAmmo >= maxAmmo - magazineSize)
            {
                actualAmmo = maxAmmo;
                yield return new WaitForSeconds(reloadTime);
            }
            else if (actualAmmo < maxAmmo)
            {
                actualAmmo += magazineSize;
                yield return new WaitForSeconds(reloadTime);
            }

            yield return null;
        }

    }

}

