using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
public class PlayerTriggerManager : MonoBehaviour
{
    private ParticleSystem windEffectParticleSystem;
    public PlayerFreeFallManager playerFallScript;
    public PlayerManager playerFlyingScript;
    public Image gettingHealthUIEffect;
    public Image gettingShieldUIEffect;
    public Image gettingRageUIEffect;
    public Color shieldEffectColor;
    public Color healthEffectColor;
    public Color RageEffectColor;
    [SerializeField]GameObject moneyParticlePrefab;
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
            GameDataManager.Instance.Totalmoney += 10;
            Instantiate(moneyParticlePrefab,transform.position+Vector3.forward,Quaternion.identity);
            Destroy(other.gameObject);
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
            gettingRageUIEffect.DOColor(RageEffectColor, 0.3f).OnComplete(() =>
            {
                Color temp = RageEffectColor;
                temp.a = 0;
                gettingRageUIEffect.DOColor(temp, 0.2f);
            });
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
            PlayerManager.Instance.clouds.SetActive(false);
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
           Debug.Log("AAAAAAAAAAAAAAAAA");
        EnvironmentMover.Instance.forwardMoveSpeed = 2;
           PlayerManager.Instance.myAnimator.SetBool("ObstacleHit",true);
           yield return new WaitForSeconds(0.3f);
        EnvironmentMover.Instance.forwardMoveSpeed = 6;
        PlayerManager.Instance.myAnimator.SetBool("ObstacleHit",false); 
    }

   
}