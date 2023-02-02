using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[DisallowMultipleComponent]
public class EnemyGunManager : MonoBehaviour
{
    [SerializeField] private GunTypes Gun;
    [SerializeField] private Transform GunParent;
    [SerializeField] List<GunScriptableObject> Guns;
    public static EnemyGunManager Instance = null;
    public GunScriptableObject EnemyGun;
    
    GameObject enemybulletTemp;


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
    
    public void Shoot(GunScriptableObject gun)
{
    if (EnemyGun.ShootingConfig.BulletPrefab != null)
    {
        Vector3 spawnPos = transform.GetChild(transform.childCount-1).transform.position;
        
        enemybulletTemp = Instantiate(EnemyGun.ShootingConfig.BulletPrefab, spawnPos, EnemyGun.ShootingConfig.BulletPrefab.transform.rotation,transform.GetChild(transform.childCount-1));
        
        enemybulletTemp.transform.DOLocalMoveZ(-10f,EnemyGun.ShootingConfig.BulletDuration).SetEase(Ease.Linear).OnComplete(()=>{
            Destroy(enemybulletTemp);
        });
    }
    else
    {
        Debug.LogError("EnemyGun.ShootingConfig.BulletPrefab is null");
    }
}


    public IEnumerator ShootAfterDelay(GunScriptableObject gun){
        yield return new WaitForSeconds(3);
        if (PlayerManager.Instance.gameStarted==true)
        {
        Shoot(EnemyGun);
        }
        StartCoroutine(ShootAfterDelay(gun));

    }
    
}
