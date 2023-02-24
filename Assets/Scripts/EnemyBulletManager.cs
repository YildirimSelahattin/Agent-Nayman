using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletManager : MonoBehaviour
{
    
    public AudioClip shootSound;
    
    void Start()
    {
        GameObject sound = new GameObject("sound");
        sound.AddComponent<AudioSource>().PlayOneShot(shootSound);
        Destroy(sound, shootSound.length); // Creates new object, add to it audio source, play sound, destroy this object after playing is done
/*
        if (GameDataManager.Instance.playSound == 1)
        {
            GameObject sound = new GameObject("sound");
            sound.AddComponent<AudioSource>().PlayOneShot(shootSound);
            Destroy(sound, shootSound.length); // Creates new object, add to it audio source, play sound, destroy this object after playing is done
        }
 */
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
