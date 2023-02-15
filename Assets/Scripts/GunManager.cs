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
    public float timeCounter = 0;
    public float currentFireRate ;
    public int currentDamage ;
    public float inGameFireRateDecreaseAmount;
    public float inGameDamageIncreaseAmount;
    private void Start() 
    {
        if(Instance ==null){
            Instance = this;
        }
        if (Guns[GameDataManager.Instance.currentGun].ShootingConfig.isAvaliable == true)
        {
            Debug.Log("asa");

            GunManager.Instance.SpawnGun((GunTypes)GameDataManager.Instance.currentGun);
        }
        EditCurrentDamage();
        EditCurrentFireRate();

        
    }
    private void Update() {
        if(PlayerManager.Instance.gameStarted == true)
        {
            timeCounter += Time.deltaTime;
            if (currentFireRate - inGameFireRateDecreaseAmount < timeCounter)
            {
                ActiveGun.Shoot(ActiveGun);
                timeCounter = 0;
            }
        }
        
    }
    public void SpawnGun(GunTypes gunType){
      
        GunScriptableObject gun = Guns.Find(gun => gun.Type == gunType);
        
        ActiveGun = gun;
        if (GunParent.transform.childCount > 1)
        {
            Debug.Log("sil");
            Destroy(GunParent.transform.GetChild(1).gameObject);
        }
        gun.Spawn(GunParent,this);
        currentFireRate = ActiveGun.ShootingConfig.baseFireRate * (Mathf.Pow(ActiveGun.ShootingConfig.fireRateIncreasePercentagePerLevel, ActiveGun.ShootingConfig.fireRateLevel));
        EditCurrentDamage();
        EditCurrentFireRate();
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
    public float EditCurrentFireRate()
    {
        currentFireRate = ActiveGun.ShootingConfig.baseFireRate * ((Mathf.Pow(1-ActiveGun.ShootingConfig.fireRateIncreasePercentagePerLevel, ActiveGun.ShootingConfig.fireRateLevel)));
        return currentFireRate;
    }
   
    public int EditCurrentDamage()
    {
        currentDamage =(int)( ActiveGun.ShootingConfig.BulletDamage * (Mathf.Pow(1+ActiveGun.ShootingConfig.damageIncreasePercentagePerLevel, ActiveGun.ShootingConfig.damageLevel)));
        return currentDamage;
    }
}
