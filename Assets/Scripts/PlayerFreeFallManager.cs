using DG.Tweening;
using FIMSpace.Basics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFreeFallManager : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("MoveBoundaries")]
    public Transform topLimit;
    public Transform botLimit;
    public Transform rightLimit;
    public Transform leftLimit;

    [Header("MoveBoundaries")]

    float screenWidth;
    float screenHeigth;
    public float distanceBetweenX;
    public float distanceBetweenZ;
    public Transform startPos;

    public GameObject agentParachute;
    public static PlayerFreeFallManager Instance;
    private ParticleSystem windEffectParticleSystem;
    private float velocity, camVelocity_x, camVelocity_y;
    private Camera mainCam;
    public Transform path;
    private Rigidbody rb;
    [Range(0f, 1f)] public float camSpeed;
    public static Animator myAnimator;

    public GameObject targetBuilding;
    public GameObject movementArrowsParent;
    public GameObject leftArrow;
    public GameObject upArrow;
    public GameObject botArrow;
    public GameObject rightArrow;

    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        PlayerManager.Instance.gameStarted = false;
        movementArrowsParent.SetActive(true);

        CityPrefabManager cityPrefabScript = GameManager.Instance.currentCity.GetComponent<CityPrefabManager>();
        targetBuilding = cityPrefabScript.GetRandomLandableBuilding();
        leftLimit = cityPrefabScript.LeftLimit.transform;
        topLimit = cityPrefabScript.TopLimit.transform;
        botLimit = cityPrefabScript.BotLimit.transform;
        rightLimit = cityPrefabScript.RightLimit.transform;

        

        mainCam = Camera.main;
        rb = GetComponent<Rigidbody>();
        myAnimator = GetComponent<Animator>();
        screenWidth = Screen.width;
        screenHeigth = Screen.height;
       
        agentParachute.gameObject.SetActive(true);
        gameObject.transform.DORotate(new Vector3(32, 0, 0), 1f).OnComplete(() =>
        {
            //slow down wind
            var main = windEffectParticleSystem.main;
            main.simulationSpeed = 2;

            //slow down real speed of environment 
            EnvironmentMover.Instance.forwardMoveSpeed *= 0.5f;
        });
        PlayerManager.Instance.myAnimator.SetBool("isParachuteOpen", true);
        OpenArrows();
    }

    void Update()
    {
        if (Input.touchCount > 0 )
        {
            Touch curTouch = Input.GetTouch(0);
            float x =(curTouch.position.x-screenWidth/2) * distanceBetweenX / (screenWidth);
            x /= 10;
            float z =(curTouch.position.y-screenHeigth/2) * distanceBetweenZ / (screenHeigth);
            z /= 10;
            Vector3 playVelocity = new Vector3(x, 0, z);
            Vector3 tempLoc = playVelocity + transform.localPosition;
            tempLoc.x = Mathf.Clamp(tempLoc.x, leftLimit.position.x, rightLimit.position.x);
            tempLoc.z = Mathf.Clamp(tempLoc.z, botLimit.position.z, topLimit.position.z);
            transform.localPosition = tempLoc;

            OpenArrows();

        }
    }

    private void LateUpdate()
    {
        var cameraNewPos = mainCam.transform.position;

        if (rb.isKinematic)
            mainCam.transform.position = new Vector3(Mathf.SmoothDamp(cameraNewPos.x, transform.position.x, ref camVelocity_x, camSpeed)
                , Mathf.SmoothDamp(cameraNewPos.y, transform.position.y + 3f, ref camVelocity_y, camSpeed), cameraNewPos.z);

    }

    public void OpenArrows()
    {

        if (Mathf.Abs(targetBuilding.transform.position.x - transform.position.x) < 50)
        {
            //Horizontal adjustments
            if (transform.position.x > targetBuilding.transform.position.x)
            {
                leftArrow.SetActive(true);
                rightArrow.SetActive(false);
            }
            else if (transform.position.x < targetBuilding.transform.position.x)
            {
                rightArrow.SetActive(true);
                leftArrow.SetActive(false);
            }
        }
        else
        {
            leftArrow.SetActive(false);
            rightArrow.SetActive(false);
        }

        //Vectical adjustments,
        if (Mathf.Abs(targetBuilding.transform.position.y - transform.position.y) < 50)
        {
            if (transform.position.y > targetBuilding.transform.position.y)
            {
                upArrow.SetActive(false);
                botArrow.SetActive(true);
            }
            else if (transform.position.y > targetBuilding.transform.position.y)
            {
                upArrow.SetActive(true);
                botArrow.SetActive(false);
            }
        }
        else
        {
            upArrow.SetActive(false);
            botArrow.SetActive(false);
        }
    }
}
