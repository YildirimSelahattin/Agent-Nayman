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
    PlayerManager.Instance.myAnimator.SetBool("Ending",true);
    PlayerManager.Instance.transform.GetChild(PlayerManager.Instance.transform.childCount-1).gameObject.SetActive(true);

}

}

private void OnTriggerEnter(Collider other) {
    if (other.tag == "Player")
    {
        CheckWin();
    }
}


}
