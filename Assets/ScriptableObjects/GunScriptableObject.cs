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
  

  public void Spawn(Transform Parent , MonoBehaviour ActiveMonoBehaviour){
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

    float x =PlayerManager.Instance.agent.transform.rotation.x ;
    float y =PlayerManager.Instance.agent.transform.rotation.y ;
    float z =PlayerManager.Instance.agent.transform.rotation.z ;
    x+= 100f;
    GameObject bullet = Instantiate(ShootingConfig.BulletPrefab,gun.Model.transform.position,ShootingConfig.BulletPrefab.transform.rotation);
    float yspawn  = SpawnPoint.y + 30f;
    
    bullet.transform.DOLocalMoveY(yspawn,ShootingConfig.BulletDuration).OnComplete(()=>{
        Destroy(bullet);
    });
    float modelX = gun.Model.transform.rotation.x+30;
   gun.Model.transform.DORotate(new Vector3(modelX,SpawnPoint.y,SpawnPoint.z),0.2f).OnComplete(()=>{
  gun.Model.transform.DORotate(new Vector3(x,y,z),0f);

   });
  } 
}
