using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BuildingManager : MonoBehaviour
{
   public GameObject BuildingIndex;
    public  ParticleSystem confetti;
    public  ParticleSystem confettix3;
    public  ParticleSystem confettix5;


private void Start() {
    

    
}

public void CheckWin(){
if(BuildingIndex == PlayerFreeFallManager.Instance.targetBuilding)
{
    confetti = PlayerFreeFallManager.Instance.targetBuilding.transform.GetChild(0).GetComponent<ParticleSystem>();
    confettix3 = PlayerFreeFallManager.Instance.targetBuilding.transform.GetChild(1).GetComponent<ParticleSystem>();
    confettix5 = PlayerFreeFallManager.Instance.targetBuilding.transform.GetChild(2).GetComponent<ParticleSystem>();


    PlayerManager.Instance.environmentMoveScript.enabled = false;
    PlayerManager.Instance.fallMoveScript.enabled=false;
    PlayerManager.Instance.myAnimator.SetBool("Ending",true);
PlayerManager.Instance.agent.transform.GetChild(3).transform.gameObject.SetActive(false);
    
    PlayerManager.Instance.agent.transform.DORotate(new Vector3(0,0,0),2f).OnComplete(()=>{
PlayerManager.Instance.agent.transform.GetChild(0).transform.gameObject.SetActive(false);
//confetti.Play();

GameDataManager.Instance.Totalmoney += GameManager.Instance.currentMoney;
if (PlayerTriggerManager.Instance.x1 == true)
{
    Debug.Log("bu hangi oyun abi");
    confetti.Play();
    
}
else if (PlayerTriggerManager.Instance.x3 == true)
{
    Debug.Log("bu hangi oyun abi x3");

    confettix3.Play();
}
else if (PlayerTriggerManager.Instance.x5 == true)
{
    Debug.Log("bu hangi oyun abi x5");

    confettix5.Play();
}

UIManager.Instance.endScreen.SetActive(true);
    });


}

}

public  void PlayConfettibyX(){
if (PlayerTriggerManager.Instance.x1 == true)
{
    Debug.Log("bu hangi oyun abi");
    confetti.Play();
    
}
else if (PlayerTriggerManager.Instance.x3 == true)
{
    Debug.Log("bu hangi oyun abi x3");

    confettix3.Play();
}
else if (PlayerTriggerManager.Instance.x5 == true)
{
    Debug.Log("bu hangi oyun abi x5");

    confettix5.Play();
}

}

private void OnTriggerEnter(Collider other) {
    if (other.tag == "Player")
    {
        CheckWin();
    }
}




}
