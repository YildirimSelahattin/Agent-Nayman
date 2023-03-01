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
    [SerializeField]  Transform spawnPos;
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
        
        enemybulletTemp = Instantiate(gun.ShootingConfig.BulletPrefab, spawnPos.position, gun.ShootingConfig.BulletPrefab.transform.rotation, GunParent.transform);
        
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
        if (PlayerManager.Instance.gameStarted==true && PlayerManager.Instance.myAnimator.GetBool("isDead")==false)
        {
        Shoot(EnemyGun);
        }
        StartCoroutine(ShootAfterDelay(gun,3f));

    }
    
}
