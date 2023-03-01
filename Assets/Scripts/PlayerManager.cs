﻿using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEditor;
using UnityEngine.SceneManagement;
using DG.Tweening;
using System.Collections;

public class PlayerManager : MonoBehaviour
{
    [Header("MoveBoundaries")]
    public Transform topLimit;
    public Transform botLimit;
    public Transform rightLimit;
    public Transform leftLimit;
    float screenWidth;
    float screenHeigth;
    public float distanceBetweenX;
    public float distanceBetweenY;
    public Transform startPos;
    [Header("AgentProperties")]
    public int Health;
    public int Shield;
    public Animator myAnimator;
    [Range(0f, 1f)] public float maxSpeed;
    [Range(0f, 1f)] public float camSpeed;
    [Range(0f, 50f)] public float pathSpeed;
    [Range(0f, 1000f)] public float agentRotateSpeed;
    public ParticleSystem agentTrail;
    public GameObject shieldGameObject;

    private float velocity, camVelocity_x, camVelocity_y;
    private Camera mainCam;
    public bool gameStarted;
    public Transform path;
    private Rigidbody rb;
    [SerializeField] public GameObject agent;
    [SerializeField] GameObject flyingAgentModel;
    public ParticleSystem CollideParticle;
    public ParticleSystem Dust;
    [SerializeField] LayerMask EnemyMask;
    public static PlayerManager Instance;
    public Vector3 wantedRotationFlying;
    public EnvironmentMover environmentMoveScript;
    public PlayerFreeFallManager fallMoveScript;
    public GameObject startPoseAgent;
    public GameObject clouds;
    public Image gettingShotUIEffect;
    public Color damageEffectColor;
    public bool isAdPlayed = false;
    
