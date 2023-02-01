using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTriggerManager : MonoBehaviour
{
    private ParticleSystem windEffectParticleSystem;
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
        if (other.CompareTag("SpeedUp"))
        {
            //speed up real speed 
            EnvironmentMover.Instance.forwardMoveSpeed *= 1.5f;
            //speed up wind
            var main = windEffectParticleSystem.main;
            main.simulationSpeed= 10;
        }
        if (other.CompareTag("Health"))
        {
            PlayerManager.Instance.Health +=50f;
        }
        if (other.CompareTag("EndOfFlying"))
        {
            //change the bounds of the move;
            CityPrefabManager cityPrefabScript = GameManager.Instance.CityParent.GetComponent<CityPrefabManager>();
            PlayerManager.Instance.topLimit = cityPrefabScript.TopLimit.transform;
            PlayerManager.Instance.botLimit = cityPrefabScript.BotLimit.transform;
            PlayerManager.Instance.leftLimit = cityPrefabScript.LeftLimit.transform;
            PlayerManager.Instance.rightLimit = cityPrefabScript.RightLimit.transform;
            
            //for gun to stop firing
            PlayerManager.Instance.gameStarted=false;
            PlayerManager.Instance.agentParachute.gameObject.SetActive(true);
            gameObject.transform.DORotate(new Vector3(-20, 0, 0), 1f).OnComplete(() =>
            {
                //slow down wind
                var main = windEffectParticleSystem.main;
                main.simulationSpeed = 2;

                //slow down real speed of environment 
                EnvironmentMover.Instance.forwardMoveSpeed *= 0.5f;
            });
            PlayerManager.myAnimator.SetBool("isParachuteOpen",true);
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