using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject[] CityPrefabs;
    [SerializeField] GameObject[] LevelPrefabs;
    public GameObject CityParent;
    public  GameObject LevelParent;
    public GameObject currentCity;
    public GameObject currentTargetBuilding;
    public static GameManager Instance; 
    // Start is called before the first frame update
    void Start()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        LoadLevel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadLevel()
    {
        Instantiate(LevelPrefabs[GameDataManager.Instance.levelToLoad],LevelParent.transform);
        currentCity = Instantiate(CityPrefabs[GameDataManager.Instance.levelToLoad],CityParent.transform);
        currentTargetBuilding = currentCity.GetComponent<CityPrefabManager>().GetRandomLandableBuilding();
    }
    
}
