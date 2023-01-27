using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other) {
        
        if (other.tag == "Enemy")
        {
           float x = GunManager.Instance.ActiveGun.ShootingConfig.BulletDamage;
            other.gameObject.GetComponent<EnemyManager>().getHit(x);
            Destroy(this.gameObject);
            
        }
        
    }
}
