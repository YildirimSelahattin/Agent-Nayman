using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityPrefabManager : MonoBehaviour
{

    public GameObject[] landableBuildings ;
    public GameObject TopLimit;
    public GameObject BotLimit;
    public GameObject RightLimit;
    public GameObject LeftLimit;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject GetRandomLandableBuilding()
    {
        int randomIndex = Random.Range(0, landableBuildings.Length);
        landableBuildings[randomIndex].transform.GetChild(0).gameObject.SetActive(true);
        return landableBuildings[randomIndex];
    }
}
