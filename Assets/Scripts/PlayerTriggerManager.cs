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
        if (other.CompareTag("Armor"))
        {
            PlayerManager.Instance.Shield += 15;
           PlayerManager.Instance.shieldGameObject.SetActive(false);
        }
        if (other.CompareTag("FireRateUp"))
        {
            StartCoroutine(FireSpeedUpForSomeTime());
            Destroy(other.gameObject);
        }
        if (other.CompareTag("Obstacle"))
        {            
            Debug.Log("obstacle");
            StartCoroutine(ObstacleHit());
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
            PlayerManager.Instance.clouds.SetActive(false);
            PlayerManager.Instance.shieldGameObject.SetActive(false);


            //change the bounds of the move;
            playerFallScript.distanceBetweenX = playerFlyingScript.distanceBetweenX;
            playerFallScript.distanceBetweenY = playerFlyingScript.distanceBetweenY;
            playerFallScript.enabled = true;
            playerFlyingScript.enabled = false;
        }
        if (other.CompareTag("End"))
        {
            PlayerManager.Instance.environmentMoveScript.enabled = false;
    PlayerManager.Instance.fallMoveScript.enabled=false;
    PlayerManager.Instance.myAnimator.SetBool("isDead",true);
PlayerManager.Instance.agent.transform.GetChild(3).transform.gameObject.SetActive(false);
        }
    }
    public IEnumerator FireSpeedUpForSomeTime(){
           float x = GunManager.Instance.currentFireRate;
           
           GunManager.Instance.inGameFireRateDecreaseAmount = GunManager.Instance.currentFireRate * 0.2f;
           yield return new WaitForSeconds(3);
           GunManager.Instance.inGameFireRateDecreaseAmount = 0;
    }
    
    public IEnumerator ObstacleHit(){
           PlayerManager.Instance.myAnimator.SetBool("ObstacleHit",true);
           yield return new WaitForSeconds(1.2f);
           PlayerManager.Instance.myAnimator.SetBool("ObstacleHit",false); 
    }

   
}