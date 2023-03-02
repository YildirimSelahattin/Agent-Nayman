using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetBinaManager : MonoBehaviour
{
    ParticleSystem confetti;
    //ParticleSystem confettix3;
    //ParticleSystem confettix5;

    bool gotThereOnce = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    public void PlayConfettiAndMultiplyMoney(int multiplierAmount)
    {
        if (gotThereOnce == false)
        {


            UIManager.Instance.totalMoneyText.gameObject.SetActive(false);
            UIManager.Instance.levelText.gameObject.SetActive(false);
            //confetti = GameManager.Instance.currentTargetBuilding.transform.GetChild(0).GetComponent<ParticleSystem>();

            //confettix3 = GameManager.Instance.currentTargetBuilding.transform.GetChild(1).GetComponent<ParticleSystem>();
            //confettix5 = GameManager.Instance.currentTargetBuilding.transform.GetChild(2).GetComponent<ParticleSystem>();
            PlayerManager.Instance.environmentMoveScript.enabled = false;
            PlayerManager.Instance.fallMoveScript.movementArrowsParent.SetActive(false);
            PlayerManager.Instance.fallMoveScript.enabled = false;
            PlayerManager.Instance.myAnimator.SetBool("Ending", true);
            PlayerManager.Instance.agent.transform.GetChild(3).transform.gameObject.SetActive(false);
            GameDataManager.Instance.TotalMoney += (multiplierAmount - 1) * GameManager.Instance.currentMoney;
            UIManager.Instance.enemyKilledText.text = "X " + GameManager.Instance.enemyKilled.ToString()+" ENEMY KILLED";
            UIManager.Instance.moneyEarnedText.text ="+ "+( (multiplierAmount ) * GameManager.Instance.currentMoney).ToString()+"$";
            GameDataManager.Instance.SaveData();
            PlayerManager.Instance.agent.transform.DOMoveZ(394.2f, 0.1f);
            PlayerManager.Instance.agent.transform.DOLocalRotate(new Vector3(-119f, 0, 0), 1f).OnComplete(() =>
            {
                PlayerManager.Instance.agent.transform.GetChild(0).transform.gameObject.SetActive(false);
            });
            
            gotThereOnce = true;
            StartCoroutine(GoWinDance());
           
        }
    }

    public IEnumerator GoWinDance()
    {
        yield return new WaitForSeconds(2f);
        UIManager.Instance.winScreen.SetActive(true);
        UIManager.Instance.flyingScreen.SetActive(false);
        PlayerManager.Instance.winAgent.SetActive(true);
        PlayerManager.Instance.myAnimator.SetBool("isWin",true);//cameramove
        yield return new WaitForSeconds(0.2f);
        PlayerManager.Instance.winAnimator.SetBool("startDance", true);
    }
}
