using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private GunSelect GunSelector;

    private void Update() {
        if (Input.GetMouseButton(0)&&GunSelector.ActiveGun !=  null)
        {
            GunSelector.ActiveGun.Shoot();
        }
    }
}
