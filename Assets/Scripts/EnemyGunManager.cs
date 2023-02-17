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

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            StartShoot();
        }
    }
    public void StartShoot()
    {
        GunScriptableObject gun = Guns.Find(gun => gun.Type == Gun);
        EnemyGun = gun;
        StartCoroutine(ShootAfterDelay(gun, 0.3f));
    }
    public void Shoot(GunScriptableObject gun)
{
    if (gun.ShootingConfig.BulletPrefab != null)
    {
        Vector3 spawnPos = transform.GetChild(transform.childCount-2).transform.position;
        
        enemybulletTemp = Instantiate(gun.ShootingConfig.BulletPrefab, spawnPos, gun.ShootingConfig.BulletPrefab.transform.rotation, GunParent.transform);
        
        enemybulletTemp.transform.DOLocalMoveZ(-20f,gun.ShootingConfig.BulletDuration).SetEase(Ease.Linear).OnComplete(()=>{
            Destroy(enemybulletTemp);
        });
    }
    else
    {
        Debug.LogError("EnemyGun.ShootingConfig.BulletPrefab is null");
    }
}


    public IEnumerator ShootAfterDelay(GunScriptableObject gun,float waitingTime){

        yield return new WaitForSeconds(waitingTime);
        if (PlayerManager.Instance.gameStarted==true)
        {
        Shoot(EnemyGun);
        }
        StartCoroutine(ShootAfterDelay(gun,3f));

    }
    
}
