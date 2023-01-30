using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject[] CityPrefabs;
    [SerializeField] GameObject[] LevelPrefabs;
    [SerializeField] GameObject CityParent;
    [SerializeField] GameObject LevelParent;

    // Start is called before the first frame update
    void Start()
    {
        LoadLevel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadLevel()
    {
        Instantiate(LevelPrefabs[GameDataManager.Instance.levelToLoad],LevelParent.transform);
        Instantiate(CityPrefabs[GameDataManager.Instance.levelToLoad],CityParent.transform);

    }
}
