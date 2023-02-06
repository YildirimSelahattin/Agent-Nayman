using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerTriggerManager : MonoBehaviour
{
    private ParticleSystem windEffectParticleSystem;
    public PlayerFreeFallManager playerFallScript;
    public PlayerManager playerFlyingScript;
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
            Destroy(other.gameObject);

        }
        if (other.CompareTag("Health"))
        {
            PlayerManager.Instance.Health +=50f;
            Destroy(other.gameObject);
        }
        if (other.CompareTag("EndOfFlying"))
        {
            Debug.Log("sa");
            //change the bounds of the move;
            playerFallScript.tunnelBottomLimit = playerFlyingScript.botLimit;
            playerFallScript.tunnelTopLimit = playerFlyingScript.topLimit;
            playerFallScript.tunnelLeftLimit = playerFlyingScript.leftLimit;
            playerFallScript.tunnelRightLimit = playerFlyingScript.rightLimit;
            playerFallScript.enabled = true;
            playerFlyingScript.enabled = false;
        }
    }
    public IEnumerator FireSpeedUpForSomeTime(){
           float x = GunManager.Instance.ActiveGun.ShootingConfig.FireRate;
           
           GunManager.Instance.decreaseAmount = GunManager.Instance.ActiveGun.ShootingConfig.FireRate * 0.8f;
           yield return new WaitForSeconds(3);
           GunManager.Instance.decreaseAmount = 0;
    }
    public IEnumerator ObstacleHit(){
           PlayerManager.Instance.myAnimator.SetBool("ObstacleHit",true);
           yield return new WaitForSeconds(1.2f);
           PlayerManager.Instance.myAnimator.SetBool("ObstacleHit",false); 
    }

   
}