using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Profe.Weapons
{
    /// <summary>
    /// Este script nos maneja el uso de armas
    /// Controla el inventario de armas
    /// Selecciona cual es el arma que quieres equipar
    /// Y ajusta sus funciones/controles según el arma equipada
    /// </summary>
    public class WeaponHandlerCesar : MonoBehaviour
    {

        [SerializeField] private Weapon[] weapons;
        [SerializeField] public Weapon currentWeapon;

        [SerializeField] private GameObject weapon1;
        [SerializeField] private GameObject weapon2;

        private void Start()
        {
            weapon1.SetActive(true);
            currentWeapon = weapons[0];
        }

        private void Update()
        {
            ChangeWeapon();
            Aim();
            ReloadGun();
        }

        private void Aim()
        {
            if (currentWeapon != null && currentWeapon.GetComponent<Smg>())
            {
                if (Input.GetKey(KeyCode.Mouse0))
                {
                    currentWeapon.Shoot();
                }
            }
            else if (currentWeapon != null && currentWeapon.GetComponent<Handgun>())
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    currentWeapon.Shoot();
                }
            }
        }

        private void ReloadGun()
        {
            if (currentWeapon != null)
            {
                if (Input.GetKeyDown(KeyCode.R))
                {
                    currentWeapon.Reload();
                    AudioManager.instance.Play("Reload Gun");
                }
            }
        }

        private void ChangeWeapon()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                weapon1.SetActive(true);
                weapon2.SetActive(false);

                currentWeapon = weapons[0];
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                weapon2.SetActive(true);
                weapon1.SetActive(false);

                currentWeapon = weapons[1];
            }
            
        }
    }

}
