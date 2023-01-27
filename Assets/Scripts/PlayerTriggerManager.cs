using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTriggerManager : MonoBehaviour
{
    private bool isOpenParachute = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("obstacle"))
        {
            gameObject.SetActive(false);
        }
        if (other.CompareTag("Money"))
        {
            GameDataManager.Instance.money += 10;
            Destroy(other.gameObject);
        }
        if (other.CompareTag("FireSpeedUp"))
        {
            StartCoroutine(FireSpeedUpForSomeTime());
            
            Destroy(other.gameObject);
           
            
        }
        if (other.CompareTag("Obstacle"))
        {
            StartCoroutine(ObstacleHit());
            
            Destroy(other.gameObject);
           
            
        }
        if (other.CompareTag("Health"))
        {
            PlayerManager.Instance.Health +=50f;
        }
        if (other.CompareTag("parachute"))
        {
            isOpenParachute = true;
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
            UIManager.Instance.windEffect.Stop();
            gameObject.transform.DOMove(new Vector3(gameObject.transform.position.x, 20, -42), 2f);
            gameObject.transform.DORotate(new Vector3(-20, 0, 0), 1f);
        }
    }
    public IEnumerator FireSpeedUpForSomeTime(){
           float x = GunManager.Instance.ActiveGun.ShootingConfig.FireRate;
           GunManager.Instance.ActiveGun.ShootingConfig.FireRate *= 0.2f;
           yield return new WaitForSeconds(3);
        GunManager.Instance.ActiveGun.ShootingConfig.FireRate = x;
           
       
    }
    public IEnumerator ObstacleHit(){
           PlayerManager.myAnimator.SetBool("ObstacleHit",true);
           yield return new WaitForSeconds(1.2f);
           PlayerManager.myAnimator.SetBool("ObstacleHit",false);

           
       
    }
}