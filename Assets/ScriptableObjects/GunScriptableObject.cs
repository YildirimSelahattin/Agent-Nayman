using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using DG.Tweening;

[CreateAssetMenu (fileName = "Gun",menuName ="Guns/Gun", order = 0)]
public class GunScriptableObject : ScriptableObject
{
  public GunTypes Type;
  public string Name;
  public GameObject ModelPrefab;
  public Vector3 SpawnPoint;
  public Vector3 SpawnRotation;

  public ShootingConfig ShootingConfig;


  private MonoBehaviour ActiveMonoBehaviour;
  private GameObject Model;
  private float LastShootTime;
  private ParticleSystem ShootSystem;
  GameObject enemybulletTemp;
  
  

  public void Spawn(Transform Parent , MonoBehaviour ActiveMonoBehaviour){
    this.ActiveMonoBehaviour = ActiveMonoBehaviour;
    LastShootTime =0;
 
    Model = Instantiate(ModelPrefab);
    Model.transform.SetParent(Parent, false);
    Model.transform.localPosition = SpawnPoint;
    Model.transform.localRotation = Quaternion.Euler(SpawnRotation);

    ShootSystem = Model.GetComponentInChildren<ParticleSystem>();

  }
  
  public void EnemySpawn(Transform Parent , MonoBehaviour ActiveMonoBehaviour){
    this.ActiveMonoBehaviour = ActiveMonoBehaviour;
    LastShootTime =0;
 
    Model = Instantiate(ModelPrefab);
    Model.transform.SetParent(Parent, false);
    Model.transform.localPosition = SpawnPoint;
    Model.transform.localRotation = Quaternion.Euler(SpawnRotation);

    ShootSystem = Model.GetComponentInChildren<ParticleSystem>();

  }

  public void Shoot(GunScriptableObject gun)
  {

   
    
    GameObject bullet = Instantiate(ShootingConfig.BulletPrefab,gun.Model.transform.GetChild(0).transform.position,ShootingConfig.BulletPrefab.transform.rotation);
    float yspawn;

     yspawn  = SpawnPoint.y - 30f;
    

    
    bullet.transform.DOLocalMoveY(yspawn,ShootingConfig.BulletDuration).OnComplete(()=>{
        Destroy(bullet);
    });
    float modely = gun.Model.transform.GetChild(0).gameObject.transform.localPosition.y + ShootingConfig.Recoil;

    
   gun.Model.transform.GetChild(0).transform.DOLocalRotate(new Vector3(0,modely,0),1f).OnComplete(()=>{
   gun.Model.transform.GetChild(0).transform.DOLocalRotate(new Vector3(0,0,0),0f);
   });
  } 
  public void EnemyShoot(GameObject enemy, GunScriptableObject gun)
  {
  
    enemybulletTemp = Instantiate(ShootingConfig.BulletPrefab,gun.Model.transform.position,ShootingConfig.BulletPrefab.transform.rotation,gun.Model.transform);
    float newyspawn;
    newyspawn  = gun.Model.transform.position.y + 30f;
      Debug.Log(newyspawn);
    enemybulletTemp.transform.DOMoveY(newyspawn,10f).OnComplete(()=>{
      Debug.Log("Sasssss");
        Destroy(enemybulletTemp);

        Debug.Log("merhaba");
    });

  Debug.Log("DoMoveY sonrasi debug");

   float modelnewy = gun.Model.transform.GetChild(0).gameObject.transform.localPosition.y + ShootingConfig.Recoil;
   gun.Model.transform.GetChild(0).transform.DOLocalRotate(new Vector3(0,modelnewy,0),1f).OnComplete(()=>{
   gun.Model.transform.GetChild(0).transform.DOLocalRotate(new Vector3(0,0,0),0f);
   });
  } 
}
