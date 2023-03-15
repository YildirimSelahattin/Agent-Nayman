using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject[] CityPrefabs;
    [SerializeField] GameObject[] LevelPrefabs;
    [SerializeField] Color[] levelColors;
    [SerializeField] Color[] ObstacleColors;
    [SerializeField] Camera mainCamera;
    public GameObject CityParent;
    public  GameObject LevelParent;
    public GameObject currentCity;
    public GameObject currentTargetBuilding;
    public static GameManager Instance;
    public Material dikenDisMat;
    public Material kupIcMat;
    public int enemyKilled = 0;
    public int currentMoney = 0;

    // Start is called before the first frame update
    void Start()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        LoadLevel();

        GameObject.FindGameObjectWithTag("FlyCam").GetComponent<CinemachineVirtualCamera>().m_Lens.FarClipPlane = 65;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadLevel()
    {
        int levelToLoad = GameDataManager.Instance.levelToLoad %(LevelPrefabs.Length - 1);
        if (levelToLoad == 0)
        {
            levelToLoad += 1;
        }
        Debug.Log("LEVELTOLOAD"+ levelToLoad);
        
        kupIcMat.color = ObstacleColors[levelToLoad];
        dikenDisMat.color = ObstacleColors[levelToLoad];
        mainCamera.backgroundColor= levelColors[levelToLoad];
        DynamicGI.UpdateEnvironment();
        Instantiate(LevelPrefabs[levelToLoad],LevelParent.transform);
        currentCity = Instantiate(CityPrefabs[levelToLoad],CityParent.transform);
        currentTargetBuilding = currentCity.GetComponent<CityPrefabManager>().GetRandomLandableBuilding(levelToLoad%3);
    }
    
}
