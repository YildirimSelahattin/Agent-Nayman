using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class EnemyGunManager : MonoBehaviour
{
    [SerializeField] private GunTypes Gun;
    [SerializeField] private Transform GunParent;
    [SerializeField] List<GunScriptableObject> Guns;
    public static EnemyGunManager Instance = null;
    public GunScriptableObject EnemyGun;

    private void Start() 
    {
        if(Instance ==null){
            Instance = this;
        }
        GunScriptableObject gun = Guns.Find(gun => gun.Type == Gun);
        EnemyGun = gun;
        gun.EnemySpawn(GunParent,this);
        StartCoroutine(ShootAfterDelay(EnemyGun));

    }
    


    public IEnumerator ShootAfterDelay(GunScriptableObject gun){
        yield return new WaitForSeconds(3);
        if (PlayerManager.Instance.gameStarted==true)
        {
        EnemyGun.EnemyShoot(EnemyManager.Instance.enemy,gun);
        }
        StartCoroutine(ShootAfterDelay(gun));

    }
    
}
