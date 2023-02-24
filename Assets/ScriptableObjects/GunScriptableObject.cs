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
    

        Debug.Log("bbb" + gun.Model.transform.GetChild(0).gameObject);
        Quaternion modely = gun.Model.transform.GetChild(0).gameObject.transform.localRotation;
        modely.y += ShootingConfig.Recoil;
       
        Quaternion originalRot = gun.Model.transform.GetChild(0).gameObject.transform.localRotation;
        Debug.Log("aaaa"+modely);

        
   gun.Model.transform.GetChild(0).transform.DOLocalRotateQuaternion(modely,0.3f).OnComplete(()=>{

       GameObject bullet = Instantiate(ShootingConfig.BulletPrefab, gun.Model.transform.GetChild(0).transform.position, ShootingConfig.BulletPrefab.transform.rotation);
       float yspawn;

       yspawn = PlayerManager.Instance.agent.transform.position.z + 30f;
       
       bullet.transform.DOMoveZ(yspawn, ShootingConfig.BulletDuration).OnComplete(() => {

           Destroy(bullet);
       });
       gun.Model.transform.GetChild(0).transform.DOLocalRotateQuaternion(originalRot,1f);
   });
  } 

  public void EnemyShoot(GameObject enemy, GunScriptableObject gun)
  {
  
    enemybulletTemp = Instantiate(ShootingConfig.BulletPrefab,EnemyManager.Instance.enemy.transform.position,ShootingConfig.BulletPrefab.transform.rotation);
    float newyspawn;
    newyspawn  = gun.Model.transform.position.y + 30f;
     
    enemybulletTemp.transform.DOMoveZ(newyspawn,10f).OnComplete(()=>{
      
        Destroy(enemybulletTemp);

      
    });

  

   
  } 
}
