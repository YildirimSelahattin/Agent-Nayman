using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
public class PlayerTriggerManager : MonoBehaviour
{
    public static PlayerTriggerManager Instance = null;
    private ParticleSystem windEffectParticleSystem;
    public PlayerFreeFallManager playerFallScript;
    public PlayerManager playerFlyingScript;
    public Image gettingHealthUIEffect;
    public Image gettingShieldUIEffect;
    public Color shieldEffectColor;
    public Color healthEffectColor;
    public bool x1 = false;
    public bool x3 = false;

    public bool x5 = false;

    // Start is called before the first frame update
    void Start()
    {if(Instance ==null){
            Instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
    
        if (other.CompareTag("Money"))
        {
            GameDataManager.Instance.Totalmoney += 10;
            GameManager.Instance.currentMoney += 10;

            Destroy(other.gameObject);
        }
        else if (other.CompareTag("x1"))
        {
            x1 = true;
            GameManager.Instance.currentMoney *= 1;
            
           // this.enabled = false;
        } 
        else if (other.CompareTag("x3"))
        {
            x3=true;
            GameManager.Instance.currentMoney *= 3;
            //this.enabled = false;
            
        }
         else if (other.CompareTag("x5"))
        {
            x5 = true;
            GameManager.Instance.currentMoney *= 5;
            //this.enabled = false;
            
        }
        else if (other.CompareTag("Armor"))
        {
            gettingShieldUIEffect.DOColor(shieldEffectColor, 0.3f).OnComplete(() =>
            {
                Color temp = shieldEffectColor;
                temp.a = 0;
                gettingShieldUIEffect.DOColor(temp, 0.2f);
            });
            PlayerManager.Instance.Shield += 15;
            UIManager.Instance.ChangeShieldText(PlayerManager.Instance.Shield);
           PlayerManager.Instance.shieldGameObject.SetActive(true);
            Destroy(other.gameObject);
        }
        else if(other.CompareTag("FireRateUp"))
        {
            StartCoroutine(FireSpeedUpForSomeTime());
            Destroy(other.gameObject);
        }
        else if(other.CompareTag("Obstacle"))
        {            
            Debug.Log("obstacle");
            StartCoroutine(ObstacleHit());
            Destroy(other.gameObject);
        }

        else if(other.CompareTag("Health"))
        {
            Destroy(other.gameObject);
            gettingHealthUIEffect.DOColor(healthEffectColor, 0.3f).OnComplete(() =>
            {
                Color temp = healthEffectColor;
                temp.a = 0;
                gettingHealthUIEffect.DOColor(temp, 0.2f);
            });
            PlayerManager.Instance.Health +=50;
            UIManager.Instance.ChangeHealthText(PlayerManager.Instance.Health);
        }
        else if(other.CompareTag("EndOfFlying"))
        {
            Debug.Log("sa"); 
          
            PlayerManager.Instance.shieldGameObject.SetActive(false);


            //change the bounds of the move;
            playerFallScript.distanceBetweenX = playerFlyingScript.distanceBetweenX;
            playerFallScript.distanceBetweenY = playerFlyingScript.distanceBetweenY;
            playerFallScript.enabled = true;
            playerFlyingScript.enabled = false;
        }
        else if(other.CompareTag("End"))
        {
            PlayerManager.Instance.environmentMoveScript.enabled = false;
            PlayerManager.Instance.fallMoveScript.enabled=false;
            PlayerManager.Instance.myAnimator.SetBool("isDead",true);
            PlayerManager.Instance.agent.transform.GetChild(3).transform.gameObject.SetActive(false);
            UIManager.Instance.endScreen.SetActive(true);
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