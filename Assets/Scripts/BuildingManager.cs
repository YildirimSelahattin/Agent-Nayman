using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BuildingManager : MonoBehaviour
{
   public GameObject BuildingIndex;
    ParticleSystem confetti;
    ParticleSystem confettix3;
    ParticleSystem confettix5;




public void CheckWin(){
if(BuildingIndex == PlayerFreeFallManager.Instance.targetBuilding)
{
        confetti = BuildingIndex.transform.GetChild(0).GetComponent<ParticleSystem>();


    PlayerManager.Instance.environmentMoveScript.enabled = false;
    PlayerManager.Instance.fallMoveScript.enabled=false;
    PlayerManager.Instance.myAnimator.SetBool("Ending",true);
PlayerManager.Instance.agent.transform.GetChild(3).transform.gameObject.SetActive(false);
    
    PlayerManager.Instance.agent.transform.DORotate(new Vector3(0,0,0),2f).OnComplete(()=>{
PlayerManager.Instance.agent.transform.GetChild(0).transform.gameObject.SetActive(false);
confetti.Play();
GameDataManager.Instance.TotalMoney += GameManager.Instance.currentMoney;


UIManager.Instance.endScreen.SetActive(true);
    });

}
}


public  void PlayConfettibyX(){


}

private void OnTriggerEnter(Collider other) {
    if (other.tag == "Player")
    {
        CheckWin();
    }
}


}
