using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerManager : MonoBehaviour
{
    private Transform ball;
    private Vector3 startMousePos, startBallPos;
    private bool moveTheBall,gameState,DetectNewPath;
    [Range(0f,1f)] public float maxSpeed;
    [Range(0f,1f)] public float camSpeed;
    [Range(0f, 50f)] public float pathSpeed;
    [Range(0f, 1000f)] public float ballRotateSpeed;
    private float velocity, camVelocity_x,camVelocity_y;
    private Camera mainCam;
    public Transform path;
    private Rigidbody rb;
    private Collider _collider;
    private Renderer BallRenderer;
    public ParticleSystem CollideParticle;
    public ParticleSystem airEffect;
    public ParticleSystem Dust;
    public ParticleSystem BallTrail;
    public Material[] Ballmats = new Material[2];
    public GameObject air;
    private Vector3 DesireBallPos;
    private bool isOpenParachute = false;
    [SerializeField] LayerMask EnemyMask;
    RaycastHit hit;
    Vector3 moveInput;
    
    void Start()
    {
        ball = transform;
        mainCam = Camera.main;
        rb = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
        BallRenderer = ball.GetChild(1).GetComponent<Renderer>();
    }
    
    void Update()
    {
        
        
        if (Input.GetMouseButtonDown(0) && MenuManager.MenuManagerInstance.GameState)
        {
            moveTheBall = true;
            BallTrail.Play();
            
        }
        else if(Input.GetMouseButtonUp(0))
        {
            moveTheBall = false;
        }

        if (moveTheBall)
        {
            Touch curTouch = Input.GetTouch(0);
            BallTrail.Play();
            float Clampx = curTouch.deltaPosition.x/80;
            float Clampy = curTouch.deltaPosition.y/80;

            
            Vector3 playVelocity = new Vector3(Clampx , Clampy ,0);
            Vector3 tempLoc = ball.transform.localPosition + playVelocity;
            tempLoc.x = Mathf.Clamp(tempLoc.x,-13f,13f);
            tempLoc.y = Mathf.Clamp(tempLoc.y,1.5f,24f);
            ball.transform.DOLocalMove(tempLoc,0.1f);
        
            
          
            
        
        }

        if (MenuManager.MenuManagerInstance.GameState)
        {
            var pathPosition = path.position;
            path.position = Vector3.MoveTowards(pathPosition,new Vector3(pathPosition.x,pathPosition.y,-1000f), Time.deltaTime * pathSpeed);
            ball.GetChild(1).Rotate(Vector3.right * ballRotateSpeed * Time.deltaTime);
        }
        
        /*
        if(curTouch.deltaPosition.x > 0f)
        {
            if (isOpenParachute)
            {
                gameObject.transform.DORotate(new Vector3(-20,0,15), 3f);
            }
            else
            {
                gameObject.transform.DORotate(new Vector3(-20,0,15), 3f);
            }
        }

        if(curTouch.deltaPosition.y < 0f)
        {
            if (isOpenParachute)
            {
                gameObject.transform.DORotate(new Vector3(-20,0,-15), 1f);
            }
            else
            {
                gameObject.transform.DORotate(new Vector3(-20,0,-15), 1f);
            }
        }
        */
    }

    private void LateUpdate()
    {
        var cameraNewPos = mainCam.transform.position;
        
        if (rb.isKinematic)
            mainCam.transform.position = new Vector3(Mathf.SmoothDamp(cameraNewPos.x,ball.transform.position.x,ref camVelocity_x,camSpeed )
                , Mathf.SmoothDamp(cameraNewPos.y,ball.transform.position.y + 3f,ref camVelocity_y,camSpeed ),cameraNewPos.z);
  
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("obstacle"))
        {
            gameObject.SetActive(false);
            MenuManager.MenuManagerInstance.GameState = false;
            MenuManager.MenuManagerInstance.menuElement[2].SetActive(true);
            MenuManager.MenuManagerInstance.menuElement[2].transform.GetChild(0).GetComponent<Text>().text = "You Lose";

        }
        if (other.CompareTag("Money"))
        {
            GameDataManager.money += 10;
            Destroy(other.gameObject);
        }

        switch (other.tag)
        {
            case "red":
                other.gameObject.SetActive(false);
                Ballmats[1] = other.GetComponent<Renderer>().material;
                BallRenderer.materials = Ballmats;
                var NewParticle = Instantiate(CollideParticle, transform.position, Quaternion.identity);
                NewParticle.GetComponent<Renderer>().material = other.GetComponent<Renderer>().material;
                var BallTrailColor = BallTrail.trails;
                BallTrailColor.colorOverLifetime = other.GetComponent<Renderer>().material.color;
                break;
            
            case "green":
                other.gameObject.SetActive(false);
                Ballmats[1] = other.GetComponent<Renderer>().material;
                BallRenderer.materials = Ballmats;
                var NewParticle1 = Instantiate(CollideParticle, transform.position, Quaternion.identity);
                NewParticle1.GetComponent<Renderer>().material = other.GetComponent<Renderer>().material;
                var BallTrailColor_1 = BallTrail.trails;
                BallTrailColor_1.colorOverLifetime = other.GetComponent<Renderer>().material.color;
                break;
            
            case "yellow":
                other.gameObject.SetActive(false);
                Ballmats[1] = other.GetComponent<Renderer>().material;
                BallRenderer.materials = Ballmats;
                var NewParticle2 = Instantiate(CollideParticle, transform.position, Quaternion.identity);
                NewParticle2.GetComponent<Renderer>().material = other.GetComponent<Renderer>().material;
                var BallTrailColor_2 = BallTrail.trails;
                BallTrailColor_2.colorOverLifetime = other.GetComponent<Renderer>().material.color;
                break;
            
            case "blue":
                other.gameObject.SetActive(false);
                Ballmats[1] = other.GetComponent<Renderer>().material;
                BallRenderer.materials = Ballmats;
                var NewParticle3 = Instantiate(CollideParticle, transform.position, Quaternion.identity);
                NewParticle3.GetComponent<Renderer>().material = other.GetComponent<Renderer>().material;
                var BallTrailColor_3 = BallTrail.trails;
                BallTrailColor_3.colorOverLifetime = other.GetComponent<Renderer>().material.color;
                break; 
            case "parachute":
                isOpenParachute = true;
                gameObject.transform.GetChild(0).gameObject.SetActive(true);
                air.SetActive(false);
	            gameObject.transform.DOMove(new Vector3(gameObject.transform.position.x,20,-42),2f);
	            gameObject.transform.DORotate(new Vector3(-20,0,0), 1f);
                break;
        }

        if (other.gameObject.name.Contains("Colorball"))
        {
            PlayerPrefs.SetInt("score", PlayerPrefs.GetInt("score") + 10);
            MenuManager.MenuManagerInstance.menuElement[1].GetComponent<Text>().text = PlayerPrefs.GetInt("score").ToString();      
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("path"))
        {
            rb.isKinematic = _collider.isTrigger = false;
            rb.velocity = new Vector3(0f,8f,0f);
            pathSpeed = pathSpeed * 2;

            var airEffectMain = airEffect.main;
            airEffectMain.simulationSpeed = 10f;
            BallTrail.Stop();
            ballRotateSpeed = 1023;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("path"))
        {
            rb.isKinematic = _collider.isTrigger = true;
            pathSpeed = 30f;
            
            var airEffectMain = airEffect.main;
            airEffectMain.simulationSpeed = 4f;

            Dust.transform.position = other.contacts[0].point + new Vector3(0f,0.3f,0f);
            Dust.GetComponent<Renderer>().material = BallRenderer.material;
            Dust.Play();
            BallTrail.Play();
            ballRotateSpeed = 0;
        } 
    }
    
}
