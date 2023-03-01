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

    public GameObject GetRandomLandableBuilding(int index)
    {
        
        landableBuildings[index].transform.GetChild(1).gameObject.SetActive(true);
        landableBuildings[index].transform.GetChild(0).gameObject.SetActive(false);

        landableBuildings[index].transform.GetChild(1).transform.GetChild(2).gameObject.SetActive(true);//open arrow 
        landableBuildings[index].transform.GetChild(1).transform.GetChild(1).gameObject.SetActive(true);//open base 
        return landableBuildings[index].transform.GetChild(1).gameObject;
    }
}
