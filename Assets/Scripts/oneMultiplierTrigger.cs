using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oneMultiplierTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    
    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            transform.parent.GetComponent<TargetBinaManager>().PlayConfettiAndMultiplyMoney(1);
           
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }

}
