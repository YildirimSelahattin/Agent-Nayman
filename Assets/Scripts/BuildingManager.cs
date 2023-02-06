using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
   public GameObject BuildingIndex;
 



public void CheckWin(){
if(BuildingIndex == PlayerFreeFallManager.Instance.targetBuilding)
{
    Debug.Log("bitti");
    PlayerManager.Instance.environmentMoveScript.enabled = false;
    PlayerManager.Instance.fallMoveScript.enabled=false;
}

}

private void OnTriggerEnter(Collider other) {
    if (other.tag == "Player")
    {
        CheckWin();
    }
}


}