    [Header("Win/Lose anim")]
    public GameObject winAgent;
    public Animator winAnimator;
    public Animator loseAnimator;
    public GameObject loseAgent;
    

    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        Health = GameDataManager.Instance.playerHealth;
        mainCam = Camera.main;
        rb = GetComponent<Rigidbody>();
        screenWidth = Screen.width;
        screenHeigth = Screen.height;
        distanceBetweenX = Mathf.Abs(leftLimit.position.x - rightLimit.position.x);
        distanceBetweenY = Mathf.Abs(topLimit.position.y - botLimit.position.y);
    }

    void Update()
    {
        if (Input.touchCount > 0 && gameStarted == true)
        {
            Touch curTouch = Input.GetTouch(0);
            float x = 2f * (curTouch.deltaPosition.x * distanceBetweenX / screenWidth);
            float y = 3f*(curTouch.deltaPosition.y * distanceBetweenY / screenHeigth);
            // Debug.Log((curTouch.deltaPosition.x * distanceBetweenX / screenWidth) + "," + (curTouch.deltaPosition.y * distanceBetweenY / screenHeigth));
            Vector3 playVelocity = new Vector3(x, y, 0);
            Vector3 tempLoc = playVelocity + transform.position;
            tempLoc.x = Mathf.Clamp(tempLoc.x, leftLimit.position.x, rightLimit.position.x);
            tempLoc.y = Mathf.Clamp(tempLoc.y, botLimit.position.y, topLimit.position.y);
            transform.position = tempLoc;
        }
    }

    private void LateUpdate()
    {
        var cameraNewPos = mainCam.transform.position;

        if (rb.isKinematic)
            mainCam.transform.position = new Vector3(Mathf.SmoothDamp(cameraNewPos.x, transform.position.x, ref camVelocity_x, camSpeed)
                , Mathf.SmoothDamp(cameraNewPos.y, transform.position.y + 3f, ref camVelocity_y, camSpeed), cameraNewPos.z);

    }

    public void StartFalling()
    {
        startPoseAgent.SetActive(false);
        flyingAgentModel.SetActive(true);
        UIManager.Instance.windEffect.gameObject.SetActive(true);
        UIManager.Instance.windEffect.Play();
        Health = GameDataManager.Instance.playerHealth;
        Shield = GameDataManager.Instance.playerShield;
        UIManager.Instance.ChangeHealthText(Health);
        UIManager.Instance.ChangeShieldText(Shield);
        if (Shield > 0)
        {
            shieldGameObject.SetActive(true);
        }
        myAnimator.SetBool("isStarted", true); //startFlying
        agent.transform.DOMove(startPos.position, 1.5f);
        agent.transform.DORotate(wantedRotationFlying, 2f).OnComplete(() =>
        {
            gameStarted = true;
            agent.GetComponent<PlayerManager>().enabled = true;
            agentTrail.Play();
            environmentMoveScript.enabled = true;
        });

    }
    public void getHit(int damage)
    {
        gettingShotUIEffect.DOColor(damageEffectColor, 0.3f).OnComplete(() =>
        {
            Color temp = damageEffectColor;
            temp.a = 0;
            gettingShotUIEffect.DOColor(temp, 0.2f);
        }) ;
        if (Shield > 0)
        {//if player has shield
            Shield -= damage;
            if (Shield < 0)
            {
                Health += Shield;
                Shield = 0;
                UIManager.Instance.ChangeShieldText(Shield);
                shieldGameObject.SetActive(false);
                //closeshield
            }
            UIManager.Instance.ChangeShieldText(Shield);
        }
        else
        {
            Health -= damage;
            PlayerManager.Instance.myAnimator.SetBool("isHit"+(int)Random.Range(1,3), true);
            StartCoroutine(PlayHitAnim());
        }

        if (Health <= 0 && isAdPlayed)
        {
            UIManager.Instance.ChangeHealthText(0);
            float x = this.gameObject.transform.position.x + Random.Range(-7, 7);
            float y = this.gameObject.transform.position.y - 8f;
            float z = this.gameObject.transform.position.z;
            this.gameObject.transform.DOMove(new Vector3(x, y, z), 1f);
            this.gameObject.transform.DOScale(0, 1f).OnComplete(() =>
            {
                PlayerManager.Instance.environmentMoveScript.enabled = false;
                PlayerManager.Instance.fallMoveScript.enabled=false;
                PlayerManager.Instance.agent.transform.GetChild(3).transform.gameObject.SetActive(false);
                UIManager.Instance.loseScreen.SetActive(true);
                PlayerManager.Instance.loseAgent.SetActive(true);
                UIManager.Instance.flyingScreen.SetActive(false);

                PlayerManager.Instance.myAnimator.SetBool("isLose", true);
                PlayerManager.Instance.loseAnimator.SetBool("Lose", true);
                UIManager.Instance.totalMoneyText.gameObject.SetActive(false);
                UIManager.Instance.levelText.gameObject.SetActive(false);
                //int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
                // SceneManager.LoadScene(currentSceneIndex);
            });

        }
        else if (Health <= 0 && !isAdPlayed)
        {
            UIManager.Instance.ChangeHealthText(0);
            PlayerManager.Instance.myAnimator.SetBool("isDead", true);

            PlayerManager.Instance.environmentMoveScript.enabled = false;
            UIManager.Instance.RevivePanelScreen.SetActive(true);
          

          
        }
        else
        {
            UIManager.Instance.ChangeHealthText(Health);
        }

    }

    public IEnumerator PlayHitAnim()
    {
        PlayerManager.Instance.myAnimator.SetBool("isHit" + 1.ToString(), false);
        PlayerManager.Instance.myAnimator.SetBool("isHit" + 2.ToString(), false);
        int random = (int)Random.Range(1, 3);
        PlayerManager.Instance.myAnimator.SetBool("isHit" + random, true);
        yield return new WaitForSeconds(0.3f);
        PlayerManager.Instance.myAnimator.SetBool("isHit"+ random, false);
    }
    public IEnumerator ObstacleHit()
    {
        Debug.Log("AAAAAAAAAAAAAAAAA");
        EnvironmentMover.Instance.forwardMoveSpeed = 2;
        PlayerManager.Instance.myAnimator.SetBool("ObstacleHit", true);
        yield return new WaitForSeconds(0.3f);
        EnvironmentMover.Instance.forwardMoveSpeed = 6;
        PlayerManager.Instance.myAnimator.SetBool("ObstacleHit", false);
    }
}