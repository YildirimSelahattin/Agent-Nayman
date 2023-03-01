using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStartShootManager : MonoBehaviour
{
    public EnemyGunManager enemyGunManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            enemyGunManager.StartShoot();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
