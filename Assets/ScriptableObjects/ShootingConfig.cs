using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Shoot Config",menuName = "Guns/Shoot Config",order = 2)]
public class ShootingConfig : ScriptableObject
{
     public LayerMask HitMask;
    
    // Ters orantı bu artarsa rate azalıyo
     public float FireRate =0.25f;
     public float BulletDamage =0.25f;

    //Duration artarsa daha yavaş gidiyor 
     public float BulletDuration =2f;

     public GameObject BulletPrefab;
}
