using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class GunManager : MonoBehaviour
{
    [SerializeField] public GunTypes Gun;
    [SerializeField] private Transform GunParent;
    [SerializeField] List<GunScriptableObject> Guns;
    public static GunManager Instance = null;
    public GunScriptableObject ActiveGun;

    private void Start() 
    {
        if(Instance ==null){
            Instance = this;
        }
        /*
        GunScriptableObject gun = Guns.Find(gun => gun.Type == Gun);
        ActiveGun = gun;
        gun.Spawn(GunParent,this);
        StartCoroutine(ShootAfterDelay(ActiveGun));
        */
    }
    
    public void SpawnGun(GunTypes gunType){
        GunScriptableObject gun = Guns.Find(gun => gun.Type == gunType);
        ActiveGun = gun;
        gun.Spawn(GunParent,this);
        StartCoroutine(ShootAfterDelay(ActiveGun));
    }

    public IEnumerator ShootAfterDelay(GunScriptableObject gun){
        yield return new WaitForSeconds(ActiveGun.ShootingConfig.FireRate);
        if (PlayerManager.Instance.gameStarted==true)
        {
        ActiveGun.Shoot(gun);
        }
        StartCoroutine(ShootAfterDelay(gun));

    }
    
}
