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
            GameDataManager.Instance.TotalMoney += 10;
            GameManager.Instance.currentMoney += 10;
            StartCoroutine(MoneyCollectAnim());
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
           
            PlayerManager.Instance.getHit(10);
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
            UIManager.Instance.loseScreen.SetActive(true);
            Vector3 position = PlayerManager.Instance.agent.transform.position;
            position.z += 50f;   
            PlayerManager.Instance.agent.transform.GetChild(0).transform.gameObject.SetActive(false);
            PlayerManager.Instance.environmentMoveScript.enabled = false;
            PlayerManager.Instance.fallMoveScript.enabled=false;
            UIManager.Instance.flyingScreen.SetActive(false);

            PlayerManager.Instance.agent.transform.DORotate(new Vector3(-90, 180f, 0f),0.3f);

            PlayerManager.Instance.agent.transform.DOMove(position,2f).OnComplete(() =>
            {
                UIManager.Instance.loseScreen.SetActive(true);
                PlayerManager.Instance.loseAgent.SetActive(true);
                PlayerManager.Instance.myAnimator.SetBool("isLose", true);
                PlayerManager.Instance.loseAnimator.SetBool("Lose", true);
                
                
            });
            
        }
    }
    public IEnumerator FireSpeedUpForSomeTime(){
           float x = GunManager.Instance.currentFireRate;
           
           GunManager.Instance.inGameFireRateDecreaseAmount = GunManager.Instance.currentFireRate * 0.2f;
           yield return new WaitForSeconds(3);
           GunManager.Instance.inGameFireRateDecreaseAmount = 0;
    }
    
    public IEnumerator ObstacleHit(){
           
        EnvironmentMover.Instance.forwardMoveSpeed = 2;
        if (PlayerManager.Instance.Health <=0)
        {
            PlayerManager.Instance.myAnimator.SetBool("isDead",true);

        }
        else
        {
            PlayerManager.Instance.myAnimator.SetBool("ObstacleHit",true);
            yield return new WaitForSeconds(.5f);
            EnvironmentMover.Instance.forwardMoveSpeed = 12;
            PlayerManager.Instance.myAnimator.SetBool("ObstacleHit",false); 
        }  
        
    }
    public IEnumerator MoneyCollectAnim()
    {
        
        PlayerManager.Instance.myAnimator.SetBool("getBriefcase", true);
        yield return new WaitForSeconds(0.8f);
        PlayerManager.Instance.myAnimator.SetBool("getBriefcase", false);
    }

}