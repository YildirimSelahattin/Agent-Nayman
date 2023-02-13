using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using DG.Tweening;
using Unity.VisualScripting;

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
  public GameObject Model;
  private float LastShootTime;
  private ParticleSystem ShootSystem;
  GameObject enemybulletTemp;
  
  

  public void Spawn(Transform Parent , MonoBehaviour ActiveMonoBehaviour){
    this.ActiveMonoBehaviour = ActiveMonoBehaviour;
    LastShootTime =0;
        if (Model != null)
        {
            Destroy(Model.gameObject);
        }
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

     yspawn  = SpawnPoint.z - 30f;
    

    
    bullet.transform.DOLocalMoveZ(yspawn,ShootingConfig.BulletDuration).OnComplete(()=>{
        Destroy(bullet);
    });
    float modely = gun.Model.transform.GetChild(0).gameObject.transform.localPosition.y + ShootingConfig.Recoil;

    
   gun.Model.transform.GetChild(0).transform.DOLocalRotate(new Vector3(0,modely,0),1f).OnComplete(()=>{
   gun.Model.transform.GetChild(0).transform.DOLocalRotate(new Vector3(0,0,0),0f);
   });
  } 

  public void EnemyShoot(GameObject enemy, GunScriptableObject gun)
  {
  
    enemybulletTemp = Instantiate(ShootingConfig.BulletPrefab,EnemyManager.Instance.enemy.transform.position,ShootingConfig.BulletPrefab.transform.rotation);
    float newyspawn;
    newyspawn  = gun.Model.transform.position.y + 30f;
      Debug.Log(newyspawn);
    enemybulletTemp.transform.DOMoveZ(newyspawn,10f).OnComplete(()=>{
      
        Destroy(enemybulletTemp);

      
    });

  

   
  } 
}
