using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Profe.Weapons
{
    public class Smg : Weapon
    {
        private float nextFire;
        protected internal override void Shoot()
        {
            if (actualAmmo > 0 && Time.time > nextFire)
            {
                actualAmmo--;
                base.Shoot();
                nextFire = Time.time + fireRate;
            }
        }

        protected internal override void Reload()
        {
            if (actualAmmo >= maxAmmo - magazineSize)
            {
                actualAmmo = maxAmmo;
            }
            else if (actualAmmo < maxAmmo)
            {
                actualAmmo += magazineSize;
            }
        }
    } 
}
