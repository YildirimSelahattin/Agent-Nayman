using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class GunManager : MonoBehaviour
{
    [SerializeField] private GunTypes Gun;
    [SerializeField] private Transform GunParent;
    [SerializeField] List<GunScriptableObject> Guns;
    public static GunManager Instance = null;
    public GunScriptableObject ActiveGun;

    private void Start() 
    {
        if(Instance ==null){
            Instance = this;
        }
        GunScriptableObject gun = Guns.Find(gun => gun.Type == Gun);
        
        ActiveGun = gun;
        gun.Spawn(GunParent,this);
        StartCoroutine(ShootAfterDelay());
    }

    public IEnumerator ShootAfterDelay(){
        yield return new WaitForSeconds(ActiveGun.ShootingConfig.FireRate);
        ActiveGun.Shoot();
        if (true)
        {
        StartCoroutine(ShootAfterDelay());
        }
    }
    
}
