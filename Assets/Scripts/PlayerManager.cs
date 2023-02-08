using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using static UnityEngine.Rendering.DebugUI;
using UnityEditor;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    [Header("MoveBoundaries")]
    public  Transform topLimit;
    public  Transform botLimit;
    public  Transform rightLimit;
    public  Transform leftLimit;
    float screenWidth;
    float screenHeigth;
    public float distanceBetweenX;
    public float distanceBetweenZ;
    public Transform startPos;
    [Header("AgentProperties")]
    [Range(0f,1f)] public float maxSpeed;
    [Range(0f,1f)] public float camSpeed;
    [Range(0f, 50f)] public float pathSpeed;
    [Range(0f, 1000f)] public float agentRotateSpeed;
    public ParticleSystem agentTrail;


    private float velocity, camVelocity_x,camVelocity_y;
    private Camera mainCam;
    public bool gameStarted;
    public Transform path;
    private Rigidbody rb;
    [SerializeField] public  GameObject agent;
    public ParticleSystem CollideParticle;
    public ParticleSystem Dust;
    [SerializeField] LayerMask EnemyMask;
    public Animator myAnimator;
    public static PlayerManager Instance;
    public Vector3 wantedRotationFlying;
    public EnvironmentMover environmentMoveScript;
    public PlayerFreeFallManager fallMoveScript;

    public float Health = 100f;
    void Start()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        Health = 100f;
        mainCam = Camera.main;
        rb = GetComponent<Rigidbody>();
        screenWidth = Screen.width;
        screenHeigth = Screen.height;
        distanceBetweenX = Mathf.Abs(leftLimit.position.x - rightLimit.position.x);
        distanceBetweenZ = Mathf.Abs(topLimit.position.z - botLimit.position.z);
    }
    
    void Update()
    {
        if (Input.touchCount > 0 && gameStarted == true)
        {
            Touch curTouch = Input.GetTouch(0);
            float x = (curTouch.deltaPosition.x * distanceBetweenX/(screenWidth));
            float z = (curTouch.deltaPosition.y * distanceBetweenZ/(screenHeigth));

            Vector3 playVelocity = new Vector3(x, 0, z);
            Vector3 tempLoc =  playVelocity + transform.localPosition ;
            tempLoc.x = Mathf.Clamp(tempLoc.x, leftLimit.position.x,rightLimit.position.x);
            tempLoc.z = Mathf.Clamp(tempLoc.z, botLimit.position.z, topLimit.position.z);
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
        gameStarted=true;
        myAnimator.SetBool("isStarted", true); // startFlying
        agent.transform.DOMoveZ(startPos.position.z, 0.5f);
        agent.transform.DORotate(wantedRotationFlying, 0.5f).OnComplete(() =>
        {
            agent.GetComponent<PlayerManager>().enabled = true;
            agentTrail.Play();
            environmentMoveScript.enabled = true;


        });

    }
    public void getHit(float damage){

    Health -= damage;
    
    if (Health <= 0)
    {
        
        float x = this.gameObject.transform.position.x+Random.Range(-7,7);
        float y = this.gameObject.transform.position.y-8f;
        float z = this.gameObject.transform.position.z;
        this.gameObject.transform.DOMove(new Vector3(x,y,z),1f);
        this.gameObject.transform.DOScale(0,1f).OnComplete(()=>{
        Destroy(this.gameObject);
        //int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
       // SceneManager.LoadScene(currentSceneIndex);
        });
        
    }

    
    }
}