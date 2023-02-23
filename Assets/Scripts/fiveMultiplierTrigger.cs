using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fiveMultiplierTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.CompareTag("Player")){
            transform.parent.GetComponent<TargetBinaManager>().PlayConfettiAndMultiplyMoney(5);
        }
    }
}
