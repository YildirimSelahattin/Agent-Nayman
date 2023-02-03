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

    public Transform tunnelLeftLimit;
    public Transform tunnelRightLimit;
    public Transform tunnelTopLimit;
    public Transform tunnelBottomLimit;
    [Header("MoveBoundaries")]

    float screenWidth;
    float screenHeigth;
    float distanceBetweenX;
    float distanceBetweenZ;
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

        topLimit = cityPrefabScript.TopLimit.transform;
        botLimit = cityPrefabScript.BotLimit.transform;
        leftLimit = cityPrefabScript.LeftLimit.transform;
        rightLimit = cityPrefabScript.RightLimit.transform;

        mainCam = Camera.main;
        rb = GetComponent<Rigidbody>();
        myAnimator = GetComponent<Animator>();
        screenWidth = Screen.width;
        screenHeigth = Screen.height;
        distanceBetweenX = Mathf.Abs(tunnelLeftLimit.position.x - tunnelRightLimit.position.x);
        distanceBetweenZ = Mathf.Abs(tunnelTopLimit.position.z - tunnelBottomLimit.position.z);

       
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
    }

    void Update()
    {
        if (Input.touchCount > 0 )
        {
            Touch curTouch = Input.GetTouch(0);
            float x = (curTouch.deltaPosition.x * distanceBetweenX / (screenWidth));
            float z =(curTouch.deltaPosition.y * distanceBetweenZ / (screenHeigth));

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
        float distanceBetweenHorizontal = transform.position.x - targetBuilding.transform.position.x;
        float distanceBetweenVertical = transform.position.z - targetBuilding.transform.position.x;

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

        //Vectical adjustments,

        if (transform.position.z > targetBuilding.transform.position.z)
        {
            upArrow.SetActive(false);
            botArrow.SetActive(true);
        }
        else if (transform.position.z > targetBuilding.transform.position.z)
        {
            upArrow.SetActive(true);
            botArrow.SetActive(false);
        }
    }
}
