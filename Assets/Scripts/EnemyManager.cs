using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class EnemyManager : MonoBehaviour
{

    public static EnemyManager Instance = null;
    [SerializeField] public GameObject enemy;
    [SerializeField] GameObject aimModel;
    [SerializeField] GameObject aimTrigger;
    [SerializeField] GameObject briefcaseGameObject;
    [SerializeField] GameObject skullParent;
    [SerializeField] GameObject percentBar;
    [SerializeField] GameObject enemyModel;
    [SerializeField] GameObject gunparent;
    public float Health = 100f;
     float curHealth;
    [SerializeField] float barOriginalScale;
    
    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        curHealth = Health;
    }

    public void getHit(float damage)
    {
        curHealth -= damage;
        if (curHealth <= 0)
        {
            //percentBar.DOValue(0,0.5f);
            briefcaseGameObject.SetActive(true);
            briefcaseGameObject.transform.DOLocalMove(new Vector3(0, 0, 2.5f), 1f);
            briefcaseGameObject.transform.DOScale(new Vector3(0.07f, 0.07f, 0.07f),1f);
            briefcaseGameObject.transform.DOLocalRotate(new Vector3(0,-95,32),1f);
            float x = this.gameObject.transform.position.x + Random.Range(-7, 7);
            float y = this.gameObject.transform.position.y - 8f;
            float z = this.gameObject.transform.position.z;
            Destroy(aimModel);
            Destroy(aimTrigger);
            Destroy(percentBar);
            Destroy(skullParent);
            Destroy(gunparent);
            this.gameObject.transform.DOMove(new Vector3(x, y, z), 3f);
            this.gameObject.transform.DOScale(0,3f).OnComplete(() =>
            {
                Destroy(this.gameObject);
            });
        }
        else
        {
            Debug.Log(curHealth * barOriginalScale / Health);
            percentBar.transform.DOScaleX(curHealth * barOriginalScale / Health,0.5f);
            Debug.Log("ads"+ percentBar.transform.localScale);
            enemyModel.transform.DOShakeRotation(0.3f,30,2,30);
        }



    }
}
