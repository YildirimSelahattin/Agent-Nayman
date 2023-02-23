using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetBinaManager : MonoBehaviour
{
    ParticleSystem confetti;
    ParticleSystem confettix3;
    ParticleSystem confettix5;
    // Start is called before the first frame update
    void Start()
    {
        confetti = PlayerFreeFallManager.Instance.targetBuilding.transform.GetChild(0).GetComponent<ParticleSystem>();
        confettix3 = PlayerFreeFallManager.Instance.targetBuilding.transform.GetChild(1).GetComponent<ParticleSystem>();
        confettix5 = PlayerFreeFallManager.Instance.targetBuilding.transform.GetChild(2).GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    public void PlayConfettiAndMultiplyMoney(int multiplierAmount)
    {

        PlayerManager.Instance.environmentMoveScript.enabled = false;
        PlayerManager.Instance.fallMoveScript.enabled = false;
        PlayerManager.Instance.myAnimator.SetBool("Ending", true);
        PlayerManager.Instance.agent.transform.GetChild(3).transform.gameObject.SetActive(false);
        GameDataManager.Instance.TotalMoney += (multiplierAmount - 1) * GameManager.Instance.currentMoney;
        PlayerManager.Instance.agent.transform.DORotate(new Vector3(0, 0, 0), 2f).OnComplete(() =>
        {
            PlayerManager.Instance.agent.transform.GetChild(0).transform.gameObject.SetActive(false);
            UIManager.Instance.endScreen.SetActive(true);
        });
        if (multiplierAmount==1)
        {
            Debug.Log("bu hangi oyun abi");
            confetti.Play();

        }
        else if (multiplierAmount == 3)
        {
            Debug.Log("bu hangi oyun abi x3");
            confettix3.Play();
        }
        else if (multiplierAmount == 5)
        {
            Debug.Log("bu hangi oyun abi x5");
            confettix5.Play();
        }
    }
}