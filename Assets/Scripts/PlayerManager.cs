using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using static UnityEngine.Rendering.DebugUI;
using UnityEditor;

public class PlayerManager : MonoBehaviour
{
    [Header("MoveBoundaries")]
    [SerializeField] Transform topLimit;
    [SerializeField] Transform botLimit;
    [SerializeField] Transform rightLimit;
    [SerializeField] Transform leftLimit;
    float screenWidth;
    float screenHeigth;
    float distanceBetweenX;
    float distanceBetweenY;
    public Transform startPos;
    [Header("AgentProperties")]
    [Range(0f,1f)] public float maxSpeed;
    [Range(0f,1f)] public float camSpeed;
    [Range(0f, 50f)] public float pathSpeed;
    [Range(0f, 1000f)] public float agentRotateSpeed;
    public ParticleSystem agentTrail;
    public GameObject agentParachute;

    private float velocity, camVelocity_x,camVelocity_y;
    private Camera mainCam;
    public Transform path;
    private Rigidbody rb;
    [SerializeField] public  GameObject agent;
    public ParticleSystem CollideParticle;
    public ParticleSystem Dust;
    [SerializeField] LayerMask EnemyMask;
    public static Animator myAnimator;
    public static PlayerManager Instance;
    public Vector3 wantedRotationFlying;
    public EnvironmentMover environmentMoveScript;
    public float Health = 100f;
    void Start()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        mainCam = Camera.main;
        rb = GetComponent<Rigidbody>();
        myAnimator = GetComponent<Animator>();
        screenWidth = Screen.width;
        screenHeigth = Screen.height;
        distanceBetweenX = Mathf.Abs(leftLimit.position.x - rightLimit.position.x);
        distanceBetweenY = Mathf.Abs(topLimit.position.y - botLimit.position.y);
    }
    
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch curTouch = Input.GetTouch(0);
            float x = 2*(curTouch.deltaPosition.x * distanceBetweenX/(screenWidth));
            float y = 2*(curTouch.deltaPosition.y * distanceBetweenY/(screenHeigth));
            Debug.Log("x"+ curTouch.deltaPosition.x +" y" + curTouch.deltaPosition.y);

            Vector3 playVelocity = new Vector3(x, y, 0);
            Vector3 tempLoc =  playVelocity + transform.localPosition ;
            tempLoc.x = Mathf.Clamp(tempLoc.x, leftLimit.position.x,rightLimit.position.x);
            tempLoc.y = Mathf.Clamp(tempLoc.y, botLimit.position.y,topLimit.position.y);
            transform.localPosition = tempLoc;

            
            
        }

    }

    private void LateUpdate()
    {
        var cameraNewPos = mainCam.transform.position;
        
        if (rb.isKinematic)
            mainCam.transform.position = new Vector3(Mathf.SmoothDamp(cameraNewPos.x,transform.position.x,ref camVelocity_x,camSpeed )
                , Mathf.SmoothDamp(cameraNewPos.y,transform.position.y + 3f,ref camVelocity_y,camSpeed ),cameraNewPos.z);
  
    }
    
    public void StartFalling()
    {
        myAnimator.SetBool("isStarted", true); // startFlying
        agent.transform.DOMoveZ(startPos.position.z, 0.5f);
        agent.transform.DORotate(wantedRotationFlying, 0.5f).OnComplete(() =>
        {
            agent.GetComponent<PlayerManager>().enabled = true;
            agentTrail.Play();
            environmentMoveScript.enabled = true;


        });

    }

    
}