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
        
        if (other.CompareTag("Enemy"))
        {
            gameObject.SetActive(false);
            Destroy(other.gameObject);
            
        }
        Destroy(other.gameObject);
    }
}
