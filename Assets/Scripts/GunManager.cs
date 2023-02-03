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
    public float  decreaseAmount;
    public float timeCounter = 0;
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
    private void Update() {
        timeCounter+=Time.deltaTime;
        if(ActiveGun.ShootingConfig.FireRate-decreaseAmount<timeCounter){
            if (PlayerManager.Instance.gameStarted==true)
        {
            ActiveGun.Shoot(ActiveGun);
        }
        timeCounter = 0;
        }
    }
    public void SpawnGun(GunTypes gunType){
        GunScriptableObject gun = Guns.Find(gun => gun.Type == gunType);
        ActiveGun = gun;
        gun.Spawn(GunParent,this);
        decreaseAmount = gun.ShootingConfig.decreaseAmount;
        //StartCoroutine(ShootAfterDelay(ActiveGun));
    }

    /*public IEnumerator ShootAfterDelay(GunScriptableObject gun){
        yield return new WaitForSeconds(ActiveGun.ShootingConfig.FireRate-decreaseAmount);
        if (PlayerManager.Instance.gameStarted==true)
        {
        ActiveGun.Shoot(gun);
        }
        StartCoroutine(ShootAfterDelay(gun));

    }*/
    
}
