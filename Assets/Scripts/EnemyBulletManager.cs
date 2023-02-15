using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletManager : MonoBehaviour
{
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other) {
        
        if (other.tag == "Player")
        {
            int x = (int)EnemyGunManager.Instance.EnemyGun.ShootingConfig.BulletDamage;
            other.gameObject.GetComponent<PlayerManager>().getHit(x);
           Destroy(this.gameObject);
         
        }
        if (other.tag == "Obstacle")
        {
            Destroy(this.gameObject);
        }
        
    }
}
