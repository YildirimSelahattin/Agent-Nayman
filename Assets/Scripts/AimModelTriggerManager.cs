using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimModelTriggerManager : MonoBehaviour
{
    [SerializeField] GameObject targetModel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Gun"))
        {
            targetModel.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Gun"))
        {
            targetModel.SetActive(false);
        }
    }
}
