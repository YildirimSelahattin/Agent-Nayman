using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Shoot Config",menuName = "Guns/Shoot Config",order = 2)]
public class ShootingConfig : ScriptableObject
{
     public LayerMask HitMask;
    
    // Ters orantı bu artarsa rate azalıyo
     public float baseFireRate =0.25f;
     public int fireRateLevel = 0;
     public int damageLevel = 0;
     public float BulletDamage =0.25f;
    public float fireRateIncreasePercentagePerLevel;
    public float damageIncreasePercentagePerLevel;
    public float fireRateUpgradeStartMoney;
    public float fireRateCostIncreasePercentage;
    public float damageUpgradeStartMoney;
    public float damageCostIncreasePercentage;
    

    //Duration artarsa daha yavaş gidiyor 
     public float BulletDuration =2f;
     public float Recoil = 90f;
    public bool isAvaliable;
     public GameObject BulletPrefab;
}
