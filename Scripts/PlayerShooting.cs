using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour
{
    public Gun gun;


    void Update()
    {
        //if(Input.GetMouseButton(0) && gun != null && gun.currentAmmo > 0)
        //{
        //    gun.Shoot();
        //    gun.isShooting = true;
        //}
        //else if (gun != null )
        //{
        //    gun.isShooting = false;
        //}

        if (gun == null) return;

        if (Input.GetMouseButton(0))
        {
            gun.Shoot();
        }
    }
}
